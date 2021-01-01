using SmartAdminMvc.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartAdminMvc.Controllers
{
    public class LogController : Controller
    {
        ProjectDb db = new ProjectDb();
        // GET: Log
        [Authorize(Roles = "AppAdmin,Admin")]
        public ActionResult Index()
        {
            var sorgu = db.LogInformation.Select(u => u).OrderByDescending(u=>u.Id).ToList();
            return View(sorgu);
        }
    }
}