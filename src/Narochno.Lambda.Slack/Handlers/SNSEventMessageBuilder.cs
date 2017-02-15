using Amazon.Lambda.Core;
using Amazon.Lambda.SNSEvents;
using Narochno.Slack.Entities;
using Newtonsoft.Json;

namespace Narochno.Lambda.Slack.Handlers
{
    public class SNSEventMessageBuilder : SlackMessageBuilder<SNSEvent>
    {
        protected override Message Handle(SNSEvent input, ILambdaContext context)
        {
            return new Message()
            {
                Attachments = new[]
                {
                    new Attachment()
                    {
                        Fallback = $"[{input.Records[0].Sns.Subject}] [{input.Records[0].Sns.TopicArn}].",
                        Color = "good",
                        Title = input.Records[0].Sns.Subject,
                        Text = JsonConvert.SerializeObject(input.Records[0].Sns.Message),
                    }
                }
            };
        }
    }
}