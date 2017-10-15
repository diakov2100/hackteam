using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hackteam.Context
{
    public class Repositry
    {
        public class user
        {
            public int id { get; set; }
            public string name { get; set; }
            public string skills { get; set; }
            public string twist_id { get; set; }
            public string twist_email { get; set; }
            public Nullable<int> project_id { get; set; }

            public List<string> roles;
            public user()
            {
            }

            public user(User User_)
            {
                id = User_.id;
                name = User_.name;
                skills = User_.skills;
                twist_id = User_.twist_id;
                twist_email = User_.twist_email;
            }

            public int AddUser()
            {
                using (hackteamEntities db = new hackteamEntities())
                {
                    var user_ = db.User.Add(new User() { name = name, skills = skills, twist_email = twist_email, twist_id = twist_id });
                    db.SaveChanges();
                    foreach (var cur in roles)
                    {
                        db.Users_Roles.Add(new Users_Roles() { role = cur, user_id = user_.id });
                    }
                    db.SaveChanges();
                    return user_.id;
                }
            }
            public static user Find(int id)
            {

                using (hackteamEntities db = new hackteamEntities())
                {
                    var user = db.User.Find(id);
                    if (user == null) return null;
                    var roles = db.Users_Roles.Where(t1 => t1.user_id == id).Select(t1 => t1.role);
                    return new user() { id = user.id, name = user.name, skills = user.skills, project_id = user.project_id, roles = roles.ToList(), twist_email = user.twist_email, twist_id = user.twist_id };
                }
            }
            public void SetProject(int prid)
            {
                using (hackteamEntities db = new hackteamEntities())
                {
                    var user = db.User.Find(id);
                    var newuser = user;
                    newuser.project_id = prid;
                    db.Entry(user).CurrentValues.SetValues(newuser);
                    db.SaveChanges();
                }
            }
        }

        public class project
        {
            public string description;
            public int id;
            public List<string> roles;
            public List<user> users;

            public int AddProject()
            {
                using (hackteamEntities db = new hackteamEntities())
                {
                    Project project = new Project()
                    {
                        description = description
                    };
                    var res = db.Project.Add(project);
                    db.SaveChanges();
                    foreach (var role in roles)
                    {
                        db.Project_Roles.Add(new Project_Roles() { project_id = res.id, role = role });
                    }
                    db.SaveChanges();
                    return res.id;
                }
            }
            public static project Find(int id)
            {
                using (hackteamEntities db = new hackteamEntities())
                {
                    var res = db.Project.Find(id);
                    if (res == null) return null;
                    var roles_ = db.Project_Roles.Where(t1 => t1.project_id == id).Select(t1 => t1.role).ToList();
                    var users_ = db.User.Where(t1 => t1.project_id == id).Select(t1 => t1).ToList();
                    var result = new project() { description = res.description, id = res.id, roles = roles_, users = new List<user>() };
                    foreach (var cur in users_)
                    {
                        result.users.Add(user.Find(cur.id));
                    }
                    return result;
                }
            }
        }
        public class invations : Invations
        {
            public static void CleanList(Invations inv)
            {
                using (hackteamEntities db = new hackteamEntities())
                {
                    db.Invations.RemoveRange(
                        db.Invations.Where(t1 => (t1.role == inv.role && t1.project_id == inv.project_id) || (t1.user_id == inv.user_id))
                    );
                    db.SaveChanges();
                }
            }
            public static List<Invations> ProjectInvations(int project_id)
            {
                using (hackteamEntities db = new hackteamEntities())
                {
                    return db.Invations.Where(t1 => t1.project_id == project_id).Select(t1 => t1).ToList();
                }
            }
        }
    }
}