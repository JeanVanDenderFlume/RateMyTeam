using CsvFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RateMyTeam.Data;
using RateMyTeam.Library;
using RateMyTeam.Data.Models;
using System.IO;
using System.ComponentModel.DataAnnotations;

namespace RateMyTeam.Services
{
    public class StudentsDataListProvider
    {

        public static void ImportStudentList( string student_file, IServiceProvider services, [StringLength(4)] string AProjectCollegeYear,[MinLength(2)]string ACollegePeriodNr ) {

            
            ApplicationDbContext dbcontext = services.GetService(typeof(ApplicationDbContext)) as ApplicationDbContext;
            /*
            Groepsnummer; studentnummer; achternaam; voorvoegsels; voorletters; roepnaam; e - mail instelling; Anders; AANWEZIG;
            P101; 2326388; Borgers; ; EAP; Ewout; e.borgers @student.fontys.nl; SNEL; ;
            P101; 3079554; Gerritsen; ; W; Wouter; w.gerritsen @student.fontys.nl; ; ;
            */
            Student student;
            ApplicationUser user;

            //var txtLines = CsvFiles.CsvFile.Read<StudentImportDTO>("D:/Projects/MembersRating/LijstStudentNummers2.csv");
            string line;
            FileStream aFile = new FileStream( student_file, FileMode.Open);
            StreamReader sr = new StreamReader(aFile);
            
            var listStudents = new List<Student>();
            string strProjectteamCodePrev = "";
            string strProjectteamCode = "";
            Projectperiod projectperiod = new Projectperiod();

            // read data in line by line
            while ((line = sr.ReadLine()) != null) {
               
                String[] csvFields = line.Split(';');

                strProjectteamCode = csvFields[0];
                if (strProjectteamCode == "") { continue; };
                if (strProjectteamCode[0] != 'P') { continue; };

                
                if (strProjectteamCodePrev != strProjectteamCode) {
                    if (strProjectteamCodePrev != "") {
                        AddProjectStudents(AProjectCollegeYear, ACollegePeriodNr, dbcontext, listStudents, strProjectteamCode, projectperiod);
                    }

                    projectperiod = new Projectperiod()
                    {
                        ProjectteamCode = strProjectteamCode,
                        CollegeLearningYear = AProjectCollegeYear,
                        CollegePeriodNr = ACollegePeriodNr,
                        //Id is calculated internally in ProjectPeriod
                       
                    };
                    strProjectteamCodePrev = strProjectteamCode;
                }
                
            user = new ApplicationUser(){
                    Email = csvFields[6].ToLower() ,
                    NormalizedEmail = csvFields[6].ToUpper(),
                    NormalizedUserName = csvFields[6].ToUpper(),
                    Firstname = csvFields[5],
                    Lastname = csvFields[2],
                    Infix = csvFields[3],
                    Initials = csvFields[4],
                };

                student = new Student(){
                    Studentnumber = csvFields[1],
                    User = user
                };

                if (!listStudents.Any(s => s.Studentnumber == student.Studentnumber)) {
                    if (!listStudents.Any(s => s.User.NormalizedEmail == student.User.NormalizedEmail)) {
                        listStudents.Add(student);
                    }
                }
            }

            // Not totally perfect code, but he very last blok of students need to be saved,             
            if (listStudents.Count > 0 ) {
                AddProjectStudents(AProjectCollegeYear, ACollegePeriodNr, dbcontext, listStudents, strProjectteamCode, projectperiod);
            }
            dbcontext.SaveChanges();
        }

        private static void AddProjectStudents(string AProjectCollegeYear, string ACollegePeriodNr, ApplicationDbContext dbcontext, List<Student> listStudents, string strProjectteamCode, Projectperiod projectperiod) {
            var calcedId = Projectperiod.CalculateId(AProjectCollegeYear, ACollegePeriodNr, strProjectteamCode);

            var projectperiod_excist = dbcontext.Projectperiods.FirstOrDefault(p => p.Id == calcedId);
            if (projectperiod_excist == null) {
                dbcontext.Add(projectperiod);
                dbcontext.SaveChanges();
            }

            foreach (Student stud in listStudents) {
                var student_rec = dbcontext.Students.SingleOrDefault(s => s.Studentnumber == stud.Studentnumber);
                if (student_rec == null) {
                    dbcontext.Add(stud);
                    ApplicationUser.CreateUserLogin(dbcontext, stud.User); // password and claim security GUID  
                }
            }
            dbcontext.SaveChanges();

            foreach (Student stud in listStudents) {
                var ProjectStudent = new ProjectStudent() { ProjectperiodId = projectperiod.Id, StudentId = stud.Studentnumber };
                dbcontext.Add(ProjectStudent);
            }
            dbcontext.SaveChanges();
            listStudents.Clear();
        }
    }
}
