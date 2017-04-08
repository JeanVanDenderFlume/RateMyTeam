using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using RateMyTeam.Data;
using RateMyTeam.Data.Models;
using RateMyTeam.ViewModels;

namespace RateMyTeam.SeedData
{
    public class SeedSampleData
    {
        private ApplicationDbContext _context = null;
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<ApplicationUser> _userManager;

        public SeedSampleData( ApplicationDbContext ADBContext, UserManager<ApplicationUser> AUserManager, RoleManager<IdentityRole> ARoleManager )
        {
            _context = ADBContext;
            _roleManager = ARoleManager;
            _userManager = AUserManager;
        }

        public void SaveStudentUser( Student student)
        {
            /*
            if (!_context.Users.Any(u => (u.Id == student.User.Id))) {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(student.User, "secret");
                student.User.PasswordHash = hashed;               
                Console.WriteLine("Calculating user pw in database");
            }

            student.User.NormalizedUserName = student.User.Email.ToUpper();
            student.User.NormalizedEmail = student.User.Email.ToUpper();
            student.User.UserName = ApplicationUser.CalcFulldisplayName(student.User.Firstname, student.User.Infix, student.User.Lastname);
            _context.Add(student);  // the coder choose to let EF do the work, Asuming User is not saved by the programmer
                                    // EF will first save the User and then the student anf fill in the FK key to User
                                    // 
            _context.SaveChanges(); */

          
        }
       

        public void SaveTutorUser(Tutor tutor) {
            tutor.User.NormalizedUserName = tutor.User.Email.ToUpper();
            tutor.User.NormalizedEmail = tutor.User.Email.ToUpper();
            tutor.User.UserName = ApplicationUser.CalcFulldisplayName(tutor.User.Firstname, tutor.User.Infix, tutor.User.Lastname);
            _context.Add(tutor);          
            _context.SaveChanges();
        }

        public static void TestUpdateStudent_SpecificTutor(IServiceProvider services ) {
            var dbContext = services.GetService(typeof(ApplicationDbContext)) as ApplicationDbContext;

            var tutor = dbContext.Tutors.Find("VELDEMA");
            if( tutor != null){
                var student = dbContext.Students.Find("5323232"); // Gellisen@gmail.com
                if (student != null) {
                    student.Mentor = tutor;
                    dbContext.SaveChanges();
                }
            };
           
        }

