using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using hackteam.Context;

namespace hackteam.Controllers
{
    public class SearchController : ApiController
    {
        private hackteamEntities db = new hackteamEntities();

        [HttpGet]
        public IHttpActionResult SearchCandidates(int id)
        {
            var project = db.Project.Find(id);
            var roles = project.Project_Roles.Where(t1 => t1.user_id == null).Select(t1 => t1.role).ToList();
            var result = db.User.Where(t1 => t1.project_id == null && t1.Users_Roles.ToList().Any(t2 => roles.Contains(t2.role))).Select(User_ => new Repositry.user()
            {
                id = User_.id,
                name = User_.name,
                skills = User_.skills,
                twist_id = User_.twist_id,
                twist_email = User_.twist_email,
                roles = User_.Users_Roles.Select(t1=> t1.role).ToList()
            }).ToList();
            
            return Ok(result);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.User.Count(e => e.id == id) > 0;
        }
    }
}
