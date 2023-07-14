﻿using MediatR;
using MQ2DotNet.MQ2API.DataTypes;
using MQFlux.Commands;
using MQFlux.Extensions;
using MQFlux.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Handlers
{
    public class SortInventoryCommandHandler : IRequestHandler<SortInventoryCommand, bool>
    {
        private readonly IItemService itemService;
        private readonly IMQLogger mqLogger;

        public SortInventoryCommandHandler(IItemService itemService, IMQLogger mqLogger)
        {
            this.itemService = itemService;
            this.mqLogger = mqLogger;
        }

        public Task<bool> Handle(SortInventoryCommand request, CancellationToken cancellationToken)
        {
            if (!request.Character.AutoSortInventory.GetValueOrDefault(false))
            {
                return Task.FromResult(false);
            }

            var me = request.Context.TLO.Me;
            var bagsAndContents = me.Bags.Flatten();
            var firstBagSlot = me.GetInventoryItem(CharacterType.FIRST_BAG_SLOT);
            var isBagInSlot1 = firstBagSlot != null && firstBagSlot.IsAContainer();
            var secondBagSlot = me.GetInventoryItem(CharacterType.FIRST_BAG_SLOT + 1);
            var isBagInSlot2 = secondBagSlot != null && secondBagSlot.IsAContainer();
            var statFood = GetStatFood(bagsAndContents, me);
            bool moveStatFood = statFood != null &&
                !(
                    // stat food is already in slot 1 or slot 1 and sub slot 1
                    (
                        !statFood.IsInAContainer() || 
                        statFood.ItemSlot2 == 1
                    ) && 
                    statFood.ItemSlot == CharacterType.FIRST_BAG_SLOT
                );

            if (moveStatFood)
            {
                return MoveStatConsumableAsync(statFood, me, isBagInSlot1, isBagInSlot2, cancellationToken);
            }

            var statDrink = GetStatDrink(bagsAndContents, me);
            var moveStatDrink = statDrink != null &&
                !(
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
                    )
                );

            if (moveStatDrink)
            {
                return MoveStatConsumableAsync(statDrink, me, isBagInSlot1, isBagInSlot2, cancellationToken);
            }

            return Task.FromResult(false);
        }

        private Task<bool> MoveStatConsumableAsync(ItemType item, CharacterType me, bool isBagInSlot1, bool isBagInSlot2, CancellationToken cancellationToken = default)
        {
            if (item.IsEdible())
            {
                mqLogger.Log($"Moving stat food [\ag{item.Name}\aw] to the first slot", TimeSpan.Zero);

                if (isBagInSlot1)
                {
                    return itemService.MoveItemAsync(item, CharacterType.FIRST_BAG_SLOT, 1, cancellationToken);
                }
                else
                {
                    return itemService.MoveItemAsync(item, CharacterType.FIRST_BAG_SLOT, cancellationToken: cancellationToken);
                }
            }
            else if (item.IsDrinkable())
            {
                mqLogger.Log($"Moving stat drink [\ag{item.Name}\aw] to the second slot", TimeSpan.Zero);

                if (isBagInSlot1)
                {
                    return itemService.MoveItemAsync(item, CharacterType.FIRST_BAG_SLOT, 2, cancellationToken);
                }
                else
                {
                    if (isBagInSlot2)
                    {
                        return itemService.MoveItemAsync(item, CharacterType.FIRST_BAG_SLOT + 1, 1, cancellationToken);
                    }
                    else
                    {
                        return itemService.MoveItemAsync(item, CharacterType.FIRST_BAG_SLOT + 1, cancellationToken: cancellationToken);
                    }
                }
            }

            return Task.FromResult(false);
        }

        private ItemType GetStatDrink(IEnumerable<ItemType> items, CharacterType me)
        {
            return items
                .Where(i => i.IsDrinkable())
                .OrderByDescending(i => i.GetNutrientScore(me))
                .ThenBy(i => i.ID)
                .ThenBy(i => i.ItemSlot)
                .ThenBy(i => i.IsInAContainer() ? i.ItemSlot2.Value : 0)
                .FirstOrDefault();
        }

        private ItemType GetStatFood(IEnumerable<ItemType> items, CharacterType me)
        {
            return items
                .Where(i => i.IsEdible())
                .OrderByDescending(i => i.GetNutrientScore(me))
                .ThenBy(i => i.ID)
                .ThenBy(i => i.ItemSlot)
                .ThenBy(i => i.IsInAContainer() ? i.ItemSlot2.Value : 0)
                .FirstOrDefault();
        }
    }
}
