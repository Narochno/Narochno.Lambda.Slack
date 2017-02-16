using System.Collections.Generic;
using System.Linq;
using Amazon.Lambda.SNSEvents;
using Narochno.Lambda.Events.Handlers;

namespace Narochno.Lambda.Events
{
    public class AmazonEventHandlerBuilder<TOutput>
        where TOutput : class
    {
        private readonly List<IEventProcessor<string, TOutput>> jsonBuilders = new List<IEventProcessor<string, TOutput>>();
        private readonly List<IEventProcessor<string, TOutput>> snsBuilders = new List<IEventProcessor<string, TOutput>>();

        public AmazonEventHandlerBuilder<TOutput> AddMessageType<T>()
            where T : IEventProcessor<string, TOutput>, new()
        {
            jsonBuilders.Add(new T());
            return this;
        }

        public AmazonEventHandlerBuilder<TOutput> AddMessageTypeForSns<T>()
            where T : IEventProcessor<string, TOutput>, new()
        {
            snsBuilders.Add(new T());
            return this;
        }

        public AmazonEventHandler<TOutput> Build()
        {
            var builders = jsonBuilders.ToList();
            if (snsBuilders.Any())
            {
                builders.Add(new SnsEventMessageHandler<TOutput>(snsBuilders.Select(x => new SnsEventProcessor<TOutput>(x)).ToList<IEventProcessor<SNSEvent, TOutput>>()));
            }
            return new AmazonEventHandler<TOutput>(builders);
        }
    }
}