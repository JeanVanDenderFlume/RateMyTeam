using RateMyTeam.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace  RateMyTeam.ViewModels
{
    public class StudentAppUserViewModel: ApplicationUser
    {
        //[Obsolete("Don't use this", true)]
        //public new ICollection Roles { get; }
        [Obsolete("Don't use this", true)]
        public new int AccessFailedCount { get; set; }
        [Obsolete("Don't use this", true)]
        public new bool LockoutEnabled { get; set; }
        [Obsolete("Don't use this", true)]
        public new DateTimeOffset? LockoutEnd { get; set; }
        [Obsolete("Don't use this", true)]
        public new bool TwoFactorEnabled { get; set; }
        [Obsolete("Don't use this", true)]
        public new bool PhoneNumberConfirmed { get; set; }
       
        //public new string PhoneNumber { get; set; }
        [Obsolete("Don't use this", true)]
        public new string ConcurrencyStamp { get; set; }
        [Obsolete("Don't use this", true)]
        public new string SecurityStamp { get; set; }
        [Obsolete("Don't use this", true)]
        public new string PasswordHash { get; set; }
        [Obsolete("Don't use this", true)]
        public new bool EmailConfirmed { get; set; }
        [Obsolete("Don't use this", true)]
        public new string NormalizedEmail { get; set; }
        //public new string Email { get; set; }
        [Obsolete("Don't use this", true)]
        public new string NormalizedUserName { get; set; }

        //[Obsolete("Don't use this", true)]
        //public new string UserName { get; set; }
    }

  
}
