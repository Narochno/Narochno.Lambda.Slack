using System.Collections.Generic;
using System.Linq;
using Amazon.Lambda.SNSEvents;
using Narochno.Lambda.Slack.Handlers;
using Narochno.Lambda.Slack.MessageBuilders;

namespace Narochno.Lambda.Slack
{
    public class HandlerBuilder
    {
        private readonly List<ISlackMessageBuilder<string>> jsonBuilders = new List<ISlackMessageBuilder<string>>();
        private readonly List<ISlackMessageBuilder<string>> snsBuilders = new List<ISlackMessageBuilder<string>>();

        public HandlerBuilder AddMessageType<T>()
            where T : ISlackMessageBuilder<string>, new()
        {
            jsonBuilders.Add(new T());
            return this;
        }

        public HandlerBuilder AddMessageTypeForSNS<T>()
            where T : ISlackMessageBuilder<string>, new()
        {
            snsBuilders.Add(new T());
            return this;
        }

        public GenericMessageHandler Build()
        {
            var builders = jsonBuilders.ToList();
            if (snsBuilders.Any())
            {
                builders.Add(new SnsEventMessageHandler(snsBuilders.Select(x => new SnsMessageBuilder(x)).ToList<ISlackMessageBuilder<SNSEvent>>()));
            }
            return new GenericMessageHandler(builders);
        }
    }
}