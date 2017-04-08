using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;



namespace  RateMyTeam.ViewModels
{

    public class UserEmailItem
    {
        public string Id { get; set; }
        public string Email { get; set; }
    }
    public class StudentCreateStep1_ViewModel
    {
        [Display(Name = " Gebruiker waar studentgegevens ingevoerd moet worden")]
        public string SelectedUserId { get; set; }

        public List<UserEmailItem> ListUsers_Email { get; set; }
    }
}
