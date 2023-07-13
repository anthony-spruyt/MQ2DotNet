using MediatR;
using MQ2DotNet.MQ2API.DataTypes;
using MQ2Flux.Commands;
using MQ2Flux.Extensions;
using MQ2Flux.Services;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MQ2Flux.Handlers
{
    public class SummonFoodAndDrinkCommandHandler : IRequestHandler<SummonFoodAndDrinkCommand, bool>
    {
        private static readonly string[] summonFoodSpellNames = new string[]
        {
            "Abundant Food",
            "Cornucopia",
            "Gift of Daybreak",
            "Gift of Xev",
            "Summon Food"
        };
        private static readonly string[] summonDrinkSpellNames = new string[]
        {
            "Abundant Drink",
            "Everfount",
            "Gift of Daybreak",
            "Gift of Xev",
            "Summon Drink"
        };
        private readonly IItemService itemService;
        private readonly ISpellCastingService spellCastingService;

        public SummonFoodAndDrinkCommandHandler(IItemService itemService, ISpellCastingService spellCastingService)
        {
            this.itemService = itemService;
            this.spellCastingService = spellCastingService;
        }

        public async Task<bool> Handle(SummonFoodAndDrinkCommand request, CancellationToken cancellationToken)
        {
            if (!request.Character.AutoSummonFoodAndDrink.GetValueOrDefault(false))
            {
                return false;
            }

            var me = request.Context.TLO.Me;
            var allMyInv = me.Inventory.Flatten();
            var actualFoodCount = allMyInv
                .Where(i => i.NoRent && i.IsEdible())
                .Sum(i => i.StackCount);

            if (actualFoodCount < 10 && TryGetSummonSpell(me, summonFoodSpellNames, out var foodSpell))
            {
                if (await spellCastingService.CastAsync(foodSpell, cancellationToken))
                {
                    await itemService.AutoInventoryAsync(cursor => cursor != null && cursor.NoRent, cancellationToken);

                    return true;
                }
            }

            // TODO: This ping pongs between the two instead of doing food first and then drinks...

            var actualDrinkCount = allMyInv
                .Where(i => i.NoRent && i.IsDrinkable())
                .Sum(i => i.StackCount);

            if (actualDrinkCount < 10 && TryGetSummonSpell(me, summonDrinkSpellNames, out var drinkSpell))
            {
                if (await spellCastingService.CastAsync(drinkSpell, cancellationToken))
                {
                    await itemService.AutoInventoryAsync(cursor => cursor != null && cursor.NoRent, cancellationToken);

                    return true;
                }   
            }

            return false;
        }

        private static bool TryGetSummonSpell(CharacterType me, string[] summonSpellNames, out SpellType spell)
        {
            var spellBookSpells = me.SpellBook.Where(i => summonSpellNames.Contains(i.Value.BaseName)).OrderByDescending(i => i.Value.Level);
            var found = spellBookSpells.Any();

            spell = found ? spellBookSpells.First().Value : null;

            return found;
        }
    }
}
