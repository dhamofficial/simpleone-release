using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.Models
{
    public class paramproperty
    {
        public string action { get; set; }
        public int Customer { get; set; }
        public int Environment { get; set; }
        public int ReleaseType { get; set; }
        public string BuildDate { get; set; }
    }

    public class Masters
    {
        public List<Customer> Customers { get; set; }
        public List<Environment> Environments { get; set; }
        public List<ReleaseType> ReleaseTypes { get; set; }
        public List<Location> Locations { get; set; }
        public List<Subscription> Subscriptions { get; set; }
    }

    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public bool Active { get; set; }
    }

    public class Environment
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }

    public class ReleaseType
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }

    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }

    public class Subscription
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
}