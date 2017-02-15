using Amazon.Lambda.Core;
using Narochno.Lambda.Slack.Events;
using Narochno.Slack.Entities;
using Newtonsoft.Json;

namespace Narochno.Lambda.Slack.MessageBuilders
{
    public class EcsEventMessageBuilder : JsonMessageBuilder<CloudWatchEvent<EcsEventDetail>>
    {
        protected override Message Build(CloudWatchEvent<EcsEventDetail> input, ILambdaContext context)
        {
            return new Message()
            {
                Attachments = new[]
                {
                    new Attachment()
                    {
                        Fallback =$"[{input.DetailType}] [{string.Join(",", input.Resources)}].",
                        Color = "good",
                        Title = input.DetailType,
                        Text =  JsonConvert.SerializeObject(input),
                    }
                }
            };
        }
    }
}