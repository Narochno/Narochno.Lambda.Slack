using System;
using Amazon.Lambda.Core;
using Amazon.Lambda.SNSEvents;
using Narochno.Primitives;
using Narochno.Slack.Entities;

namespace Narochno.Lambda.Slack.MessageBuilders
{
    public sealed class SnsMessageBuilder : ISlackMessageBuilder<SNSEvent>
    {
        private readonly ISlackMessageBuilder<string> internalBuilder;

        public SnsMessageBuilder(ISlackMessageBuilder<string> internalBuilder)
        {
            this.internalBuilder = internalBuilder;
        }

        public Optional<Message> TryBuild(SNSEvent snsEvent, ILambdaContext context)
        {
            try
            {
                return internalBuilder.TryBuild(snsEvent.Records[0].Sns.Message, context);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}