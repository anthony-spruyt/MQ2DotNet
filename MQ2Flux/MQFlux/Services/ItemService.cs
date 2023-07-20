using Microsoft.Extensions.DependencyInjection;
using MQ2DotNet.MQ2API.DataTypes;
using MQFlux.Extensions;
using MQFlux.Utils;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Services
{
    public interface IItemService
    {
        Task AutoInventoryAsync(Predicate<ItemType> predicate = null, CancellationToken cancellationToken = default);
        /// <summary>
        /// Destroy the current item on the cursor if it matches the provided name.
        /// </summary>
        /// <param name="itemName">To ensure you dont destroy something unintended on the cursor the item name is required.</param>
        /// <returns></returns>
        Task DestroyAsync(string itemName);
        /// <summary>
        /// Drops the current item on the cursor if it matches the provided name.
        /// </summary>
        /// <param name="itemName">To ensure you dont drop something unintended on the cursor the item name is required.</param>
        /// <returns></returns>
        Task DropAsync(string itemName);
        /// <summary>
        /// Move an item.
        /// </summary>
        /// <param name="item">The item to move.</param>
        /// <param name="invSlot">The inventory slot. See <see cref="MQ2DotNet.EQ.InvSlot"/>, <seealso cref="MQ2DotNet.EQ.InvSlot2"/>, <seealso cref="MQ2DotNet.EQ.Expansion"/> and <see cref="CharacterType.FIRST_BAG_SLOT"/> etc.</param>
        /// <param name="invSubSlot">The optional base 1 container slot number if moving into a container.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> MoveItemAsync(ItemType item, int invSlot, int? invSubSlot = null, CancellationToken cancellationToken = default);
        Task<bool> UseItemAsync(ItemType item, string verb = "Using", CancellationToken cancellationToken = default);
    }

    public static class ItemServiceExtensions
    {
        public static IServiceCollection AddItemService(this IServiceCollection services)
        {
            return services
                .AddSingleton<IItemService, ItemService>();
        }
    }

    public class ItemService : IItemService, IDisposable
    {
        private readonly IMQLogger mqLogger;
        private readonly IContext context;
        private readonly ConcurrentDictionary<string, DateTime> cachedCommands;

        private SemaphoreSlim semaphore;
        private bool disposedValue;

        public ItemService(IMQLogger mqLogger, IContext context)
        {
            this.mqLogger = mqLogger;
            this.context = context;

            cachedCommands = new ConcurrentDictionary<string, DateTime>();
            semaphore = new SemaphoreSlim(1);
        }

        public async Task AutoInventoryAsync(Predicate<ItemType> predicate = null, CancellationToken cancellationToken = default)
        {
            if (!await Wait.While(() => context.TLO.Cursor == null, TimeSpan.FromSeconds(2), cancellationToken))
            {
                return;
            }

            if (predicate != null && !predicate(context.TLO.Cursor))
            {
                return;
            }

            mqLogger.Log($"Putting [\ag{context.TLO.Cursor.Name}\aw] into your inventory", TimeSpan.Zero);

            var timedoutOrCancelled = !await Wait.While
            (
                () =>
                {
                    var stillOnCursor = context.TLO.Cursor != null;

                    if (stillOnCursor)
                    {
                        context.MQ.DoCommand("/autoinv");
                    }

                    return stillOnCursor;
                }, 
                TimeSpan.FromSeconds(2), 
                cancellationToken
            );

            if (timedoutOrCancelled && context.TLO.Cursor != null && context.TLO.Me.FreeInventory.GetValueOrDefault(0u) == 0u)
            {
                mqLogger.Log($"Cannot put [\ag{context.TLO.Cursor.Name}\aw] into your inventory because it is full", TimeSpan.Zero);
            }
        }

        public Task DestroyAsync(string itemName)
        {
            if (string.Compare(itemName, context.TLO.Cursor?.Name) == 0)
            {
                mqLogger.Log($"\arDestroying \aw[\ag{context.TLO.Cursor.Name}\aw]", TimeSpan.Zero);

                context.MQ.DoCommand("/destroy");
            }

            return Task.CompletedTask;
        }

        public Task DropAsync(string itemName)
        {
            if (string.Compare(itemName, context.TLO.Cursor?.Name) == 0)
            {
                mqLogger.Log($"\arDropping \aw[\ag{context.TLO.Cursor.Name}\aw]", TimeSpan.Zero);

                context.MQ.DoCommand("/drop");
            }

            return Task.CompletedTask;
        }

        public async Task<bool> MoveItemAsync(ItemType item, int invSlot, int? invSubSlot, CancellationToken cancellationToken = default)
        {
            if (!IsValidMove(item, invSlot, invSubSlot))
            {
                return false;
            }

            var debug = false;
            var itemSlot = item.ItemSlot.Value;
            var itemSlot2 = item.ItemSlot2;
            string command;
            int packNumber;
            int packSlotNumber;

            // Move item onto cursor.
            if (item.IsInAContainer())
            {
                // In a container
                packNumber = GetPackNumber(item.ItemSlot.Value);
                packSlotNumber = item.ItemSlot2.Value;
                if (debug) mqLogger.Log($"Moving [\ag{item.Name}\aw] in pack {packNumber} slot {packSlotNumber} to cursor", TimeSpan.Zero);
                command = $"/nomodkey /shiftkey /itemnotify in pack{packNumber} {packSlotNumber} leftmouseup";
            }
            else
            {
                // Directly in a slot
                if (debug) mqLogger.Log($"Moving [\ag{item.Name}\aw] in inv slot {item.ItemSlot} to cursor", TimeSpan.Zero);
                command = $"/nomodkey /shiftkey /itemnotify {item.ItemSlot} leftmouseup";
            }

            context.MQ.DoCommand(command);

            var timeout = TimeSpan.FromSeconds(3);

            // Make sure the item we picked up is on the cursor.
            if (!await Wait.While(() => context.TLO.Cursor == null || context.TLO.Cursor.ID.Value != item.ID.Value, timeout, cancellationToken))
            {
                return false;
            }

            // Move item from cursor into target slot
            if (invSubSlot.HasValue && invSubSlot.Value > 0)
            {
                // In a container
                packNumber = GetPackNumber(invSlot);
                packSlotNumber = invSubSlot.Value;
                if (debug) mqLogger.Log($"Moving [\ag{context.TLO.Cursor.Name}\aw] on cursor to pack {packNumber} slot {packSlotNumber}", TimeSpan.Zero);
                command = $"/nomodkey /shiftkey /itemnotify in pack{packNumber} {packSlotNumber} leftmouseup";
            }
            else
            {
                // Directly in a slot
                if (debug) mqLogger.Log($"Moving [\ag{context.TLO.Cursor.Name}\aw] on cursor to inv slot {invSlot}", TimeSpan.Zero);
                command = $"/nomodkey /shiftkey /itemnotify {invSlot} leftmouseup";
            }

            context.MQ.DoCommand(command);

            // Make sure the item we picked up is not still on the cursor.
            if (!await Wait.While(() => context.TLO.Cursor != null && context.TLO.Cursor.ID.Value == item.ID.Value, timeout, cancellationToken))
            {
                return false;
            }

            // Check if we need to move what was in the target slot back to the source slot.
            if (context.TLO.Cursor != null)
            {
                // Move item from cursor into source slot
                if (itemSlot2.HasValue && itemSlot2.Value > 0)
                {
                    // In a container
                    packNumber = GetPackNumber(itemSlot);
                    packSlotNumber = itemSlot2.Value;
                    if (debug) mqLogger.Log($"Moving [\ag{context.TLO.Cursor.Name}\aw] on cursor to pack {packNumber} slot {packSlotNumber}", TimeSpan.Zero);
                    command = $"/nomodkey /shiftkey /itemnotify in pack{packNumber} {packSlotNumber} leftmouseup";
                }
                else
                {
                    // Directly in a slot
                    if (debug) mqLogger.Log($"Moving [\ag{context.TLO.Cursor.Name}\aw] on cursor to inv slot {itemSlot}", TimeSpan.Zero);
                    command = $"/nomodkey /shiftkey /itemnotify {itemSlot} leftmouseup";
                }

                context.MQ.DoCommand(command);

                // Make sure the cursor is now empty.
                if (!await Wait.While(() => context.TLO.Cursor != null, timeout, cancellationToken))
                {
                    return false;
                }
            }

            return true;
        }

        public async Task<bool> UseItemAsync(ItemType item, string verb = "Using", CancellationToken cancellationToken = default)
        {
            await semaphore.WaitAsync(cancellationToken);

            try
            {
                var me = context.TLO.Me;

                if
                (
                    !item.CanUse ||
                    !item.IsTimerReady() ||
                    me.AmICasting() ||
                    (
                        item.HasCastTime() &&
                        me.Moving
                    )
                )
                {
                    return false;
                }

                PurgeCache();

                string command = $"/useitem \"{item.Name}\"";

                if (!cachedCommands.TryAdd(command, DateTime.UtcNow))
                {
                    return false;
                }

                if (!item.HasCastTime())
                {
                    context.MQ.DoCommand(command);

                    mqLogger.Log($"{verb} [\ag{item.Name}\aw]", TimeSpan.Zero);

                    return true;
                }

                var castTime = item.Clicky.CastTime.Value;
                var timeout = castTime + TimeSpan.FromMilliseconds(1000); // add some fat
                var castOnYou = item.Clicky.Spell.CastOnYou;
                var castOnAnother = item.Clicky.Spell.CastOnAnother;
                var wasCastOnYou = false;
                var wasCastOnAnother = false;
                var fizzled = false;
                var interrupted = false;

                Task<bool> waitForEQTask = Task.Run
                (
                    () => context.Chat.WaitForEQ
                    (
                        text =>
                        {
                            wasCastOnYou = string.Compare(text, castOnYou) == 0;
                            wasCastOnAnother = text.Contains(castOnAnother);
                            fizzled = text.Contains("Your") && text.Contains("spell fizzles");
                            interrupted = text.Contains("Your") && text.Contains("spell is interrupted");

                            return wasCastOnYou || wasCastOnAnother || fizzled || interrupted;
                        },
                        timeout,
                        cancellationToken
                    )
                );

                context.MQ.DoCommand(command);

                mqLogger.Log($"{verb} [\ag{item.Name}\aw]", TimeSpan.Zero);

                // Some spells dont write a message so assume it was successful if it timed out.
                _ = await waitForEQTask;

                if (fizzled || interrupted)
                {
                    var reason = fizzled ? "fizzled" : "was interrupted";

                    mqLogger.Log($"{verb} [\ag{item.Name}\aw] \ar{reason}", TimeSpan.Zero);

                    return false;
                }
            }
            finally
            {
                semaphore.Release();
            }

            return true;
        }

        private bool IsValidMove(ItemType item, int invSlot, int? invSubSlot)
        {
            // We cant move if there is something already on the cursor.
            if (context.TLO.Cursor != null)
            {
                mqLogger.Log($"Move [\ag{item.Name}\aw] \arcancelled \awcursor is not empty", TimeSpan.FromSeconds(5));

                return false;
            }

            // if slot is wearable slot
            if (invSlot >= CharacterType.FIRST_WORN_ITEM && invSlot <= CharacterType.LAST_WORN_ITEM)
            {
                // TODO validate if CanUse covers all these
                // can i wear it IE
                // - race
                // - diety
                // - level
                // - size
                if (!item.CanUse)
                {
                    return false;
                }
            }
            // if slot is inventory slot
            else if (invSlot >= CharacterType.FIRST_BAG_SLOT && invSlot <= CharacterType.LAST_BAG_SLOT)
            {
                // if target slot is a bag
                var targetSlotItem = context.TLO.Me.GetInventoryItem(invSlot);

                if (targetSlotItem != null && targetSlotItem.IsAContainer())
                {
                    // does it fit (size)
                    if (targetSlotItem.SizeCapacity < item.Size)
                    {
                        return false;
                    }
                    // TODO validate if can use Type == "Ammo"
                    // XOR - if not both ammo or neither ammo
                    if ((string.Compare(item.Type, "Ammo", true) == 0) ^ (string.Compare(targetSlotItem.Type, "Ammo", true) == 0))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private int GetPackNumber(int slot)
        {
            return slot - CharacterType.FIRST_BAG_SLOT + 1;
        }

        private void PurgeCache()
        {
            var purgeOlderThan = DateTime.UtcNow - TimeSpan.FromMilliseconds(1000);
            var keys = cachedCommands.Where(i => i.Value < purgeOlderThan).Select(i => i.Key).ToArray();

            foreach (var key in keys)
            {
                cachedCommands.TryRemove(key, out var _);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    if (semaphore != null)
                    {
                        semaphore.Dispose();
                        semaphore = null;
                    }
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~ItemService()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
