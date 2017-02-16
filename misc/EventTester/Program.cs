using System;
using Amazon.Lambda.SNSEvents;
using Newtonsoft.Json;

namespace EventTester
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var json =
                "{\"Records\":[{\"awsRegion\":\"eu-west-1\",\"codecommit\":{\"references\":[{\"commit\":\"8c200289ea6dd9459dbf35e7d462baa22ac02c5f\",\"ref\":\"refs/heads/dev\"}]},\"eventId\":\"cc5dd84e-1fd4-47be-bb7e-9f5104732758\",\"eventName\":\"TriggerEventTest\",\"eventPartNumber\":1,\"eventSource\":\"aws:codecommit\",\"eventSourceARN\":\"arn:aws:codecommit:eu-west-1:807103944602:Visibility-API\",\"eventTime\":\"2017-02-15T14:20:25.189+0000\",\"eventTotalParts\":1,\"eventTriggerConfigId\":\"cc5dd84e-1fd4-47be-bb7e-9f5104732758\",\"eventTriggerName\":\"Slack\",\"eventVersion\":\"1.0\",\"userIdentityARN\":\"arn:aws:iam::807103944602:user/ahathcock\"}]}";

            var x = JsonConvert.DeserializeObject<SNSEvent>(json);
            Console.WriteLine(x.Records[0].EventSource);
        }
    }
}
