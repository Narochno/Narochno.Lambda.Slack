using Amazon.Lambda.Core;
using Narochno.Lambda.Events;
using Narochno.Lambda.Events.Types;
using Narochno.Slack.Entities;
using Newtonsoft.Json;

namespace Narochno.Lambda.Slack
{
    public class EcsEventMessageBuilder : JsonEventProcessor<CloudWatchEvent<EcsEventDetail>, Message>
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