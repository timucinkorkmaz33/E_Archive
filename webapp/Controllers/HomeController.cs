#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using SmartAdminMvc.Entity;
using SmartAdminMvc.ModelView;
using System.Web.Configuration;

#endregion

namespace SmartAdminMvc.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ProjectDb db = new ProjectDb();

        // GET: home/index

        public ActionResult Index()
        {
            var licence = WebConfigurationManager.AppSettings["ProductKey"];
            var licencecontrol = Licence.GetLicenseCode();
            if (licence != licencecontrol)
            {
                return View("Licences");
            }
            return View();
        }
      
        public ActionResult Social()
        {
            return View();
        }

        // GET: home/inbox
        public ActionResult Inbox()
        {
            return View();
        }

        // GET: home/widgets
        public ActionResult Widgets()
        {
            return View();
        }

        // GET: home/chat
        public ActionResult Chat()
        {
            return View();
        }

        //private void AddBanksDropDownList()
        //{
        //    var company = db.Portfolio_Banks.ToList();

        //    List<SelectListItem> listbanks = new List<SelectListItem>();

        //    if (company != null)
        //    {
        //        foreach (Portfolio_Banks bank in company)
        //        {

        //            listbanks.Add(new SelectListView
        //            {
        //                _Text = bank.BankName,
        //                _Value = bank.Id
        //            });
        //        }

        //    }
        //    ViewBag.Banks = listbanks;
        //}
    }
}