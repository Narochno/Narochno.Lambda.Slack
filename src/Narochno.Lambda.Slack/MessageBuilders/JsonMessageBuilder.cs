using System;
using Amazon.Lambda.Core;
using Narochno.Primitives;
using Narochno.Slack.Entities;
using Newtonsoft.Json;

namespace Narochno.Lambda.Slack.MessageBuilders
{
    public abstract class JsonMessageBuilder<T> : ISlackMessageBuilder<string>
    {
        public Optional<Message> TryBuild(string json, ILambdaContext context)
        {
            try
            {
                T input = JsonConvert.DeserializeObject<T>(json);
                return Build(input, context);
            }
            catch (Exception)
            {
                return null;
            }
        }

        protected abstract Message Build(T input, ILambdaContext context);
    }
}