using Amazon.Lambda.Core;
using Narochno.Primitives;
using Narochno.Slack.Entities;

namespace Narochno.Lambda.Slack.Handlers
{
    public interface ISlackMessageBuilder
    {
        Optional<Message> Handle(string json, ILambdaContext context);
    }
}