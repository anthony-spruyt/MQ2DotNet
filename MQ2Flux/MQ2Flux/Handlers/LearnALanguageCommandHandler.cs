using MediatR;
using MQ2Flux.Commands;
using MQ2Flux.Services;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MQ2Flux.Handlers
{
    public class LearnALanguageCommandHandler : IRequestHandler<LearnALanguageCommand>
    {
        private readonly IMQ2ChatHistory chatHistory;

        public LearnALanguageCommandHandler(IMQ2ChatHistory chatHistory)
        {
            this.chatHistory = chatHistory;
        }

        public Task Handle(LearnALanguageCommand request, CancellationToken cancellationToken)
        {
            var me = request.Context.TLO.Me;

            if (!me.Combat && me.Grouped)
            {
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
                    var mq2 = request.Context.MQ2;
                    var message = $"Lets practise {language}. The time is {DateTimeOffset.Now.ToLocalTime()}. One two three four five six seven eight nine ten! Lets do it all again.";

                    if (chatHistory.NoSpam(TimeSpan.FromSeconds(1), message))
                    {
                        mq2.DoCommand($"/language {languageNumber}");
                        mq2.DoCommand($"/g {message}");
                    }
                }
            }

            return Task.CompletedTask;
        }
    }
}
