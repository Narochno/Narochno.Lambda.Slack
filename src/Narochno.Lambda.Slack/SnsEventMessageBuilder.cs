using Amazon.Lambda.Core;
using Amazon.Lambda.SNSEvents;
using Narochno.Lambda.Events;
using Narochno.Slack.Entities;

namespace Narochno.Lambda.Slack
{
    public class SnsEventMessageBuilder : JsonEventProcessor<SNSEvent, Message>
    {
        protected override Message Build(SNSEvent input, ILambdaContext context)
        {
            return new Message()
            {
                Attachments = new[]
                {
                    new Attachment()
                    {
                        Fallback = input.Records[0].Sns.Subject,
                        Color = "good",
                        Title = input.Records[0].Sns.Subject,
                        Text = input.Records[0].Sns.Message
                    }
                }
            };
        }
    }
}