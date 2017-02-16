using Narochno.Lambda.Events;
using Narochno.Lambda.Slack;
using Narochno.Lambda.Slack.MessageBuilders;
using Narochno.Slack.Entities;

namespace EventTester
{
    public class TestHandler : SlackLambdaEventHandler
    {
        public TestHandler() :
            base(new AmazonEventHandlerBuilder<Message>()
                .AddMessageType<EcsEventMessageBuilder>()
                .AddMessageTypeForSns<CodeCommitEventMessageBuilder>()
                .Build())
        {
        }
    }
}