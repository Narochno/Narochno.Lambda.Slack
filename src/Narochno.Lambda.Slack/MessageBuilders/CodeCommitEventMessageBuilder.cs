using System.Linq;
using Amazon.Lambda.Core;
using Narochno.Lambda.Events;
using Narochno.Lambda.Events.Types;
using Narochno.Slack.Entities;
using Newtonsoft.Json;

namespace Narochno.Lambda.Slack.MessageBuilders
{
    public class CodeCommitEventMessageBuilder : JsonEventProcessor<CodeCommitEvent, Message>
    {
        protected override Message Build(CodeCommitEvent input, ILambdaContext context)
        {
            string message = string.Join("\n",
                input.Records[0].CodeCommit.References.Select(x => x.Commit + " on ref " + x.Ref));
            return new Message()
            {
                Attachments = new[]
                {
                    new Attachment()
                    {
                        Fallback = JsonConvert.SerializeObject(input),
                        Color = "good",
                        Title = input.Records[0].EventName,
                        Text = message
                    }
                }
            };
        }
    }
}