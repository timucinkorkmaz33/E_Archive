namespace SmartAdminMvc.Entity
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using Microsoft.AspNet.Identity.EntityFramework;
    using SmartAdminMvc.Identity;

    public partial class ProjectDb : IdentityDbContext<ApplicationUser>
    {
        public ProjectDb()
            : base("name=ProjectDb")
        {
          Database.SetInitializer(new MigrateDatabaseToLatestVersion<ProjectDb, Migrations.Configuration>("ProjectDb"));
        }

        public virtual DbSet<Project_General> Project_General { get; set; }
        public virtual DbSet<LogInformation> LogInformation { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