        public static void TestAddUserFirst_SaveStudentManagedWay(IServiceProvider services) {
            var dbContext = services.GetService(typeof(ApplicationDbContext)) as ApplicationDbContext;

            // Planner
            var user = new ApplicationUser
            {
                Firstname = "Wilma",
                Lastname = "Witesseson",
                Email = "Witesseson@gmail.com",

                PhoneNumber = "+923363333352",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            /*
            var usertest = dbContext.Users.SingleOrDefault(s => s.NormalizedEmail == user.Email);
            if(usertest != null) {
                dbContext.Remove(usertest);
                dbContext.SaveChanges();
                dbContext.Entry(usertest).State = EntityState.Unchanged;  // if usertest == null cannot unset tracking 
                            }
            */


            dbContext.Add(user);
            dbContext.SaveChanges();
            //dbContext.Entry(user).State = EntityState.Detached;            
            //dbContext.Attach(user);
            var tutor = dbContext.Tutors.Find("VELDEMA");
            if (tutor != null) {
                dbContext.Entry(user).State = EntityState.Detached;
                dbContext.Entry(tutor).State = EntityState.Detached;

                var student = new Student();
                student.Studentnumber = "48483338";
                var student_test = dbContext.Tutors.Find(student.Studentnumber);
                if(student_test == null) {
                    dbContext.Add(student);
                }
                else {
                    dbContext.Update(student);
                }
                //student.UserId = user.Id;
                //student.Tutorcode = tutor.Tutorcode;

                dbContext.Entry(user).State = EntityState.Modified;
                dbContext.Entry(tutor).State = EntityState.Modified;

                student.User = user;
                student.Mentor = tutor;                    

                //dbContext.Entry(student.UserId).State = EntityState.Modified;
                //dbContext.Entry(tutor.Tutorcode).State = EntityState.Modified;
                dbContext.SaveChanges();
            }            

        }

        public static void CreateStartSeeding(IServiceProvider services) {
            var dbContext = services.GetService(typeof(ApplicationDbContext)) as ApplicationDbContext;
            if ( dbContext.Database.EnsureCreated() == false ) {
                Console.WriteLine("Initial DB is already created");

                //return;
            }
            Console.WriteLine("Now seeding with sample data");
           
            var roleManager = services.GetService(typeof(RoleManager<IdentityRole>)) as RoleManager<IdentityRole>;
            var userManager = services.GetService(typeof(UserManager<ApplicationUser>)) as UserManager<ApplicationUser>;

            var seedData = new SeedSampleData(dbContext, userManager, roleManager);
            string Tutorcode = "";

            ApplicationUser user;
            Tutor tutor;
            var lstStudents = new List<Student>();

            string[] roles = new string[] {
                "Student",
                "Tutor",
                "Administrator",
                "Manager",
                "Planner"
            };

            var roleStore = new RoleStore<IdentityRole>(dbContext);
            foreach (string role in roles) {
                if (!dbContext.Roles.Any(r => r.Name == role)) {
                    var new_role = new IdentityRole(role);
                    new_role.NormalizedName = role.ToUpper();
                    roleStore.CreateAsync(new_role).Wait();
                }
            }

            // Planner
            user = new ApplicationUser
            {
                Firstname = "Ruud",
                Lastname = "Plannisma",
                Email = "plannisma@gmail.com",

                PhoneNumber = "+923363333352",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };
            ApplicationUser.CreateUserLogin(dbContext, user, "secreet");
            dbContext.SaveChanges();
            roles = new string[] { "Planner" };
            seedData.ModifyUserRolesChangesAsync(user, roles).Wait();

            var listTutors = new List<Tutor>();

            // Tutor nr1
            Tutorcode = "VELDEMA";
            user = new ApplicationUser
            {
                Firstname = "Johwhann",
                Lastname = "Veldemans",
                Email = "Veldemans@gmail.com",

                PhoneNumber = "+92136666952",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };
            tutor = new Tutor {  Tutorcode = Tutorcode, User = user };
            listTutors.Add(tutor);

            // Tutor nr2
            Tutorcode = "ADEMA";
            user = new ApplicationUser
            {
                Firstname = "Zacherias",
                Lastname = "Adema",
                Email = "Adema@gmail.com",
                PhoneNumber = "+9233213062",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };
            tutor = new Tutor{  Tutorcode = Tutorcode, User = user };
            listTutors.Add(tutor);

            // Tutor nr3
            Tutorcode = "OKKIE";
            user = new ApplicationUser
            {
                Firstname = "Okkie",
                Lastname = "Ochalema",
                Email = "Ochalema@gmail.com",
                PhoneNumber = "+923923352",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };
            tutor = new Tutor { Tutorcode = Tutorcode, User = user };
            listTutors.Add(tutor);

            var i = 0;
            foreach( var tut in listTutors) {
                seedData.SaveTutorUser(tut); //-- EF saves the object User and then the Tutor, because of a filled User and FK in Tutor table. 
                if (i == 0) roles = new string[] { "Manager", "Tutor" }; else roles = new string[] { "Tutor" };
                seedData.ModifyUserRolesChangesAsync(tut.User, roles).Wait();
                i++;
            }           

            tutor = listTutors[0];
            // Student nr1
            user = new ApplicationUser
            {
                Firstname = "Wattie",
                Lastname = "Bolgels",
                Email = "Bolgels@gmail.com",
                PhoneNumber = "+923366633352",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };
            lstStudents.Add(new Student { Studentnumber = "12457025332", User = user, Mentor = tutor });



            // Student nr2
            user = new ApplicationUser
            {
                Firstname = "Mattie",
                Infix = "van der",
                Lastname = "Gellisen",
                Email = "Gellisen@gmail.com",
                PhoneNumber = "+923366633352",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };
            lstStudents.Add(new Student { Studentnumber = "532532324", User = user, Mentor = tutor });

            // Student nr3
            user = new ApplicationUser
            {
                Firstname = "Marieke",
                Infix = "",
                Lastname = "Themari",
                Email = "Themari@gmail.com",
                PhoneNumber = "+923362367652",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };
            lstStudents.Add(new Student { Studentnumber = "984838964", User = user, Mentor = tutor });


            // Student nr4
            user = new ApplicationUser
            {
                Firstname = "Janneke",
                Infix = "",
                Lastname = "Wierdinga",
                Email = "Wierdinga@gmail.com",
                PhoneNumber = "+923362367652",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };
            lstStudents.Add(new Student { Studentnumber = "518247812", User = user, Mentor = tutor });




            // Student nr5
            tutor = listTutors[1];
            user = new ApplicationUser
            {
                Firstname = "Jan-Peter",
                Infix = "",
                Lastname = "Handelsma",
                Email = "Handelsma@gmail.com",
                PhoneNumber = "+923362367652",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };
            lstStudents.Add(new Student { Studentnumber = "38234038", User = user, Mentor = tutor });



            // Student nr6
            tutor = listTutors[1];
            user = new ApplicationUser
            {
                Firstname = "Jassie",
                Infix = "",
                Lastname = "Gadaffie",
                Email = "Gadaffie@gmail.com",
                PhoneNumber = "+923362367652",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };
            lstStudents.Add(new Student { Studentnumber = "24483572", User = user, Mentor = tutor });



            // Student nr7
            tutor = listTutors[1];
            user = new ApplicationUser
            {
                Firstname = "Wiesje",
                Infix = "",
                Lastname = "Wiegel",
                Email = "Wiegel@gmail.com",
                PhoneNumber = "+923362367652",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };
            lstStudents.Add(new Student { Studentnumber = "259523178", User = user, Mentor = tutor });



            // Student nr8
            tutor = listTutors[1];
            user = new ApplicationUser
            {
                Firstname = "Ans",
                Infix = "",
                Lastname = "Hypertyp",
                Email = "Hypertyp@gmail.com",
                PhoneNumber = "+923362367652",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };
            lstStudents.Add(new Student { Studentnumber = "259523171", User = user, Mentor = tutor });

            foreach (Student stud in lstStudents) {
                ApplicationUser.CreateUserLogin(dbContext, stud.User);
                dbContext.Add(stud);
                dbContext.SaveChanges();

                //seedData.SaveStudentUser(stud ); // EF saves first the user and then the Student.
                seedData.ModifyUserRolesChangesAsync(stud.User, new string[] { "Student" }).Wait();             
            }

            //foreach (StudentUserInfo u in lstStudentUserInfo) { tutorGroupP101.Students.Add(u.Student);             }
            //var lstStudents = lstStudentUserInfo.Select(x => x.Student).ToList();

            
            

        }

        public async System.Threading.Tasks.Task ModifyUserRolesChangesAsync(ApplicationUser user, string[] roles) {

            var userStore = new UserStore<ApplicationUser>(_context);


            /* var manager = new UserManager<ApplicationUser>(userStore);
                Severity	Code	Description	Project	File	Line	Suppression State
                Error	CS70  36	
                There is no argument given that corresponds to the required formal parameter 'optionsAccessor' 
                of 'UserManager<ApplicationUser>.UserManager(IUserStore<ApplicationUser>, IOptions<IdentityOptions>, 
                IPasswordHasher<ApplicationUser>, IEnumerable<IUserValidator<ApplicationUser>>, IEnumerable<IPasswordValidator<ApplicationUser>>, 
                ILookupNormalizer, IdentityErrorDescriber, IServiceProvider, ILogger<UserManager<ApplicationUser>>)'	
                RateMyTeam	D:\Projects\MembersRating\RateMyTeam\SeedData\SeedSampleData.cs	381	Active             
             */

            var passwordHasher = new PasswordHasher<ApplicationUser>();
            var itemsValidators = new List<UserValidator<ApplicationUser>>();
            itemsValidators.Add(new UserValidator<ApplicationUser>());
            var passwordValidators = new List<PasswordValidator<ApplicationUser>>();
            passwordValidators.Add(new PasswordValidator<ApplicationUser>());
            var keyNormalizer = new LookupNormalizer();

            var errors = new IdentityErrorDescriber();
            //LoggerFactory logger = new LoggerFactory<ApplicationUserManager<ApplicationUser>>();

            /*
            var _userManager = new UserManager<ApplicationUser>(
                  userStore,
                  null,
                  passwordHasher,
                  itemsValidators,
                  passwordValidators,
                  null,
                  errors,
                  services, null
                  );
                  */

            // ApplicationUserManager _userManager = services.GetService(typeof(ApplicationUserManager)) as ApplicationUserManager;
            //services.GetService<UserManager<ApplicationUser>>();

            //UserManager<ApplicationUser> _userManager = dbContext.GetService<UserManager<ApplicationUser>>();
            //var store = new UserStore(dbContext);


            //if (_userManager == null)
            //    _userManager = new ApplicationUserManager(new UserStore<ApplicationUserManager<ApplicationUser>>(dbContext));

            //var passwordHasher = new PasswordHasher();
            // UserManager<ApplicationUser> _userManager =  new UserManager<ApplicationUser>(store );      

            //ApplicationUser user = await _userManager.FindByEmailAsync(email);
            var blUserExist = false;
            var user_rec = _context.Users.SingleOrDefault(u => u.NormalizedEmail == user.Email.ToUpper());
            if (user_rec != null) blUserExist = true;


            /*
            var listUserRoles = new List<IdentityRole>();

            foreach (string role in roles) {
                var role2 = new IdentityRole( role );
                var role_rec = _context.Roles.SingleOrDefault(r => r.NormalizedName == role.ToUpper());
                if (role_rec == null) {
                    _roleManager.CreateAsync(role2).Wait();
                }
                else {
                    _roleManager.UpdateAsync(role2).Wait();
                    var userRole = new IdentityUserRole<string>
                    {
                        RoleId = role_rec.Id
                    };
                    user.Roles.Add(userRole);
                }
                userStore.AddToRoleAsync(user, role ).Wait();
                listUserRoles.Add(role2);               
            }
            */

            foreach (string role in roles) {
                var blUserIsInRole = await _userManager.IsInRoleAsync(user, role );
                if (!blUserIsInRole) {
                    await _userManager.AddToRoleAsync(user, role );
                }

                /*
                var = await _userManager.IsInRoleAsync(user_rec, "" );

                if (!await _userManager.IsInRoleAsync(user, adminRole.Name)) {
                        await userManager.AddToRoleAsync(user, adminRole.Name);
                    }*/
            }

            if (!blUserExist) {
                //var listUserRoles = user.Roles;
                //user.Roles = listUserRoles;
                //userStore.AddToRoleAsync(user, listUserRoles).Wait();

            }
            else {
                /*
                IdentityResult roleRuslt = isExist ? await _roleManager.UpdateAsync(role2).Wait()
                                            : await _roleManager.CreateAsync(role2).Wait()
                                            */
                //_context.Entry(user).State = EntityState.Detached;

                //_userManager.AddToRoleAsync(user, role).Wait();
                //userStore.UpdateAsync(user).Wait();
            }



            try {
                _context.SaveChanges();
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);

            }
            // var result = await _userManager.AddToRolesAsync(user, roles );

        }

        private class LookupNormalizer
        {
            public LookupNormalizer()
            {
            }
        }
    }
}
