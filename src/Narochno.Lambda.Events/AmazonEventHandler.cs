using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Narochno.Lambda.Events.Handlers;
using Narochno.Primitives;

namespace Narochno.Lambda.Events
{
    public class AmazonEventHandler<TOutput> : BaseMessageHandler<string, TOutput>
        where TOutput : class
    {
        public AmazonEventHandler(List<IEventProcessor<string, TOutput>> builders)
            : base(builders)
        {
        }

        public async Task<Optional<TOutput>> ProcessEvent(Stream stream, ILambdaContext context)
        {
            var json = await new StreamReader(stream).ReadToEndAsync();
            return TryHandle(json, context);
        }
    }
}