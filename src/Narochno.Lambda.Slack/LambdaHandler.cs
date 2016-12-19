using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.Lambda.SNSEvents;
using Newtonsoft.Json;
using JsonSerializer = Amazon.Lambda.Serialization.Json.JsonSerializer;

namespace Narochno.Lambda.Slack
{
    public class LambdaHandler
    {
        private readonly HttpClient httpClient = new HttpClient();

        [LambdaSerializer(typeof(JsonSerializer))]
        public async Task EcsCloudWatch(Stream stream, ILambdaContext context)
        {
            var webhookUri = Environment.GetEnvironmentVariable("slack_webhook_url");

            var json = await new StreamReader(stream).ReadToEndAsync();
            
            try
            {
                var input = JsonConvert.DeserializeObject<SNSEvent>(json);
                var obj = new
                {
                    attachments = new[]
                    {
                        new
                        {
                            fallback = $"[{input.Records[0].Sns.Subject}] [{input.Records[0].Sns.TopicArn}].",
                            color = "good",
                            title = input.Records[0].Sns.Subject,
                            text = JsonConvert.SerializeObject(input.Records[0].Sns.Message),
                        }
                    }
                };
                await httpClient.PostAsync(webhookUri, new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json"));
                return;
            }
            catch (Exception e)
            {
                context.Logger.Log(e.ToString());
            }
            try
            {
                var input = JsonConvert.DeserializeObject<CloudWatchEvent<EcsEventDetail>>(json);
                var obj = new
                {
                    attachments = new[]
                    {
                        new
                        {
                            fallback = $"[{input.DetailType}] [{string.Join(",", input.Resources)}].",
                            color = "good",
                            title = input.DetailType,
                            text = JsonConvert.SerializeObject(input),
                        }
                    }
                };
                await httpClient.PostAsync(webhookUri, new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json"));
                return;
            }
            catch (Exception e)
            {
                context.Logger.Log(e.ToString());
            }
            var obj2 = new
            {
                attachments = new[]
                {
                        new
                        {
                            fallback = "Unknown message",
                            color = "good",
                            title = "Unknown message",
                            text = json
                        }
                    }
            };
            await httpClient.PostAsync(webhookUri, new StringContent(JsonConvert.SerializeObject(obj2), Encoding.UTF8, "application/json"));
        }
    }
}