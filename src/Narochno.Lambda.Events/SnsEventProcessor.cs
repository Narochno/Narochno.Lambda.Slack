using System;
using Amazon.Lambda.Core;
using Amazon.Lambda.SNSEvents;
using Narochno.Primitives;

namespace Narochno.Lambda.Events
{
    public sealed class SnsEventProcessor<TOutput> : IEventProcessor<SNSEvent, TOutput>
        where TOutput : class
    {
        private readonly IEventProcessor<string, TOutput> eventProcessor;

        public SnsEventProcessor(IEventProcessor<string, TOutput> eventProcessor)
        {
            this.eventProcessor = eventProcessor;
        }

        public Optional<TOutput> TryBuild(SNSEvent snsEvent, ILambdaContext context)
        {
            try
            {
                return eventProcessor.TryBuild(snsEvent.Records[0].Sns.Message, context);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}