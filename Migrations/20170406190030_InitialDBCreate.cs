using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RateMyTeam.Migrations
{
    public partial class InitialDBCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 255, nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usertokens",
                columns: table => new
                {
                    UserId = table.Column<string>(maxLength: 255, nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 255, nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usertokens", x => new { x.UserId, x.LoginProvider, x.Name });
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 255, nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    Firstname = table.Column<string>(nullable: true),
                    Infix = table.Column<string>(nullable: true),
                    Initials = table.Column<string>(maxLength: 10, nullable: true),
                    Lastname = table.Column<string>(nullable: false),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 255, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 255, nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    SecurityStamp = table.Column<string>(nullable: true),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roleclaims",
                columns: table => new
                {
                    Id = table.Column<int>(maxLength: 255, nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    RoleId = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roleclaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Roleclaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Userclaims",
                columns: table => new
                {
                    Id = table.Column<int>(maxLength: 255, nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Userclaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Userclaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Userlogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 255, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 255, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Userlogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_Userlogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Userroles",
                columns: table => new
                {
                    UserId = table.Column<string>(maxLength: 255, nullable: false),
                    RoleId = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Userroles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_Userroles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Userroles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Projectperiods",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    CollegeLearningYear = table.Column<string>(maxLength: 4, nullable: false),
                    CollegePeriodNr = table.Column<string>(maxLength: 2, nullable: false),
                    CollegePeriod_Enddate = table.Column<DateTime>(nullable: false),
                    CollegePeriod_Startdate = table.Column<DateTime>(nullable: false),
                    Created_at = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    ProjectteamCode = table.Column<string>(maxLength: 10, nullable: false),
                    RatingInput_ClosingDatetime = table.Column<DateTime>(nullable: false),
                    RatingInput_OpeningDatetime = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(maxLength: 255, nullable: true),
                    UpdatedByRefUser = table.Column<string>(nullable: true),
                    Updated_at = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projectperiods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projectperiods_Users_UpdatedByRefUser",
                        column: x => x.UpdatedByRefUser,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tutors",
                columns: table => new
                {
                    Tutorcode = table.Column<string>(maxLength: 10, nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    Created_at = table.Column<DateTime>(nullable: false),
                    UpdatedByRefUser = table.Column<string>(nullable: true),
                    Updated_at = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tutors", x => x.Tutorcode);
                    table.ForeignKey(
                        name: "FK_Tutors_Users_UpdatedByRefUser",
                        column: x => x.UpdatedByRefUser,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tutors_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Studentnumber = table.Column<string>(maxLength: 20, nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    Created_at = table.Column<DateTime>(nullable: false),
                    Mentorcode = table.Column<string>(maxLength: 10, nullable: true),
                    UpdatedByRefUser = table.Column<string>(nullable: true),
                    Updated_at = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Studentnumber);
                    table.ForeignKey(
                        name: "FK_Students_Tutors_Mentorcode",
                        column: x => x.Mentorcode,
                        principalTable: "Tutors",
                        principalColumn: "Tutorcode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Students_Users_UpdatedByRefUser",
                        column: x => x.UpdatedByRefUser,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Students_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectMemberRates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    CatagoryRatings = table.Column<string>(nullable: true),
                    Created_at = table.Column<DateTime>(nullable: false),
                    ProjectPeriodId = table.Column<string>(nullable: true),
                    RatingsGivenByStudentId = table.Column<string>(nullable: true),
                    StudentIdThatIsRated = table.Column<string>(nullable: true),
                    Subject1Rate = table.Column<int>(nullable: false),
                    Subject2Rate = table.Column<int>(nullable: false),
                    Subject3Rate = table.Column<int>(nullable: false),
                    UpdatedByRefUser = table.Column<string>(nullable: true),
                    Updated_at = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectMemberRates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectMemberRates_Projectperiods_ProjectPeriodId",
                        column: x => x.ProjectPeriodId,
                        principalTable: "Projectperiods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectMemberRates_Students_RatingsGivenByStudentId",
                        column: x => x.RatingsGivenByStudentId,
                        principalTable: "Students",
                        principalColumn: "Studentnumber",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectMemberRates_Students_StudentIdThatIsRated",
                        column: x => x.StudentIdThatIsRated,
                        principalTable: "Students",
                        principalColumn: "Studentnumber",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectMemberRates_Users_UpdatedByRefUser",
                        column: x => x.UpdatedByRefUser,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectStudent",
                columns: table => new
                {
                    ProjectperiodId = table.Column<string>(nullable: false),
                    StudentId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectStudent", x => new { x.ProjectperiodId, x.StudentId });
                    table.ForeignKey(
                        name: "FK_ProjectStudent_Projectperiods_ProjectperiodId",
                        column: x => x.ProjectperiodId,
                        principalTable: "Projectperiods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectStudent_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Studentnumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roleclaims_RoleId",
                table: "Roleclaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Userclaims_UserId",
                table: "Userclaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Userlogins_UserId",
                table: "Userlogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Userroles_RoleId",
                table: "Userroles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMemberRates_ProjectPeriodId",
                table: "ProjectMemberRates",
                column: "ProjectPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMemberRates_RatingsGivenByStudentId",
                table: "ProjectMemberRates",
                column: "RatingsGivenByStudentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMemberRates_StudentIdThatIsRated",
                table: "ProjectMemberRates",
                column: "StudentIdThatIsRated");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMemberRates_UpdatedByRefUser",
                table: "ProjectMemberRates",
                column: "UpdatedByRefUser");

            migrationBuilder.CreateIndex(
                name: "IX_Projectperiods_UpdatedByRefUser",
                table: "Projectperiods",
                column: "UpdatedByRefUser");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectStudent_StudentId",
                table: "ProjectStudent",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_Mentorcode",
                table: "Students",
                column: "Mentorcode");

            migrationBuilder.CreateIndex(
                name: "IX_Students_UpdatedByRefUser",
                table: "Students",
                column: "UpdatedByRefUser");

            migrationBuilder.CreateIndex(
                name: "IX_Students_UserId",
                table: "Students",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tutors_UpdatedByRefUser",
                table: "Tutors",
                column: "UpdatedByRefUser");

            migrationBuilder.CreateIndex(
                name: "IX_Tutors_UserId",
                table: "Tutors",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Roleclaims");

            migrationBuilder.DropTable(
                name: "Userclaims");

            migrationBuilder.DropTable(
                name: "Userlogins");

            migrationBuilder.DropTable(
                name: "Userroles");

            migrationBuilder.DropTable(
                name: "Usertokens");

            migrationBuilder.DropTable(
                name: "ProjectMemberRates");

            migrationBuilder.DropTable(
                name: "ProjectStudent");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Projectperiods");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Tutors");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
