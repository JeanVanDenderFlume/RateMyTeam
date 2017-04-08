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
