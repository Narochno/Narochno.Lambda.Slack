using System;
using Amazon.Lambda.Core;
using Narochno.Primitives;
using Narochno.Slack.Entities;
using Newtonsoft.Json;

namespace Narochno.Lambda.Slack.Handlers
{
    public abstract class SlackMessageBuilder<T> : ISlackMessageBuilder
    {
        public Optional<Message> Handle(string json, ILambdaContext context)
        {
            T input;
            try
            {
                input = JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception)
            {
                return null;
            }
            return Handle(input, context);
        }

        protected abstract Message Handle(T input, ILambdaContext context);
    }
}