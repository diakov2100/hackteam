﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace hackteam
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class hackteamEntities : DbContext
    {
        public hackteamEntities()
            : base("name=hackteamEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Invations> Invations { get; set; }
        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<Project_Roles> Project_Roles { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Users_Roles> Users_Roles { get; set; }
        public virtual DbSet<database_firewall_rules> database_firewall_rules { get; set; }
    }
}
