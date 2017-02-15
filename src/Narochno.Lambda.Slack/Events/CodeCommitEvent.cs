using System;

namespace Narochno.Lambda.Slack.Events
{
    public class CodeCommitEvent
    {
        public CodeCommitEventRecord[] Records { get; set; }
    }

    public class CodeCommitEventRecord
    {
        public CodeCommit CodeCommit { get; set; }

        public string AwsRegion { get; set; }
        public string EventId { get; set; }
        public string EventName { get; set; }
        public int EventPartNumber { get; set; }
        public string EventSource { get; set; }
        public DateTime EventTime { get; set; }
        public string EventTriggerConfigId { get; set; }
        public string EventTriggerName { get; set; }
        public string EventVersion { get; set; }
        public string UserIdentityArn { get; set; }
    }

    public class CodeCommit
    {
        public Reference[] References { get; set; }
    }

    public class Reference
    {
        public string Commit { get; set; }
        public string Ref { get; set; }
    }
}