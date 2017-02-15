using Narochno.Lambda.Slack;
using Narochno.Lambda.Slack.MessageBuilders;

namespace EventTester
{
    public class TestHandler : LambdaEventHandler
    {
        public TestHandler() :
            base(new HandlerBuilder()
                .AddMessageType<EcsEventMessageBuilder>()
                .AddMessageTypeForSNS<CodeCommitEventMessageBuilder>())
        {
        }
    }
}