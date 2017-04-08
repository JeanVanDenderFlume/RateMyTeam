using Microsoft.AspNetCore.Mvc;
using RateMyTeam.Controllers;
using RateMyTeam.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RateMyTeam.ViewComponents
{
    public class ProjectStudentsViewComponent : ViewComponent
    {
        private ApplicationDbContext _dbcontext;
        //private ApplicationDbContext _dbcontext = new ApplicationDbContext<MyContext>();

        public ProjectStudentsViewComponent(ApplicationDbContext ADBContext) {
            _dbcontext = ADBContext;           
        }

        public IViewComponentResult Invoke() {
            //MyContext context = new MyContext(db);
            //IEnumerable<tableRowClass> mc = await context.tableRows.ToListAsync();
            var studctrl = new StudentsController(_dbcontext);
            return null;
            //return await studctrl.Index();
        }
    }
}
