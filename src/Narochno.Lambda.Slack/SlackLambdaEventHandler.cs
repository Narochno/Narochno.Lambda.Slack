using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.Json;
using Narochno.Lambda.Events;
using Narochno.Slack;
using Narochno.Slack.Entities;

namespace Narochno.Lambda.Slack
{
    public abstract class SlackLambdaEventHandler
    {
        private readonly SlackClient slackClient;
        private readonly AmazonEventHandler<Message> handler;

        protected SlackLambdaEventHandler(AmazonEventHandler<Message> handler)
        {
            slackClient = new SlackClient(new SlackConfig()
            {
                WebHookUrl = Environment.GetEnvironmentVariable("slack_webhook_url")
            });
            this.handler = handler;
        }


        [LambdaSerializer(typeof(JsonSerializer))]
        public async Task Handle(Stream stream, ILambdaContext context)
        {
            var json = await new StreamReader(stream).ReadToEndAsync();
            var message = handler.TryHandle(json, context);
            if (message.HasValue)
            {
                await slackClient.PostMessage(message.Value, CancellationToken.None);
            }
        }
    }
}