using System;
using Amazon.Lambda.Core;
using Narochno.Primitives;
using Newtonsoft.Json;

namespace Narochno.Lambda.Events
{
    public abstract class JsonEventProcessor<TInput, TOutput> : IEventProcessor<string, TOutput>
        where TOutput : class
    {
        public Optional<TOutput> TryBuild(string json, ILambdaContext context)
        {
            try
            {
                TInput input = JsonConvert.DeserializeObject<TInput>(json);
                return Build(input, context);
            }
            catch (Exception)
            {
                return null;
            }
        }

        protected abstract TOutput Build(TInput input, ILambdaContext context);
    }
}