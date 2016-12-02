using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Newtonsoft.Json;
using JsonSerializer = Amazon.Lambda.Serialization.Json.JsonSerializer;

namespace Visibility.Lambda.Slack
{
    public class LambdaHandler
    {
        private readonly HttpClient httpClient = new HttpClient();

        [LambdaSerializer(typeof(JsonSerializer))]
        public async Task EcsCloudWatch(CloudWatchEvent<EcsEventDetail> input, ILambdaContext context)
        {

            Console.WriteLine("Function name: " + context.FunctionName);
            Console.WriteLine("RemainingTime: " + context.RemainingTime);
            await Task.Delay(TimeSpan.FromSeconds(0.42));
            Console.WriteLine("RemainingTime after sleep: " + context.RemainingTime);

            var webhookUri = Environment.GetEnvironmentVariable("slack_webhook_url");

            var obj = new
            {
                attachments = new[]
                {
                    new
                    {
                        fallback = $"[{input.DetailType}] [{input.Detail.Status}].",
                        color = "good",
                        title = input.DetailType,
                        text = JsonConvert.SerializeObject(input),
                    }
                }
            };
            httpClient.PostAsync(webhookUri, new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json")).Wait();

        }
    }
}