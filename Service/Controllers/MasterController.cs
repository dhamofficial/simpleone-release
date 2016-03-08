using Newtonsoft.Json;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

                    if (param.Id > 0 && releases.Count>0)
                        return Json(releases[0]);
                    else
                        return Json(releases);
                }
                else
                {
                    var releases = db.GetRecentReleases();
                    var dashboarddata = db.GetDashboardData();

                    var dashboardItems = new { tiledata= dashboarddata, recentitems = releases };

                    return Json(dashboardItems);
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
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            if (true)//item.Customer > 0)
            {
                if (item.Id == 0)
                {
                    db.ReleaseItems.Add(item);
                    db.SaveChanges();
                }
                else
                {
                    db.ReleaseItems.Attach(item);
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return Ok(new { Action = "Success" });
            }
            //else
            //{
            //    return BadRequest("Invalid Parameters/Insufficient Parameters");
            //}
        }
    }
}
