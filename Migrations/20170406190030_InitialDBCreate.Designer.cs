using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using RateMyTeam.Data;

namespace RateMyTeam.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20170406190030_InitialDBCreate")]
    partial class InitialDBCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Roleclaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Userclaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(255);

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(255);

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("Userlogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasMaxLength(255);

                    b.Property<string>("RoleId")
                        .HasMaxLength(255);

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("Userroles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasMaxLength(255);

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(255);

                    b.Property<string>("Name")
                        .HasMaxLength(255);

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("Usertokens");
                });

            modelBuilder.Entity("RateMyTeam.Data.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255);

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("Firstname");

                    b.Property<string>("Infix");

                    b.Property<string>("Initials")
                        .HasMaxLength(10);

                    b.Property<string>("Lastname")
                        .IsRequired();

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(255);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(255);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RateMyTeam.Data.Models.ProjectMemberRate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CatagoryRatings");

                    b.Property<DateTime>("Created_at");

                    b.Property<string>("ProjectPeriodId");

                    b.Property<string>("RatingsGivenByStudentId");

                    b.Property<string>("StudentIdThatIsRated");

                    b.Property<int>("Subject1Rate");

                    b.Property<int>("Subject2Rate");

                    b.Property<int>("Subject3Rate");

                    b.Property<string>("UpdatedByRefUser");

                    b.Property<DateTime>("Updated_at");

                    b.HasKey("Id");

                    b.HasIndex("ProjectPeriodId");

                    b.HasIndex("RatingsGivenByStudentId");

                    b.HasIndex("StudentIdThatIsRated");

                    b.HasIndex("UpdatedByRefUser");

                    b.ToTable("ProjectMemberRates");
                });

            modelBuilder.Entity("RateMyTeam.Data.Models.Projectperiod", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CollegeLearningYear")
                        .IsRequired()
                        .HasMaxLength(4);

                    b.Property<string>("CollegePeriodNr")
                        .IsRequired()
                        .HasMaxLength(2);

                    b.Property<DateTime>("CollegePeriod_Enddate");

                    b.Property<DateTime>("CollegePeriod_Startdate");

                    b.Property<DateTime>("Created_at");

                    b.Property<string>("Description")
                        .HasMaxLength(255);

                    b.Property<string>("ProjectteamCode")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<DateTime>("RatingInput_ClosingDatetime");

                    b.Property<DateTime>("RatingInput_OpeningDatetime");

                    b.Property<string>("Title")
                        .HasMaxLength(255);

                    b.Property<string>("UpdatedByRefUser");

                    b.Property<DateTime>("Updated_at");

                    b.HasKey("Id");

                    b.HasIndex("UpdatedByRefUser");

                    b.ToTable("Projectperiods");
                });

            modelBuilder.Entity("RateMyTeam.Data.Models.ProjectStudent", b =>
                {
                    b.Property<string>("ProjectperiodId");

                    b.Property<string>("StudentId");

                    b.HasKey("ProjectperiodId", "StudentId");

                    b.HasIndex("StudentId");

                    b.ToTable("ProjectStudent");
                });

            modelBuilder.Entity("RateMyTeam.Data.Models.Student", b =>
                {
                    b.Property<string>("Studentnumber")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(20);

                    b.Property<DateTime>("Created_at");

                    b.Property<string>("Mentorcode")
                        .HasMaxLength(10);

                    b.Property<string>("UpdatedByRefUser");

                    b.Property<DateTime>("Updated_at");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("Studentnumber");

                    b.HasIndex("Mentorcode");

                    b.HasIndex("UpdatedByRefUser");

                    b.HasIndex("UserId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("RateMyTeam.Data.Models.Tutor", b =>
                {
                    b.Property<string>("Tutorcode")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(10);

                    b.Property<DateTime>("Created_at");

                    b.Property<string>("UpdatedByRefUser");

                    b.Property<DateTime>("Updated_at");

                    b.Property<string>("UserId");

                    b.HasKey("Tutorcode");

                    b.HasIndex("UpdatedByRefUser");

                    b.HasIndex("UserId");

                    b.ToTable("Tutors");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("RateMyTeam.Data.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("RateMyTeam.Data.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RateMyTeam.Data.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RateMyTeam.Data.Models.ProjectMemberRate", b =>
                {
                    b.HasOne("RateMyTeam.Data.Models.Projectperiod", "ProjectPeriod")
                        .WithMany()
                        .HasForeignKey("ProjectPeriodId");

                    b.HasOne("RateMyTeam.Data.Models.Student", "RatingsGivenByStudent")
                        .WithMany()
                        .HasForeignKey("RatingsGivenByStudentId");

                    b.HasOne("RateMyTeam.Data.Models.Student", "StudentThatIsRated")
                        .WithMany()
                        .HasForeignKey("StudentIdThatIsRated");

                    b.HasOne("RateMyTeam.Data.Models.ApplicationUser", "UpdatedByUser")
                        .WithMany()
                        .HasForeignKey("UpdatedByRefUser");
                });

            modelBuilder.Entity("RateMyTeam.Data.Models.Projectperiod", b =>
                {
                    b.HasOne("RateMyTeam.Data.Models.ApplicationUser", "UpdatedByUser")
                        .WithMany()
                        .HasForeignKey("UpdatedByRefUser");
                });

            modelBuilder.Entity("RateMyTeam.Data.Models.ProjectStudent", b =>
                {
                    b.HasOne("RateMyTeam.Data.Models.Projectperiod", "Projectperiod")
                        .WithMany("ProjectStudents")
                        .HasForeignKey("ProjectperiodId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RateMyTeam.Data.Models.Student", "Student")
                        .WithMany("ProjectStudents")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RateMyTeam.Data.Models.Student", b =>
                {
                    b.HasOne("RateMyTeam.Data.Models.Tutor", "Mentor")
                        .WithMany()
                        .HasForeignKey("Mentorcode");

                    b.HasOne("RateMyTeam.Data.Models.ApplicationUser", "UpdatedByUser")
                        .WithMany()
                        .HasForeignKey("UpdatedByRefUser");

                    b.HasOne("RateMyTeam.Data.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RateMyTeam.Data.Models.Tutor", b =>
                {
                    b.HasOne("RateMyTeam.Data.Models.ApplicationUser", "UpdatedByUser")
                        .WithMany()
                        .HasForeignKey("UpdatedByRefUser");

                    b.HasOne("RateMyTeam.Data.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });
        }
    }
}
