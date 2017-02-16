using System.Collections.Generic;
using Amazon.Lambda.Core;
using Narochno.Primitives;

namespace Narochno.Lambda.Events.Handlers
{
    public abstract class BaseMessageHandler<TInput, TOutput> : IMessageHandler<TInput, TOutput>
        where TOutput : class
    {
        private readonly List<IEventProcessor<TInput, TOutput>> builders;

        protected BaseMessageHandler(List<IEventProcessor<TInput, TOutput>> builders)
        {
            this.builders = builders;
        }

        protected virtual Optional<TOutput> UnhandledMessage(TInput input)
        {
            return null;
        }

        public Optional<TOutput> TryHandle(TInput input, ILambdaContext context)
        {
            Optional<TOutput> message = null;
            foreach (var slackMessageBuilder in builders)
            {
                message = slackMessageBuilder.TryBuild(input, context);
                if (message.HasValue)
                {
                    break;
                }
            }
            if (message.HasNoValue)
            {
                return UnhandledMessage(input);
            }
            return message;
        }
    }
}