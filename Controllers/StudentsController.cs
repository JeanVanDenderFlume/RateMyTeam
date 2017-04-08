using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RateMyTeam.Data;
using RateMyTeam.Data.Models;
using RateMyTeam.ViewModels;
using AutoMapper;
using System.Dynamic;
using Microsoft.AspNetCore.Authorization;

namespace RateMyTeam.Controllers
{
   
    public class StudentsController : Controller {
        private readonly ApplicationDbContext _context;

        public StudentsController(ApplicationDbContext context) {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index() {
            var  lstStudentsViewData = await GetListStudentsViewData();
            return View(lstStudentsViewData);
        }

        public async Task<List<StudentViewModel>> GetListStudentsViewData() {
            //var config1 = new MapperConfiguration(cfg => cfg.CreateMap<Student, StudentViewModel>());
            //var mapper1 = config1.CreateMapper();

         
           

            var config_user = new MapperConfiguration(cfg => cfg.CreateMap<ApplicationUser, StudentAppUserViewModel>());
            var mapper_user = config_user.CreateMapper();


           
            var lstStudents = await _context.Students
                .Include(x => x.User)
                .Include(x => x.Mentor)                
                .ToListAsync();
         
          
            /*
            var listStudents = await _context.Students
                 .Include(x => x.User)
                 .Include(x => x.Mentor)
                 .Include(x => x.ProjectStudents)
                 .ToListAsync();

            var listProjectteams = await _context.Projectperiods               
                .Include(x => x.ProjectStudents)
                .ToListAsync();
            */

            /*
            var lstStudents = await _context.Students
              .Join(_context.Projectperiods, stud => stud.Studentnumber, prjstud => prjstud.ProjectteamCode,              
               (stud, prjstud) => new { Studentnumber = stud.Studentnumber, Projectperiod = prjstud });
              .ToListAsync();
*/


            //_context.Entry(Student).Collection(s => s.ProjectStudents).Load();
            //_context.Entry(Student).Reference(s => s.ProjectStudents).Load();


            var lstStudentsViewData = new List<StudentViewModel>();

            foreach (Student s in lstStudents) {
                //var projectperiod = s.ProjectStudents.Select(ps => ps.Projectperiod).Single();
                //StudentViewModel svm = mapper1.Map<StudentViewModel>(s);                 
                var svm_User = mapper_user.Map<StudentAppUserViewModel>(s.User);
                //var svm_Tutor = mapper3.Map<StudentAppUserViewModel>(s.Tutor);
                var svm = new StudentViewModel()
                {
                    Studentnumber = s.Studentnumber,
                    User = svm_User,
                    Mentor = s.Mentor,
                    //Projectperiod = projectperiod,
                };
                lstStudentsViewData.Add(svm);
            }
            return lstStudentsViewData;
        }


        // GET: Students/Details/5
        public async Task<IActionResult> Details(string id = null) {
            if (id == null) {
                return NotFound();
            }

            //var lstStudents = await _context.Students.ToListAsync();

            //var applicationUser = await _context.ApplicationUser.SingleOrDefaultAsync(m => m.Id == id);
            var student = await _context.Students
              .Include(x => x.User)
              .Include(x => x.Mentor)
              .Include(x => x.Mentor.User)
              .SingleOrDefaultAsync(m => m.Studentnumber == id);

            if (student == null) {
                return NotFound();
            }

            return View(student);
        }

        [Authorize(Roles = "Tutor,Planner")]
        // GET: Students/CreateWizard
        public IActionResult CreateStep1() {

            var listUsers_email = _context.Users
                .Where(p => p.Lastname != "").OrderBy(c => c.Email)
                .Select(x => new UserEmailItem { Id = x.Id, Email = x.Email }).ToList();

            var listUsers_NoStudent = new List<UserEmailItem>();
            foreach( var user_email in listUsers_email) {
                var studtest = _context.Students
                    .Include(x => x.User)
                    .SingleOrDefault(x => x.User.Id == user_email.Id);
                if( studtest == null) {
                    listUsers_NoStudent.Add(user_email);
                }
            }
            //ViewBag.ListUsers_Email = listUsers_email;

            var model = new StudentCreateStep1_ViewModel();
            model.ListUsers_Email = listUsers_NoStudent;
            return View(model);

            //var email = "jeanvandender@gmail.com";
            //return RedirectToAction("Create", "Students", new { user_email = email });            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Tutor,Planner")]
        public IActionResult CreateStep1(StudentCreateStep1_ViewModel student_step1) {

            var user_id = student_step1.SelectedUserId;

            if((user_id == "") ||(user_id == null )){
                return NotFound();
            }
            return RedirectToAction("CreateStep2", "Students", new { user_id = user_id });            
        }

        // GET
        [Authorize(Roles = "Tutor,Planner")]
        public async Task<IActionResult> CreateStep2(string user_id) {           
            if (user_id == null) {
                return NotFound();
            }
            
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Id == user_id );           
            if (user == null) {
                return NotFound();
            }
            var config_user = new MapperConfiguration(cfg => cfg.CreateMap<ApplicationUser, StudentAppUserViewModel>());
            var mapper_user = config_user.CreateMapper();
            var svm_User = mapper_user.Map<StudentAppUserViewModel>( user);
            var svm = new StudentViewModel()
            {
                Studentnumber = "",
                User = svm_User,
                Mentor = null
            };         

            return View(svm);
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Tutor,Planner")]
        public async Task<IActionResult> CreateStep2( StudentViewModel svm) {            
            StudentAppUserViewModel svm_user = svm.User;            
            if ((svm_user.Email == "") || (svm_user.Email == null)) { return NotFound(); };
            if ( (svm.Studentnumber == "") || ( svm.Studentnumber == null )){ return NotFound(); }                   

            if (ModelState.IsValid) {
                Student student = _context.Students.Where(b => b.Studentnumber == svm.Studentnumber).FirstOrDefault();  
                if ( student == null) {
                    student = await CopyToNewStudent(svm, svm.User.Email);
                    student.Studentnumber = svm.Studentnumber;
                    _context.Update(student.User);
                    await _context.SaveChangesAsync();
                    _context.Add(student);
                }
                else {
                    _context.Update(student.User);
                    await _context.SaveChangesAsync();
                    _context.Update(student);
                }                  

                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(svm);
        }

        // GET: Students/Edit/5
        [Authorize(Roles = "Tutor,Planner")]
        public async Task<IActionResult> Edit(string id = null)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(x => x.User)
                .Include(x => x.Mentor)
                .Include(x => x.Mentor.User)
                .SingleOrDefaultAsync(m => m.Studentnumber == id);
            //var applicationUser = await _context.ApplicationUser.SingleOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }
                                           
            var config_user = new MapperConfiguration(cfg => cfg.CreateMap<ApplicationUser, StudentAppUserViewModel>());
            var mapper_user = config_user.CreateMapper();
            var svm_User = mapper_user.Map<StudentAppUserViewModel>(student.User);
            //var svm_Tutor = mapper3.Map<StudentAppUserViewModel>(s.Tutor);
            var svm = new StudentViewModel()
            {
                Studentnumber = student.Studentnumber,
                User = svm_User,
                Mentor = student.Mentor
            };
            return View(svm);
        }

        private async Task<Student> CopyToNewStudent(StudentViewModel svm, string user_email ) {
            var student = new Student();
            student.User = await _context.Users.SingleOrDefaultAsync(x => x.NormalizedEmail == user_email.ToUpper());
            if( student.User == null ) { return null; };
            student.User.Email = svm.User.Email;
            student.User.Firstname = svm.User.Firstname;
            student.User.Infix = svm.User.Infix;
            student.User.Initials = svm.User.Initials;
            student.User.Lastname = svm.User.Lastname;
            return student;
        }

        private async Task<Student> CopyToStudent(StudentViewModel svm, string Studentnumber) {
           Student student;
           if (svm == null) return null;
           if (Studentnumber != "") {
                student = await _context.Students
                               .Include(x => x.User)
                               .Include(x => x.Mentor)
                               .Include(x => x.Mentor.User)
                               .SingleOrDefaultAsync(m => m.Studentnumber == Studentnumber);
                if (student == null) return null;
            }
            else return null;           

            student.User.Email = svm.User.Email;
            student.User.Firstname = svm.User.Firstname;
            student.User.Infix = svm.User.Infix;
            student.User.Initials = svm.User.Initials;
            student.User.Lastname = svm.User.Lastname;
            //var applicationUser = await _context.ApplicationUser.SingleOrDefaultAsync(m => m.Id == id);
            return student;
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Tutor,Planner")]
        public async Task<IActionResult> Edit( string id, StudentViewModel svm ) {    
            StudentAppUserViewModel svm_user = svm.User;

            if (id != svm.Studentnumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                Student student = await CopyToStudent( svm, svm.Studentnumber);
                if (student == null) {  return NotFound();  }
                
                try {
                    _context.Update(student.User );
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException){
                    if (!ApplicationUserExists(student.User.Id)){
                        return NotFound();
                    }
                    else{
                        throw;
                    }
                }

                try {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) {
                    if (!StudentExists(student.Studentnumber)) {
                        return NotFound();
                    }
                    else {
                        throw;
                    }
                }                
                return RedirectToAction("Index");
            }
            return View(svm);
        }

        // GET: Students/Delete/5
        [Authorize(Roles = "Tutor,Planner")]
        public async Task<IActionResult> Delete(string id = null )
        {
            if (id == null)
            {
                return NotFound();
            }
            var student = await _context.Students
                            .Include(x => x.User)
                            .Include(x => x.Mentor)
                            .Include(x => x.Mentor.User)
                            .SingleOrDefaultAsync(m => m.Studentnumber == id);


            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Tutor,Planner")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var student = await _context.Students.SingleOrDefaultAsync(m => m.Studentnumber == id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ApplicationUserExists(string id)
        {
            return _context.ApplicationUser.Any(e => e.Id == id);
        }

        private bool StudentExists(string id) {
            return _context.Students.Any(e => e.Studentnumber == id);
        }

    }
}
