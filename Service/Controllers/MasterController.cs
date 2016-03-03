using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Service.Controllers
{
    public class MasterController : ApiController
    {
        private AppDbContext db = new AppDbContext();

        // GET: api/Customers
        public IHttpActionResult GetMasters()
        {

            var master = new Masters();

            master.Customers = db.Customers.ToList();
            master.Environments = db.Environments.ToList();
            master.Locations = db.Locations.ToList();
            master.ReleaseTypes = db.ReleaseTypes.ToList();


            return Json(master);
        }
    }
}
