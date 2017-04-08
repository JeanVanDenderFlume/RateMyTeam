using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using RateMyTeam.ViewModels;

namespace RateMyTeam.Data.Models
{

    public class Projectperiod
    {

        [Key]
        [Required]
        [MaxLength(14)] // 2017_01_P103
                        //[ConcurrencyCheck, MaxLength(12, ErrorMessage = "BloggerName must be 10 characters or less"), MinLength(12)]

        private string _CollegeLearningYear;
        private string _CollegePeriodNr;
        private string _Id;
        private string _ProjectteamCode;

        [NotMapped]
        public ICollection<StudentViewModel> listStudentsViewData;  // Partialview uses thid

         // many to many,  ProjectPeriodId <=> List of students, and student can belong to several projects
        public List<ProjectStudent> ProjectStudents { get; set; }

        public string Id {
            get { return _Id; }
            set
            {
                _Id = value;
            }
        }

        [Required]
        [Range(2016, 2040, ErrorMessage = "Can only be between 2016 .. 2040")]
        [StringLength(4, ErrorMessage = "Max 4 digits")]
        public string CollegeLearningYear {
            get { return _CollegeLearningYear; }
            set {
                _CollegeLearningYear = value; CalcId();
            }
        }

        [Required]
        [Range(1, 8, ErrorMessage = "Can only be between 1 .. 8")]
        [StringLength(2, ErrorMessage = "Max 2 digits")]
        public string CollegePeriodNr
        {
            get { return _CollegePeriodNr; }
            set {
                _CollegePeriodNr = value; CalcId();
            }
        }


        public DateTime CollegePeriod_Startdate { get; set; }
        public DateTime CollegePeriod_Enddate { get; set; }

        [Required]       
        [MaxLength(10)]
        public string ProjectteamCode
        {
            get { return _ProjectteamCode; }
            set {
                _ProjectteamCode = value; CalcId();
            }
        }

        [MaxLength(255)]
        public string Title { get; set; }
        [MaxLength(255)]
        public string Description { get; set; }

       
        public DateTime RatingInput_OpeningDatetime { get; set; }
        public DateTime RatingInput_ClosingDatetime { get; set; }

        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }

        [ForeignKey("UpdatedByRefUser")]
        public ApplicationUser UpdatedByUser { get; set; }

        private void CalcId() {
            this._Id = CalculateId(this._CollegeLearningYear, this._CollegePeriodNr, this._ProjectteamCode);
        }
           
        public static string CalculateId( string CollegeLearningYear, string CollegePeriodNr, string ProjectteamCode) {
            if ( String.IsNullOrEmpty(CollegeLearningYear)) return null;
            if (String.IsNullOrEmpty(CollegePeriodNr)) return null;
            if (String.IsNullOrEmpty(ProjectteamCode)) return null;

            var intCollegePeriodNr = Convert.ToInt16( CollegePeriodNr);
            var calcedId = CollegeLearningYear + "_" + string.Format("{0:00}", intCollegePeriodNr) + "_" + ProjectteamCode;
            return calcedId;
        }
    }
}
