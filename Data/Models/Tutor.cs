using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RateMyTeam.Data.Models
{
    public class Tutor
    {
        [Key]
        [MaxLength(10)]
        public string  Tutorcode{ get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }

        [ForeignKey("UpdatedByRefUser")]
        public ApplicationUser UpdatedByUser { get; set; }
    }
}
