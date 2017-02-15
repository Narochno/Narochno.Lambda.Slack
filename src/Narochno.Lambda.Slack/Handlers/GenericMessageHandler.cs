using System.Collections.Generic;
using Narochno.Lambda.Slack.MessageBuilders;

namespace Narochno.Lambda.Slack.Handlers
{
    public class GenericMessageHandler : BaseMessageHandler<string>
    {
        public GenericMessageHandler(List<ISlackMessageBuilder<string>> builders) : base(builders)
        {
        }

        protected override string BuildUnknownMessage(string input)
        {
            return input;
        }
    }
}