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
    public class UsersController : ApiController
    {
        private hackteamEntities db = new hackteamEntities();

        
        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(int id)
        {
            var user = Repositry.user.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

       
        // POST: api/Users
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(Repositry.user user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var res = user.AddUser();
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

        private bool UserExists(int id)
        {
            return db.User.Count(e => e.id == id) > 0;
        }
    }
}