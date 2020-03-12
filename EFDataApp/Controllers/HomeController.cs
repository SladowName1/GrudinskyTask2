using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EFDataApp.Models;
using Microsoft.EntityFrameworkCore;
using CustomIdentityApp.Models;


namespace EFDataApp.Controllers
{
    public class HomeController : Controller
    {
       
        private ApplicationContext db;
        public HomeController(ApplicationContext context)
        {
            db = context;
        }
        public async Task<IActionResult> Homy()
        {
            return View(await db.Users.ToListAsync().ConfigureAwait(false));
        }
     
    }
}
