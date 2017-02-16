using Amazon.Lambda.Core;
using Narochno.Primitives;

namespace Narochno.Lambda.Events
{
    public interface IEventProcessor<TInput, TOutput>
        where TOutput : class
    {
        Optional<TOutput> TryBuild(TInput input, ILambdaContext context);
    }
}