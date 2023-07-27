using MQ2DotNet.MQ2API.DataTypes;
using MQFlux.Behaviors;
using MQFlux.Core;
using MQFlux.Extensions;
using MQFlux.Models;
using MQFlux.Services;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Commands
{
    public class SummonFoodAndDrinkCommand :
        PCCommand,
        ICharacterConfigRequest,
        IConsciousRequest,
        INotInCombatRequest,
        IStandingStillRequest,
        ICasterRequest,
        INotCastingRequest,
        INoItemOnCursorRequest,
        INotFeignedDeathRequest,
        IIdleTimeRequest
    {
        public bool AllowBard => false;
        public CharacterConfig Character { get; set; }
        public FluxConfig Config { get; set; }
        public IContext Context { get; set; }
        public TimeSpan IdleTime => TimeSpan.FromSeconds(1);
    }

    public class SummonFoodAndDrinkCommandHandler : PCCommandHandler<SummonFoodAndDrinkCommand>
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
        private readonly IMacroService macroService;

        public SummonFoodAndDrinkCommandHandler(IItemService itemService, ISpellCastingService spellCastingService, IMacroService macroService)
        {
            this.itemService = itemService;
            this.spellCastingService = spellCastingService;
            this.macroService = macroService;
        }

        public override async Task<CommandResponse<bool>> Handle(SummonFoodAndDrinkCommand request, CancellationToken cancellationToken)
        {
            if (!request.Character.AutoSummonFoodAndDrink.GetValueOrDefault(false))
            {
                return CommandResponse.FromResult(false);
            }

            var me = request.Context.TLO.Me;
            var allMyInv = me.Bags.Flatten();
            var actualFoodCount = allMyInv
                .Where(i => i.NoRent && i.IsEdible())
                .Sum(i => i.StackCount);

            if (actualFoodCount < 10 && TryGetSummonSpell(me, summonFoodSpellNames, out var foodSpell))
            {
                try
                {
                    await macroService.Pause(cancellationToken);

                    if (await spellCastingService.Cast(foodSpell, waitForSpellReady: true, cancellationToken))
                    {
                        await itemService.AutoInventory(cursor => cursor != null && cursor.NoRent, cancellationToken);

                        return CommandResponse.FromResult(true);
                    }
                }
                finally
                {
                    macroService.Resume();
                }
            }

            var actualDrinkCount = allMyInv
                .Where(i => i.NoRent && i.IsDrinkable())
                .Sum(i => i.StackCount);

            if (actualDrinkCount < 10 && TryGetSummonSpell(me, summonDrinkSpellNames, out var drinkSpell))
            {
                try
                {
                    await macroService.Pause(cancellationToken);

                    if (await spellCastingService.Cast(drinkSpell, waitForSpellReady: true, cancellationToken))
                    {
                        await itemService.AutoInventory(cursor => cursor != null && cursor.NoRent, cancellationToken);

                        return CommandResponse.FromResult(true);
                    }
                }
                finally
                {
                    macroService.Resume();
                }
            }

            return CommandResponse.FromResult(false);
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
