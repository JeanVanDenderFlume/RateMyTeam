using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RateMyTeam.Data.Models;
using Microsoft.Extensions.Configuration;

namespace RateMyTeam.Data
{
    public class DBContext : DbContext
    {
        // You don't need all this.  Store it in web.conf
        // Also look into GraphDiff, a nuget package, that helps deal with entities

        public DbSet<Student> Students { get; set; }
        public DbSet<Tutor> Tutors { get; set; }
        public DbSet<Projectperiod> Projectperiods { get; set; }
        public DbSet<ProjectMemberRate> ProjectMemberRates { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);



            builder.Entity<ApplicationUser>().ToTable("Users");
            builder.Entity<ApplicationUser>(entity => entity.Property(m => m.Id).HasMaxLength(255));
            builder.Entity<ApplicationUser>(entity => entity.Property(m => m.NormalizedEmail).HasMaxLength(255));
            builder.Entity<ApplicationUser>(entity => entity.Property(m => m.NormalizedUserName).HasMaxLength(255));

            // JVD When creating a Student record then the user object is required
            builder.Entity<ProjectStudent>(entity => entity.HasKey( x => new { x.ProjectperiodId, x.StudentId } ));

            builder.Entity<Student>(entity => entity.Property(m => m.UserId).IsRequired());

            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityRole>(entity => entity.Property(m => m.Id).HasMaxLength(255));
            builder.Entity<IdentityRole>(entity => entity.Property(m => m.NormalizedName).HasMaxLength(255));

            builder.Entity<IdentityUserLogin<string>>().ToTable("Userlogins");
            builder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.LoginProvider).HasMaxLength(255));
            builder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.ProviderKey).HasMaxLength(255));
            builder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(255));

            builder.Entity<IdentityUserRole<string>>().ToTable("Userroles");
            builder.Entity<IdentityUserRole<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(255));
            builder.Entity<IdentityUserRole<string>>(entity => entity.Property(m => m.RoleId).HasMaxLength(255));

            builder.Entity<IdentityUserToken<string>>().ToTable("Usertokens");
            builder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(255));
            builder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.LoginProvider).HasMaxLength(255));
            builder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.Name).HasMaxLength(255));

            builder.Entity<IdentityUserClaim<string>>().ToTable("Userclaims");
            builder.Entity<IdentityUserClaim<string>>(entity => entity.Property(m => m.Id).HasMaxLength(255));
            builder.Entity<IdentityUserClaim<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(255));
            builder.Entity<IdentityRoleClaim<string>>(entity => entity.Property(m => m.Id).HasMaxLength(255));
            builder.Entity<IdentityRoleClaim<string>>(entity => entity.Property(m => m.RoleId).HasMaxLength(255));

            builder.Entity<IdentityRoleClaim<string>>().ToTable("Roleclaims");

        }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
    }
    
}

