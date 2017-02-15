using System;
using System.Collections.Generic;
using Amazon.Lambda.Core;
using Amazon.Lambda.SNSEvents;
using Narochno.Lambda.Slack.MessageBuilders;
using Narochno.Primitives;
using Narochno.Slack.Entities;
using Newtonsoft.Json;

namespace Narochno.Lambda.Slack.Handlers
{
    public class SnsEventMessageHandler : BaseMessageHandler<SNSEvent>, ISlackMessageBuilder<string>
    {
        public SnsEventMessageHandler(List<ISlackMessageBuilder<SNSEvent>> builders) : base(builders)
        {
        }

        protected override string BuildUnknownMessage(SNSEvent input)
        {
            return JsonConvert.SerializeObject(input.Records[0].Sns.Message);
        }

        public Optional<Message> TryBuild(string json, ILambdaContext context)
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
            return Build(input, context);
        }
    }
}