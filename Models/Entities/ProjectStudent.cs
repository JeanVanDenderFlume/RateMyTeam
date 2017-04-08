using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RateMyTeam.Data.Models
{
    public class ProjectStudent
    {
        public string ProjectperiodId { get; set; }
        public Projectperiod Projectperiod { get; set; }

        public string StudentId { get; set; }
        public Student Student { get; set; }
    }
}
