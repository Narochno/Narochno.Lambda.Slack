using Amazon.Lambda.Core;
using Narochno.Slack.Entities;

namespace Narochno.Lambda.Slack.Handlers
{
    public interface IMessageHandler<T>
    {
        Message Build(T input, ILambdaContext context);
    }
}