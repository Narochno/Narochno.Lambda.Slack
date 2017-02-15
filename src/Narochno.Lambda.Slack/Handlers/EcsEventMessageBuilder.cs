using Amazon.Lambda.Core;
using Narochno.Lambda.Slack.Events;
using Narochno.Slack.Entities;
using Newtonsoft.Json;

namespace Narochno.Lambda.Slack.Handlers
{
    public class EcsEventMessageBuilder : SlackMessageBuilder<CloudWatchEvent<EcsEventDetail>>
    {
        protected override Message Handle(CloudWatchEvent<EcsEventDetail> input, ILambdaContext context)
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