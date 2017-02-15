using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.Json;
using Narochno.Lambda.Slack.Handlers;
using Narochno.Slack;

namespace Narochno.Lambda.Slack
{
    public abstract class LambdaEventHandler
    {
        private readonly SlackClient slackClient;
        private readonly GenericMessageHandler messageHandler;


        protected LambdaEventHandler(HandlerBuilder builder)
        {
            slackClient = new SlackClient(new SlackConfig()
            {
                WebHookUrl = Environment.GetEnvironmentVariable("slack_webhook_url")
            });
            messageHandler = builder.Build();
        }


        [LambdaSerializer(typeof(JsonSerializer))]
        public async Task Handle(Stream stream, ILambdaContext context)
        {
            var json = await new StreamReader(stream).ReadToEndAsync();
            var message = messageHandler.Build(json, context);
            await slackClient.PostMessage(message, CancellationToken.None);
        }
    }
}