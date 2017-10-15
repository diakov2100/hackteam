using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using hackteam;
using hackteam.Context;

namespace hackteam.Controllers
{
    public class ProjectsController : ApiController
    {
        private hackteamEntities db = new hackteamEntities();

       
        // GET: api/Projects/5
        public IHttpActionResult GetProject(int id)
        {
            var project = Repositry.project.Find(id);
            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        // POST: api/Projects
        public IHttpActionResult PostProject([FromUri] int  user, [FromBody] Repositry.project project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var res = project.AddProject();
            var admin = Repositry.user.Find(user);
            admin.SetProject(res);
            return Ok(res);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProjectExists(int id)
        {
            return db.Project.Count(e => e.id == id) > 0;
        }
    }
}