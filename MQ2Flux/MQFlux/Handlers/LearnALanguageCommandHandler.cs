using MediatR;
using MQFlux.Commands;
using MQFlux.Services;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Handlers
{
    public class LearnALanguageCommandHandler : IRequestHandler<LearnALanguageCommand>
    {
        private readonly IChatHistory chatHistory;

        public LearnALanguageCommandHandler(IChatHistory chatHistory)
        {
            this.chatHistory = chatHistory;
        }

        public Task Handle(LearnALanguageCommand request, CancellationToken cancellationToken)
        {
            if (!request.Character.AutoLearnLanguages.GetValueOrDefault(false))
            {
                return Task.CompletedTask;
            }

            var me = request.Context.TLO.Me;

            if (!me.Grouped)
            {
                return Task.CompletedTask;
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
                var mq = request.Context.MQ;
                var message = $"Lets practise {language}. The time is {DateTimeOffset.Now.ToLocalTime()}. One two three four five six seven eight nine ten! Lets do it all again.";

                if (chatHistory.NoSpam(TimeSpan.FromSeconds(1), message))
                {
                    mq.DoCommand($"/language {languageNumber}");
                    mq.DoCommand($"/g {message}");
                }
            }

            return Task.CompletedTask;
        }
    }
}
