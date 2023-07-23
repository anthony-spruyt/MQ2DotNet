using MQ2DotNet.MQ2API.DataTypes;
using MQFlux.Behaviors;
using MQFlux.Core;
using MQFlux.Extensions;
using MQFlux.Models;
using MQFlux.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Commands
{
    public class PutStatFoodInTopSlotsCommand :
        PCCommand,
        ICharacterConfigRequest,
        IConsciousRequest,
        INotCastingRequest,
        INoItemOnCursorRequest
    {
        public bool AllowBard => true;
        public CharacterConfig Character { get; set; }
        public FluxConfig Config { get; set; }
        public IContext Context { get; set; }
    }

    public class PutStatFoodInTopSlotsCommandHandler : PCCommandHandler<PutStatFoodInTopSlotsCommand>
    {
        private readonly IItemService itemService;
        private readonly IMQLogger mqLogger;

        public PutStatFoodInTopSlotsCommandHandler(IItemService itemService, IMQLogger mqLogger)
        {
            this.itemService = itemService;
            this.mqLogger = mqLogger;
        }

        public override async Task<CommandResponse<bool>> Handle(PutStatFoodInTopSlotsCommand request, CancellationToken cancellationToken)
        {
            if (!request.Character.AutoPutStatFoodInTopSlots.GetValueOrDefault(false))
            {
                return CommandResponse.FromResult(false);
            }

            var me = request.Context.TLO.Me;
            var bagsAndContents = me.Bags.Flatten();
            var statFood = GetMostNutritiousConsumable(bagsAndContents.Where(i => i.IsEdible()), me);

            if (statFood != null && !IsInFirstBagSlot(statFood))
            {
                var firstBagSlot = me.GetInventoryItem(CharacterType.FIRST_BAG_SLOT);
                var isBagInSlot1 = firstBagSlot != null && firstBagSlot.IsAContainer();

                var moved = await MoveStatFoodAsync(statFood, isBagInSlot1, cancellationToken);

                return CommandResponse.FromResult(moved);
            }

            var statDrink = GetMostNutritiousConsumable(bagsAndContents.Where(i => i.IsDrinkable()), me);

            if (statDrink != null)
            {
                var firstBagSlot = me.GetInventoryItem(CharacterType.FIRST_BAG_SLOT);
                var isBagInSlot1 = firstBagSlot != null && firstBagSlot.IsAContainer();
                var secondBagSlot = me.GetInventoryItem(CharacterType.FIRST_BAG_SLOT + 1);
                var isBagInSlot2 = secondBagSlot != null && secondBagSlot.IsAContainer();

                if (!IsInSecondBagSlot(statDrink, isBagInSlot1, isBagInSlot2))
                {
                    var moved = await MoveStatDrinkAsync(statDrink, isBagInSlot1, isBagInSlot2, cancellationToken);

                    return CommandResponse.FromResult(moved);
                }
            }

            return CommandResponse.FromResult(false);
        }

        private static bool IsInFirstBagSlot(ItemType item)
        {
            return
                // stat food is already in slot 1 or slot 1 and sub slot 1
                (
                    !item.IsInAContainer() ||
                    item.ItemSlot2 == 1
                ) &&
                item.ItemSlot == CharacterType.FIRST_BAG_SLOT;
        }

        private static bool IsInSecondBagSlot(ItemType statDrink, bool isBagInSlot1, bool isBagInSlot2)
        {
            return
                // stat drink is in bag 1 slot 2
                (
                    statDrink.ItemSlot2 == 2 &&
                    statDrink.ItemSlot == CharacterType.FIRST_BAG_SLOT
                ) ||
                // there is no bag in slot 1
                !isBagInSlot1 &&
                (
                    // and no bag in slot 2 and stat drink is in slot 2
                    (
                        !isBagInSlot2 &&
                        statDrink.ItemSlot == CharacterType.FIRST_BAG_SLOT + 1 &&
                        !statDrink.IsInAContainer()
                    ) ||
                    // or a bag in slot 2 and stat drink is in slot 2 sub slot 1
                    (
                        isBagInSlot2 &&
                        statDrink.ItemSlot == CharacterType.FIRST_BAG_SLOT + 1 &&
                        statDrink.ItemSlot2 == 1
                    )
                );
        }

        private Task<bool> MoveStatDrinkAsync(ItemType item, bool isBagInSlot1, bool isBagInSlot2, CancellationToken cancellationToken = default)
        {
            mqLogger.Log($"Moving stat drink [\ag{item.Name}\aw] to the second slot", TimeSpan.Zero);

            if (isBagInSlot1)
            {
                return itemService.MoveItem(item, CharacterType.FIRST_BAG_SLOT, 2, cancellationToken);
            }
            else
            {
                if (isBagInSlot2)
                {
                    return itemService.MoveItem(item, CharacterType.FIRST_BAG_SLOT + 1, 1, cancellationToken);
                }
                else
                {
                    return itemService.MoveItem(item, CharacterType.FIRST_BAG_SLOT + 1, cancellationToken: cancellationToken);
                }
            }
        }

        private Task<bool> MoveStatFoodAsync(ItemType item, bool isBagInSlot1, CancellationToken cancellationToken = default)
        {
            mqLogger.Log($"Moving stat food [\ag{item.Name}\aw] to the first slot", TimeSpan.Zero);

            if (isBagInSlot1)
            {
                return itemService.MoveItem(item, CharacterType.FIRST_BAG_SLOT, 1, cancellationToken);
            }
            else
            {
                return itemService.MoveItem(item, CharacterType.FIRST_BAG_SLOT, cancellationToken: cancellationToken);
            }
        }

        private ItemType GetMostNutritiousConsumable(IEnumerable<ItemType> items, CharacterType me)
        {
            return items
                .Select(i => new { Item = i, Score = i.GetNutrientScore(me) })
                .Where(i => i.Score > 0)
                .OrderByDescending(i => i.Score)
                .ThenBy(i => i.Item.ID)
                .ThenBy(i => i.Item.ItemSlot)
                .ThenBy(i => i.Item.IsInAContainer() ? i.Item.ItemSlot2.Value : 0)
                .FirstOrDefault()?.Item;
        }
    }
}
