using System.Collections.Generic;
using Amazon.Lambda.Core;
using Narochno.Lambda.Slack.MessageBuilders;
using Narochno.Primitives;
using Narochno.Slack.Entities;

namespace Narochno.Lambda.Slack.Handlers
{
    public abstract class BaseMessageHandler<T> : IMessageHandler<T>
    {
        private readonly List<ISlackMessageBuilder<T>> builders;

        protected BaseMessageHandler(List<ISlackMessageBuilder<T>> builders)
        {
            this.builders = builders;
        }

        protected abstract string BuildUnknownMessage(T input);

        public Message Build(T input, ILambdaContext context)
        {
            Optional<Message> message = null;
            foreach (var slackMessageBuilder in builders)
            {
                message = slackMessageBuilder.TryBuild(input, context);
                if (message.HasValue)
                {
                    context.Logger.Log(slackMessageBuilder.GetType() + " fits");
                    break;
                }
                context.Logger.Log(slackMessageBuilder.GetType() + " doesn't fit");
            }
            if (message.HasNoValue)
            {
                context.Logger.Log("fallback fits");
                message = new Message()
                {
                    Attachments = new[]
                    {
                        new Attachment()
                        {
                            Fallback ="Unknown message",
                            Color = "good",
                            Title =  "Unknown message",
                            Text =  BuildUnknownMessage(input)
                        }
                    }
                };
            }
            return message.Value;
        }
    }
}