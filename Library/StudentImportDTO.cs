using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RateMyTeam.Library
{
    public class StudentImportDTO
    {
        //Groepsnummer; studentnummer; achternaam; voorvoegsels; voorletters; roepnaam; e - mail instelling; Anders; AANWEZIG;
        //P101; 2326388; Borgers; ; EAP; Ewout; e.borgers @student.fontys.nl; SNEL; ;
        public string Projectgroupnumber;
        public string Studentnummer;
        public string Lastname;
        public string Infix;
        public string Initials;
        public string Firstname;
        public string Email;

        public string Misc;
        public string Present;

    }
}
