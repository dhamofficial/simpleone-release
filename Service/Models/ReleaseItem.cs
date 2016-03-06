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
        public string Version { get; set; }
        public string BuildDate { get; set; }
        public string BuildNumber { get; set; }
        public int Environment { get; set; }
        public int ReleaseType { get; set; }
        public string DBServer { get; set; }
        public string DBName { get; set; }
        public int Location { get; set; }
        public int Subscription{ get; set; }
        public string Hostname { get; set; }
        public bool SharedInstance { get; set; }
        public int NumberOfInstances { get; set; }

        //[System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string CustomerName { get; set; }
        //[System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string ReleaseTypeName { get; set; }
        //[System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string EnvironmentName { get; set; }
        //[System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string LocationName { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string SubscriptionName { get; set; }

        public string CloudURL
        {
            get { if (cloudurlcol != null) return String.Join(",", cloudurlcol.Select(x => x.text).ToArray()); else return null; }
            set { if (value != null) cloudurlcol = new List<tagitem>(value.Split(',').Select(s => new tagitem { text=s })); }
        }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public List<tagitem> cloudurlcol { get; set; }

        public string DNSURL
        {
            get { if (dnsurlcol != null) return String.Join(",", dnsurlcol.Select(x => x.text).ToArray()); else return null; }
            set { if (value != null) dnsurlcol = new List<tagitem>(value.Split(',').Select(s => new tagitem { text = s })); }
        }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public List<tagitem> dnsurlcol { get; set; }

        public string MobileURL
        {
            get { if (mobileurlcol != null) return String.Join(",", mobileurlcol.Select(x => x.text).ToArray()); else return null; }
            set { if (value != null) mobileurlcol = new List<tagitem>(value.Split(',').Select(s => new tagitem { text = s })); }
        }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public List<tagitem> mobileurlcol { get; set; }

        public string Modules
        {
            get { if (modulescol != null) return String.Join(",", modulescol.Select(x => x.text).ToArray()); else return null; }
            set { if (value != null) modulescol = new List<tagitem>(value.Split(',').Select(s => new tagitem { text = s })); }
        }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public List<tagitem> modulescol { get; set; }


        public string VMSize
        {
            get { if (vmsizelcol != null) return String.Join(",", vmsizelcol.Select(x => x.text).ToArray()); else return null; }
            set { if (value != null) vmsizelcol = new List<tagitem>(value.Split(',').Select(s => new tagitem { text = s })); }
        }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public List<tagitem> vmsizelcol { get; set; }
    }

    public class tagitem
    {
        public string text { get; set; }
        public string id { get; set; }
    }
}