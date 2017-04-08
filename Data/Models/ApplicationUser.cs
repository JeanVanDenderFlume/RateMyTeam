using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace RateMyTeam.Data.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser {
        private string _Infix;
        private string _Lastname;
        private string _Firstname;

        [MaxLength(10)]
        public string Initials { get; set; }
        [Display(Name = "tussenvoegsel")]
        public string Infix {   // Tussenvoegsel
            get { return _Infix; }
            set { _Infix = value;
                  _FulldisplayName = CalcFulldisplayName(_Firstname, _Infix, _Lastname);
            }
        }

        [Required]
        public string Lastname{
            get { return _Lastname; }
            set { _Lastname = value;
                _FulldisplayName = CalcFulldisplayName(_Firstname, _Infix, _Lastname);
            }
        }
        
        public string Firstname {
            get{ return _Firstname; }
            set{  _Firstname = value;
                _FulldisplayName = CalcFulldisplayName( _Firstname, _Infix, _Lastname);
            }
        }
       

        private string _FulldisplayName;        
        public static string CalcFulldisplayName(string AFirstName, string AInfix, string ALastname) {
            var strOut = "";
            if ( !String.IsNullOrEmpty(AFirstName ))
                strOut = AFirstName.Trim() + " ";

            if (!String.IsNullOrEmpty(AInfix))
                strOut = strOut + AInfix.Trim() + " ";

            if (!String.IsNullOrEmpty(ALastname))
                strOut = strOut + ALastname.Trim();
            return strOut;
        }

        public string FulldisplayName {
            get{
                if(_FulldisplayName == null) {
                    _FulldisplayName = CalcFulldisplayName(_Firstname, _Infix, _Lastname);
                }
                return _FulldisplayName;
            }
        }


        public static void CreateUserLogin( ApplicationDbContext dbContext, ApplicationUser user, string user_password = "" ) {
            var _context = dbContext;
            var userStore = new UserStore<ApplicationUser>(_context);
            var user_excist = _context.Users.SingleOrDefault(u => u.Email.ToUpper() == user.Email.ToUpper());
            if ( user_excist == null ) {
                _context.Users.Add(user);
            }
            else {
                _context.Users.Update(user);
            }

            var blInitUserName = false;
            if (string.IsNullOrEmpty(user.UserName)) {
                blInitUserName = true;
            }
            else {
                if (!(user.UserName.IndexOf("@") > -1)) {
                    blInitUserName = true;
                }
            }
            
            if (blInitUserName) { 
                var s = user.Email.ToLower();
                var s1 = (s[0] + "").ToUpper();
                var s2 = s1 + s.Substring(1);
                user.UserName = s2;
                user.NormalizedEmail = user.Email.ToUpper();

                if (string.IsNullOrEmpty(user.PasswordHash)) {
                    var password = new PasswordHasher<ApplicationUser>();
                    if (user_password == "") {
                        user_password = "secret_" + user.Lastname[0];
                    }
                    var hashed = password.HashPassword(user, user_password);
                    user.PasswordHash = hashed;
                }
            }

          

            if (string.IsNullOrEmpty(user.NormalizedEmail)) user.NormalizedEmail = user.Email.ToUpper();

            //  }      CalcFulldisplayName(user.Firstname, user.Infix, user.Lastname);
            if (string.IsNullOrEmpty(user.SecurityStamp)) user.SecurityStamp = Guid.NewGuid().ToString("D"); // In order to let Claims work, otherwise after login a error will be trown

           

          
           

            /*
            if (!_context.Users.Any(u => (u.NormalizedUserName == user.Email.Trim().ToUpper()))) {

                try {
                    _context.SaveChanges();
                }
                catch (Exception ex) {
                    Console.WriteLine(ex.Message);
                }
                //userStore.CreateAsync(user).Wait();
            }*/
        }

    }



}
