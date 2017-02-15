using Amazon.Lambda.Core;
using Narochno.Primitives;
using Narochno.Slack.Entities;

namespace Narochno.Lambda.Slack.MessageBuilders
{
    public interface ISlackMessageBuilder<T>
    {
        Optional<Message> TryBuild(T input, ILambdaContext context);
    }
}