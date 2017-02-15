using Narochno.Lambda.Slack;
using Narochno.Lambda.Slack.Handlers;

namespace EventTester
{
    public class TestHandler : LambdaEventHandler
    {
        public TestHandler() :
            base(new ISlackMessageBuilder[]
            {
                new SNSEventMessageBuilder(),
                new EcsEventMessageBuilder(),
            })
        {
        }
    }
}