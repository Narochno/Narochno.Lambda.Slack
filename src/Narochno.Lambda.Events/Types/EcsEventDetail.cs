using Newtonsoft.Json.Linq;

namespace Narochno.Lambda.Events.Types
{
    public class EcsEventDetail
    {
        public bool AgentConnected { get; set; }
        public JArray Attributes { get; set; }
        public string ClusterArn { get; set; }
        public string ContainerInstanceArn { get; set; }
        public string Ec2InstanceId { get; set; }
        public JArray RegisteredResources { get; set; }
        public JArray RemainingResources { get; set; }
        public string Status { get; set; }
        public int Version { get; set; }
        public string UpdatedAt { get; set; }
    }
}