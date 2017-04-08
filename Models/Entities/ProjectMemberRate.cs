using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using RateMyTeam.Data.Models;

namespace RateMyTeam.Data.Models
{
    public class ProjectMemberRate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string ProjectPeriodId { get; set; }
        [ForeignKey("ProjectPeriodId")]
        public Projectperiod ProjectPeriod { get; set; }

        public string StudentIdThatIsRated { get; set; }
        [ForeignKey("StudentIdThatIsRated")]
        public Student StudentThatIsRated { get; set; }

        // 0 = not filled in
        [Range(0, 5, ErrorMessage = "Can only be between 0..5")]
        public int Subject1Rate { get; set; }

        [Range(0, 5, ErrorMessage = "Can only be between 0..5")]
        public int Subject2Rate { get; set; }

        [Range(0, 5, ErrorMessage = "Can only be between 0..5")]
        public int Subject3Rate { get; set; }

     
        public string CatagoryRatings { get; set; } //  Normal Q rating, or  Z rating

        public string RatingsGivenByStudentId { get; set; }
        [ForeignKey("RatingsGivenByStudentId")]
        public Student RatingsGivenByStudent { get; set; }

        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }

        [ForeignKey("UpdatedByRefUser")]
        public ApplicationUser UpdatedByUser { get; set; }
    }
}
