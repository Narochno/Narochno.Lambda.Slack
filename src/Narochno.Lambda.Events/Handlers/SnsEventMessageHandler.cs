using System;
using System.Collections.Generic;
using Amazon.Lambda.Core;
using Amazon.Lambda.SNSEvents;
using Narochno.Primitives;
using Newtonsoft.Json;

namespace Narochno.Lambda.Events.Handlers
{
    public class SnsEventMessageHandler<TOutput> : BaseMessageHandler<SNSEvent, TOutput> , IEventProcessor<string, TOutput>
        where TOutput : class
    {
        public SnsEventMessageHandler(List<IEventProcessor<SNSEvent, TOutput>> builders)
            : base(builders)
        {
        }

        public Optional<TOutput> TryBuild(string json, ILambdaContext context)
        {
            SNSEvent input;
            try
            {
                input = JsonConvert.DeserializeObject<SNSEvent>(json);
            }
            catch (Exception)
            {
                return null;
            }
            return TryHandle(input, context);
        }
    }
}