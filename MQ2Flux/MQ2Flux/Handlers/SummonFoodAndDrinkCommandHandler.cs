using MediatR;
using MQ2DotNet.MQ2API;
using MQ2DotNet.MQ2API.DataTypes;
using MQ2Flux.Commands;
using MQ2Flux.Extensions;
using MQ2Flux.Services;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MQ2Flux.Handlers
{
    public class SummonFoodAndDrinkCommandHandler : IRequestHandler<SummonFoodAndDrinkCommand>
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

        private readonly ISpellCastingService spellCastingService;
        private readonly IMQ2Logger mq2Logger;

        public SummonFoodAndDrinkCommandHandler(ISpellCastingService spellCastingService, IMQ2Logger mq2Logger)
        {
            this.spellCastingService = spellCastingService;
            this.mq2Logger = mq2Logger;
        }

        public async Task Handle(SummonFoodAndDrinkCommand request, CancellationToken cancellationToken)
        {
            var me = request.Context.TLO.Me;

            if (!me.Spawn.Class.CanCast || me.Combat || me.Moving || me.AmICasting())
            {
                return;
            }

            var allMyInv = me.Inventory.Flatten();
            var actualFoodCount = allMyInv
                .Where(i => i.NoRent && i.IsEdible())
                .Sum(i => i.StackCount);

            if (actualFoodCount < 10 && TryGetSummonSpell(me, summonFoodSpellNames, out var foodSpell))
            {
                await spellCastingService.CastAsync(foodSpell, cancellationToken);
                AutoInventory(request.Context.MQ2, request.Context.TLO.Cursor);
            }

            var actualDrinkCount = allMyInv
                .Where(i => i.NoRent && i.IsDrinkable())
                .Sum(i => i.StackCount);

            if (actualDrinkCount < 10 && TryGetSummonSpell(me, summonDrinkSpellNames, out var drinkSpell))
            {
                await spellCastingService.CastAsync(drinkSpell, cancellationToken);
                AutoInventory(request.Context.MQ2, request.Context.TLO.Cursor);
            }
        }

        private static bool TryGetSummonSpell(CharacterType me, string[] summonSpellNames, out SpellType spell)
        {
            var spellBookSpells = me.SpellBook.Where(i => summonSpellNames.Contains(i.Value.BaseName)).OrderByDescending(i => i.Value.Level);
            var found = spellBookSpells.Any();

            spell = found ? spellBookSpells.First().Value : null;

            return found;
        }

        private void AutoInventory(MQ2 mq2, ItemType cursor)
        {
            if (cursor != null && cursor.NoRent)
            {
                mq2Logger.Log($"Putting [\ag{cursor.Name}\aw] into your inventory");
                mq2.DoCommand("/autoinv");
            }
        }
    }
}
