using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RateMyTeam.Data.Models
{
    public class Student
    {
        [Key]
        [MaxLength(20)]
        public string  Studentnumber { get; set; }  

        [MaxLength(255)]
        public string UserId { get; set; }
        //[Required]

        public string FirstName {get;set;}

        public string LastName {get;set;}
        public ApplicationUser User { get; set; }

        [MaxLength(10)]
        [Display(Name = "Mentor")]
        public string Mentorcode { get; set; }

        [ForeignKey("Mentorcode")]
        public Tutor  Mentor { get; set; }
        

        public List<ProjectStudent> ProjectStudents { get; set; }
        



        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }

        [ForeignKey("UpdatedByRefUser")]
        public ApplicationUser UpdatedByUser { get; set; }

        public string FullName()
        {
            //Logic to concatenate the Fullname, you don't need temporary variables   
        }
    }
}
