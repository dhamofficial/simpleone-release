using Newtonsoft.Json;
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

        // GET: api/GetMasters
        public IHttpActionResult GetMasters()
        {
            var master = db.GetMasters();
            return Json(master);
        }

        [HttpGet]
        public IHttpActionResult GetMasters(string action)
        {
            if (string.IsNullOrEmpty(action) == false)
            {
                var param = JsonConvert.DeserializeObject<paramproperty>(action);

                if(param.action== "filterlist")
                {
                    var releases = db.GetRecentReleases(param);
                    return Json(releases);
                }
                else
                {
                    var releases = db.GetRecentReleases();
                    return Json(releases);
                }
            }
            else
            {
                var releases = db.GetRecentReleases();
                return Json(releases);
            }
        }

        [HttpPost]
        public IHttpActionResult PostItem(ReleaseItem item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (item.Customer > 0)
            {
                db.ReleaseItems.Add(item);
                db.SaveChanges();

                return Ok(new { Action = "Success" });
            }
            else
            {
                return BadRequest("Invalid Parameters/Insufficient Parameters");
            }
        }
    }
}
