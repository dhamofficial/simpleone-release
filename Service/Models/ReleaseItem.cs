using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.Models
{
    public class ReleaseItem
    {
        public int Id { get; set; }
        public int Customer { get; set; }
        public string CustomerName { get; set; }
        public string Version { get; set; }
        public string BuildDate { get; set; }
        public string BuildNumber { get; set; }
        public int Environment { get; set; }
        public int ReleaseType { get; set; }
        public string ReleaseTypeName { get; set; }

        public string CloudURL { get; set; }
        public string DNSURL { get; set; }
        public string MobileURL { get; set; }
        public string DBServer { get; set; }
        public string DBName { get; set; }
        public string Modules { get; set; }
        public int Location { get; set; }
        public int Subscription{ get; set; }
        public string Hostname { get; set; }
        public string VMSize { get; set; }
        public bool SharedInstance { get; set; }
        public int NumberOfInstances { get; set; }
        public string EnvironmentName { get; set; }
        public string LocationName { get; set; }
        public string SubscriptionName { get; set; }
    }
}