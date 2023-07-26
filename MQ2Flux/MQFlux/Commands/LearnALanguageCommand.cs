using MQFlux.Behaviors;
using MQFlux.Core;
using MQFlux.Models;
using MQFlux.Services;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Commands
{
    public class LearnALanguageCommand :
        PCCommand,
        ICharacterConfigRequest,
        IGroupedRequest,
        IConsciousRequest
    {
        public IContext Context { get; set; }
        public CharacterConfigSection Character { get; set; }
        public FluxConfig Config { get; set; }
    }

    public class LearnALanguageCommandHandler : PCCommandHandler<LearnALanguageCommand>
    {
        private readonly IChatHistory chatHistory;

        public LearnALanguageCommandHandler(IChatHistory chatHistory)
        {
            this.chatHistory = chatHistory;
        }

        public override Task<CommandResponse<bool>> Handle(LearnALanguageCommand request, CancellationToken cancellationToken)
        {
            if (!request.Character.AutoLearnLanguages.GetValueOrDefault(false))
            {
                return CommandResponse.FromResultTask(false);
            }

            var me = request.Context.TLO.Me;

            if (!me.Grouped)
            {
                return CommandResponse.FromResultTask(false);
            }

            // Assume the /language help command returns only languages you have 1 or more points in and in the same order
            // since it cant retrieve the language number for the character as far as I can tell.
            var languageSkills = me.LanguageSkills.Where(i => i.Value > 0);
            string language = null;
            int? languageNumber = null;

            for (int i = 0; i < languageSkills.Count(); i++)
            {
                var item = languageSkills.ElementAt(i);

                if (item.Value < 100)
                {
                    language = item.Key;
                    languageNumber = i + 1;

                    break;
                }
            }

            if (languageNumber.HasValue)
            {
                Practise(request, language, languageNumber.Value);

                return CommandResponse.FromResultTask(true);
            }

            return CommandResponse.FromResultTask(false);
        }

        private void Practise(LearnALanguageCommand request, string language, int languageNumber)
        {
            var mq = request.Context.MQ;
            var message = $"Lets practise {language}. The time is {DateTimeOffset.Now.ToLocalTime()}. One two three four five six seven eight nine ten! Lets do it all again.";

            if (chatHistory.NoSpam(TimeSpan.FromSeconds(1), message))
            {
                mq.DoCommand($"/language {languageNumber}");
                mq.DoCommand($"/g {message}");
            }
        }
    }
}
