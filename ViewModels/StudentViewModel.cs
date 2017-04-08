using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RateMyTeam.Data.Models;

namespace  RateMyTeam.ViewModels
{
    public class StudentViewModel: Student
    {       
        // overwrite the Student.User with type with less properties
        public new StudentAppUserViewModel User { get; set; }

        public Projectperiod Projectperiod { get; set; }

        

    }
}
