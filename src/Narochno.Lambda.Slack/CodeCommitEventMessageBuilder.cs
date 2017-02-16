using System.Linq;
using Amazon.Lambda.Core;
using Amazon.Lambda.SNSEvents;
using Narochno.Lambda.Events;
using Narochno.Lambda.Events.Types;
using Narochno.Slack.Entities;
using Newtonsoft.Json;

namespace Narochno.Lambda.Slack
{
    public class CodeCommitEventMessageBuilder : JsonEventProcessor<SNSEvent, Message>
    {
        protected override Message Build(SNSEvent input, ILambdaContext context)
        {
            var codeComment = JsonConvert.DeserializeObject<CodeCommitEvent>(input.Records[0].Sns.Message);

            var index = codeComment.Records[0].UserIdentityArn.LastIndexOf(":");
            string message = codeComment.Records[0].UserIdentityArn.Substring(index + 1);
            message += " - " +
                string.Join("\n",
                codeComment.Records[0].CodeCommit.References.Select(x => x.Commit + " on ref " + x.Ref));
            return new Message()
            {
                Attachments = new[]
                {
                    new Attachment()
                    {
                        Fallback = input.Records[0].Sns.Subject,
                        Color = "good",
                        Title = input.Records[0].Sns.Subject,
                        Text = message
                    }
                }
            };
        }
    }
}