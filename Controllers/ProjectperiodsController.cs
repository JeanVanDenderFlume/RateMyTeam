using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RateMyTeam.Data;
using RateMyTeam.Data.Models;

namespace RateMyTeam.Controllers
{
    public class ProjectperiodsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProjectperiodsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Projectperiods
        public async Task<IActionResult> Index()
        {
            return View(await _context.Projectperiods.OrderByDescending( p => p.Id ).ToListAsync() );
        }

        


        // GET: Projectperiods/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectperiod = await _context.Projectperiods
                .SingleOrDefaultAsync(m => m.Id == id);
            if (projectperiod == null)
            {
                return NotFound();
            }

            var studentsCtrl = new StudentsController(_context);
            var listStudentsViewData = await studentsCtrl.GetListStudentsViewData();
            projectperiod.listStudentsViewData = listStudentsViewData;

            return View(projectperiod);
        }

        // GET: Projectperiods/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Projectperiods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CollegeLearningYear,CollegePeriodNr,CollegePeriod_Startdate,CollegePeriod_Enddate,ProjectteamCode,Title,Description,RatingInput_OpeningDatetime,RatingInput_ClosingDatetime,Created_at,Updated_at")] Projectperiod projectperiod)
        {
            if (ModelState.IsValid)
            {
                _context.Add(projectperiod);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(projectperiod);
        }

        // GET: Projectperiods/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectperiod = await _context.Projectperiods.SingleOrDefaultAsync(m => m.Id == id);
            if (projectperiod == null)
            {
                return NotFound();
            }
            return View(projectperiod);
        }

        // POST: Projectperiods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,CollegeLearningYear,CollegePeriodNr,CollegePeriod_Startdate,CollegePeriod_Enddate,ProjectteamCode,Title,Description,RatingInput_OpeningDatetime,RatingInput_ClosingDatetime,Created_at,Updated_at")] Projectperiod projectperiod)
        {
            if (id != projectperiod.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(projectperiod);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectperiodExists(projectperiod.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(projectperiod);
        }

        // GET: Projectperiods/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectperiod = await _context.Projectperiods
                .SingleOrDefaultAsync(m => m.Id == id);
            if (projectperiod == null)
            {
                return NotFound();
            }

            return View(projectperiod);
        }

        // POST: Projectperiods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var projectperiod = await _context.Projectperiods.SingleOrDefaultAsync(m => m.Id == id);
            _context.Projectperiods.Remove(projectperiod);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ProjectperiodExists(string id)
        {
            return _context.Projectperiods.Any(e => e.Id == id);
        }
    }
}
