using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.Json;
using Narochno.Lambda.Slack.Handlers;
using Narochno.Primitives;
using Narochno.Slack;
using Narochno.Slack.Entities;

namespace Narochno.Lambda.Slack
{
    public abstract class LambdaEventHandler
    {
        private readonly SlackClient slackClient;
        private readonly List<ISlackMessageBuilder> builders;

        protected LambdaEventHandler(IEnumerable<ISlackMessageBuilder> builders)
        {
            slackClient = new SlackClient(new SlackConfig()
            {
                WebHookUrl = Environment.GetEnvironmentVariable("slack_webhook_url")
            });
            this.builders = builders.ToList();
        }


        [LambdaSerializer(typeof(JsonSerializer))]
        public async Task Handle(Stream stream, ILambdaContext context)
        {
            var json = await new StreamReader(stream).ReadToEndAsync();
            Optional<Message> message = null;
            foreach (var slackMessageBuilder in builders)
            {
                message = slackMessageBuilder.Handle(json, context);
                if (message.HasValue)
                {
                    break;
                }
            }
            if (message.HasNoValue)
            {
                message = new Message()
                {
                    Attachments = new[]
                    {
                        new Attachment()
                        {
                            Fallback ="Unknown message",
                            Color = "good",
                            Title =  "Unknown message",
                            Text =  json
                        }
                    }
                };
            }
            await slackClient.PostMessage(message.Value, CancellationToken.None);
        }
    }
}