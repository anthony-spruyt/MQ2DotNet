﻿using Microsoft.Extensions.DependencyInjection;
using MQ2DotNet.MQ2API.DataTypes;
using MQFlux.Extensions;
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
        private readonly IMQContext context;
        private readonly ConcurrentDictionary<string, DateTime> cachedCommands;

        private SemaphoreSlim semaphore;
        private bool disposedValue;

        public ItemService(IMQLogger mqLogger, IMQContext context)
        {
            this.mqLogger = mqLogger;
            this.context = context;

            cachedCommands = new ConcurrentDictionary<string, DateTime>();
            semaphore = new SemaphoreSlim(1);
        }

        public async Task AutoInventoryAsync(Predicate<ItemType> predicate = null, CancellationToken cancellationToken = default)
        {
            DateTime waitUntil = DateTime.UtcNow + TimeSpan.FromSeconds(2);

            // Wait for something to hit the cursor up to a timeout.
            while (waitUntil >= DateTime.UtcNow && context.TLO.Cursor == null && !cancellationToken.IsCancellationRequested)
            {
                await MQFlux.Yield(cancellationToken);
            }

            if (cancellationToken.IsCancellationRequested || (predicate != null && !predicate(context.TLO.Cursor)))
            {
                return;
            }

            mqLogger.Log($"Putting [\ag{context.TLO.Cursor.Name}\aw] into your inventory", TimeSpan.Zero);

            while (context.TLO.Cursor != null && !cancellationToken.IsCancellationRequested)
            {
                context.MQ.DoCommand("/autoinv");

                await MQFlux.Yield(cancellationToken);
            }
        }

        public Task DestroyAsync(string itemName)
        {
            if (string.Compare(itemName, context.TLO.Cursor?.Name) == 0)
            {
                context.MQ.DoCommand("/destroy");
            }

            return Task.CompletedTask;
        }

        public Task DropAsync(string itemName)
        {
            if (string.Compare(itemName, context.TLO.Cursor?.Name) == 0)
            {
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
                mqLogger.Log($"Moving [\ag{item.Name}\aw] in pack {packNumber} slot {packSlotNumber} to cursor", TimeSpan.Zero);
                command = $"/nomodkey /shiftkey /itemnotify in pack{packNumber} {packSlotNumber} leftmouseup";
            }
            else
            {
                // Directly in a slot
                mqLogger.Log($"Moving [\ag{item.Name}\aw] in inv slot {item.ItemSlot} to cursor", TimeSpan.Zero);
                command = $"/nomodkey /shiftkey /itemnotify {item.ItemSlot} leftmouseup";
            }

            context.MQ.DoCommand(command);

            var waitUntil = DateTime.UtcNow + TimeSpan.FromSeconds(2);

            // Check if we picked the item up successfully.
            while (context.TLO.Cursor == null || context.TLO.Cursor.ID.Value != item.ID.Value)
            {
                await MQFlux.Yield(cancellationToken);

                if (waitUntil < DateTime.UtcNow || cancellationToken.IsCancellationRequested)
                {
                    mqLogger.Log("foooooooo 1");
                    return false;
                }
            }

            // Move item from cursor into target slot
            if (invSubSlot.HasValue && invSubSlot.Value > 0)
            {
                // In a container
                packNumber = GetPackNumber(invSlot);
                packSlotNumber = invSubSlot.Value;
                mqLogger.Log($"Moving [\ag{context.TLO.Cursor.Name}\aw] on cursor to pack {packNumber} slot {packSlotNumber}", TimeSpan.Zero);
                command = $"/nomodkey /shiftkey /itemnotify in pack{packNumber} {packSlotNumber} leftmouseup";
            }
            else
            {
                // Directly in a slot
                mqLogger.Log($"Moving [\ag{context.TLO.Cursor.Name}\aw] on cursor to inv slot {invSlot}", TimeSpan.Zero);
                command = $"/nomodkey /shiftkey /itemnotify {invSlot} leftmouseup";
            }

            context.MQ.DoCommand(command);

            waitUntil = DateTime.UtcNow + TimeSpan.FromSeconds(2);

            // Make sure the item we picked up is not still on the cursor.
            while (context.TLO.Cursor != null && context.TLO.Cursor.ID.Value == item.ID.Value)
            {
                await MQFlux.Yield(cancellationToken);

                if (waitUntil < DateTime.UtcNow || cancellationToken.IsCancellationRequested)
                {
                    mqLogger.Log("foooooooo 2");
                    return false;
                }
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
                    mqLogger.Log($"Moving [\ag{context.TLO.Cursor.Name}\aw] on cursor to pack {packNumber} slot {packSlotNumber}", TimeSpan.Zero);
                    command = $"/nomodkey /shiftkey /itemnotify in pack{packNumber} {packSlotNumber} leftmouseup";
                }
                else
                {
                    // Directly in a slot
                    mqLogger.Log($"Moving [\ag{context.TLO.Cursor.Name}\aw] on cursor to inv slot {itemSlot}", TimeSpan.Zero);
                    command = $"/nomodkey /shiftkey /itemnotify {itemSlot} leftmouseup";
                }

                context.MQ.DoCommand(command);

                waitUntil = DateTime.UtcNow + TimeSpan.FromSeconds(2);

                // Make sure the cursor is now empty.
                while (context.TLO.Cursor != null)
                {
                    await MQFlux.Yield(cancellationToken);

                    if (waitUntil < DateTime.UtcNow || cancellationToken.IsCancellationRequested)
                    {
                        mqLogger.Log("foooooooo 3");
                        return false;
                    }
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

                string command = $"/useitem {item.Name}";

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

                Task waitForEQTask = Task.Run
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

                await waitForEQTask;

                if (fizzled || interrupted)
                {
                    var reason = fizzled ? "fizzled" : "was interrupted";

                    mqLogger.Log($"{verb} [\ag{item.Name}\aw] \ar{reason}", TimeSpan.Zero);

                    return false;
                }
            }
            catch (TimeoutException)
            {
                // Some spells dont write a message so assume it was successful if it timed out.
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
                mqLogger.Log($"Moving [\ag{item.Name}\aw] \arcancelled - cursor is not empty", TimeSpan.FromSeconds(5));

                return false;
            }

            // TODO
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
