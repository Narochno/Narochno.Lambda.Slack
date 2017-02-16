using Amazon.Lambda.Core;
using Narochno.Primitives;

namespace Narochno.Lambda.Events.Handlers
{
    public interface IMessageHandler<TInput, TOutput>
        where TOutput : class
    {
        Optional<TOutput> TryHandle(TInput input, ILambdaContext context);
    }
}