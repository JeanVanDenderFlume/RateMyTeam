using RateMyTeam.Data;
using RateMyTeam.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// 
namespace RateMyTeam.ViewModels
{
    public class AppUserViewModel
    {
        public static ApplicationUser GetCreateAppUser( string UserName) {
            var dbconext = ApplicationDbContext.CreateDBContextMySQL();
            ApplicationUser AppUser = dbconext.Users.SingleOrDefault(u => u.NormalizedEmail == UserName.ToUpper());
            return AppUser;
        }        
    }

  
}
