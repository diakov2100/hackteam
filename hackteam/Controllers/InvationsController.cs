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
    public class InvationsController : ApiController
    {
        private hackteamEntities db = new hackteamEntities();

       
        // GET: api/Invations/5
        [ResponseType(typeof(Invations))]
        public IHttpActionResult GetInvations(int user)
        {
            List<Invations> invations = db.Invations.Where(t1=> t1.user_id == user ).Select(t1=> t1).ToList();
            if (invations == null)
            {
                return NotFound();
            }

            return Ok(invations);
        }
        [HttpGet]
        public IHttpActionResult InvationsProject(int project)
        {
            List<Invations> invations = db.Invations.Where(t1 => t1.project_id == project).Select(t1 => t1).ToList();
            if (invations == null)
            {
                return NotFound();
            }

            return Ok(invations);
        }

        // POST: api/Invations
        [ResponseType(typeof(Invations))]
        public IHttpActionResult PostInvations(Invations invations)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Invations.Add(invations);
            db.SaveChanges();
            return Ok();
        }

        // DELETE: api/Invations/5
        [ResponseType(typeof(Invations))]
        public IHttpActionResult DeleteInvations(int invitation)
        {
            Invations invations = db.Invations.Find(invitation);
            if (invations == null)
            {
                return NotFound();
            }
            var project_roles = db.Project_Roles.Where(t1 => t1.project_id == invations.project_id && t1.role == invations.role).Select(t1 => t1).FirstOrDefault();
            
            var project_roles_mod = project_roles;
            project_roles_mod.user_id = invations.user_id;
            db.Entry(project_roles).CurrentValues.SetValues(project_roles_mod);
            db.SaveChanges();

            Repositry.invations.CleanList(invations);

            var user_= Repositry.user.Find(invations.user_id);
            user_.SetProject(invations.project_id);
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InvationsExists(int id)
        {
            return db.Invations.Count(e => e.id == id) > 0;
        }
    }
}