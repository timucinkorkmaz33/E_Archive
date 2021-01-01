#region Using

using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using SmartAdminMvc.Entity;
using SmartAdminMvc.Identity;
using SmartAdminMvc.Models;
using SmartAdminMvc.ModelView;
using System.IO;
using CaptchaMvc.HtmlHelpers;
using Newtonsoft.Json.Linq;

#endregion

namespace SmartAdminMvc.Controllers
{

    public class AccountController : Controller
    {
        ProjectDb db = new ProjectDb();
        private UserManager<ApplicationUser> userManager;

        private RoleManager<ApplicationRole> roleManager;
        public AccountController()
        {
            ProjectDb db = new ProjectDb();
            UserStore<ApplicationUser> userStore = new UserStore<ApplicationUser>(db);
            userManager = new UserManager<ApplicationUser>(userStore);
            RoleStore<ApplicationRole> roleStore = new RoleStore<ApplicationRole>(db);
            roleManager = new RoleManager<ApplicationRole>(roleStore);
            // IQueryable<Account> xx = db.Account.Select(x => x);
        }

        [Authorize(Roles = "AppAdmin,Admin")]
        public ActionResult Index()
        {
            //var sorgu = from c in db.Account
            //    select new Register()
            //    {
            //        Username = c.UserName,
            //        Name = c.NameSurname,
            //        Surname = c.NameSurname,
            //        Email = c.EMail,
            //        Password = c.Password,
            //        Id=c.Id

            //    };
            var userName = User.Identity.GetUserName();
            //var userId = User.Identity.GetUserId();
            //var role = roleManager.Roles.Select(u => u.).ToList();

            // List<ApplicationUser> iresult = userManager.Users.Where(x => x.UserName!="dora").ToList();
            if (userName == "dora")
            {
                List<ApplicationUser> iresult = userManager.Users.Select(x => x).Where(u=>u.Active=="1").ToList();
                return View(iresult.ToList());
            }
            else
            {
                List<ApplicationUser> iresult = userManager.Users.Where(x => x.UserName != "dora" && x.Active=="1").ToList();
                return View(iresult.ToList());
            }
            //IQueryable<ApplicationRole> vv = roleManager.Roles.Select(x => x);

            //  AccountRegistrationModel_Custom ac=new AccountRegistrationModel_Custom();



            //  string currentUserId = User.Identity.GetUserId();
            //  ApplicationUser currentUser = userManager.FindById(currentUserId);

            return View();
        }
        [Authorize(Roles = "AppAdmin,Admin")]
        public ActionResult Register()
        {
            AddItemDropDownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "AppAdmin,Admin")]
        public ActionResult Register(Register model)
        {
            if (ModelState.IsValid)
            {
                //Account acc=new Account();
                //acc.EMail = model.Email;
                //acc.NameSurname = model.Name + model.Surname;
                //acc.Password = model.Password;
                //acc.UserName = model.Username;
                //acc.Task = "Admin";
                //db.Account.Add(acc);
                //db.SaveChanges();
                //return RedirectToAction("Login", "Account");
                var usercontrol = db.Users.Where(u => u.UserName == model.Username);
                if (usercontrol.Count() != 0)
                {
                    AddItemDropDownList();
                    TempData["userMessage"] = "Lütfen kullanıcı adınız değiştiriniz";
                    return View(model);
                }
                //var emailcontrol = db.Users.Where(u => u.Email == model.Email);
                //if (emailcontrol.Count() != 0)
                //{
                //    AddItemDropDownList();
                //    TempData["emailMessage"] = "Lütfen email adresini değiştiriniz";
                //    return View(model);
                //}

                ApplicationUser user = new ApplicationUser();
                user.Name = model.Name;
                user.Surname = model.Surname;
                user.Email = model.Email;
                user.UserName = model.Username;
                user.CompanyName = model.CompanyName;
                user.Active = "1";
                user.IpAddress = model.IpAddress;


                IdentityResult iResult = userManager.Create(user, model.Password);
                if (iResult.Succeeded)
                {

                    userManager.AddToRole(user.Id, model.Roles);
                    string path = Server.MapPath("~/UploadFiles/" + user.CompanyName);
                    Directory.CreateDirectory(path);
                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    ModelState.AddModelError("RegisterUser", "Kullanıcı ekleme işleminde hata!");
                }

            }
            AddItemDropDownList();
            return View(model);

        }
        [Authorize(Roles = "Editor,User")]
        public ActionResult ChangePassword(string id)
        {
            var username = User.Identity.Name;
            var userid = db.Users.Where(u => u.UserName == username).Select(u => u).FirstOrDefault().Id;

            Register user = new Register();
            var sonuc = userManager.FindById(userid);

            user.Username = sonuc.UserName;
            user.Name = sonuc.Name;
            user.Email = sonuc.Email;
            user.Surname = sonuc.Surname;
            user.CompanyName = sonuc.CompanyName;
            user.IpAddress = sonuc.IpAddress;
            //user.Password = sonuc.PasswordHash;
            user.Id = userid;

            //user.Roles=sonuc.Roles.Where(u=>u)
            return View(user);
        }
        [HttpPost]
        [Authorize(Roles = "Editor,User")]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(Register model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = userManager.FindById(model.Id);
                user.Email = model.Email;
                user.UserName = model.Username;
                user.Name = model.Name;
                user.Surname = model.Surname;
                user.IpAddress = model.IpAddress;
                userManager.Update(user);
                if (!model.Password.Equals(user.PasswordHash))
                {

                    var removeresult = userManager.RemovePassword(model.Id);
                    if (removeresult.Succeeded)
                    {
                        var sonuc1 = userManager.AddPassword(model.Id, model.Password);

                        if (sonuc1.Succeeded)
                        {
                            Response.Write("<script>alert('Şifre Değiştirme işlemi basarılı.')</script>");
                            return RedirectToAction("index", "home");

                        }
                        else
                        {
                            Response.Write("<script>alert('Şifre Değiştirme işleminde hata.')</script>");
                            return View(model);
                        }
                    }

                }


                return RedirectToAction("index", "Account");

            }

            return View(model);
        }
        [Authorize(Roles = "AppAdmin,Admin")]
        public ActionResult EditUsers(string id)
        {

            Register user = new Register();
            var sonuc = userManager.FindById(id);

            user.Username = sonuc.UserName;
            user.Name = sonuc.Name;
            user.Email = sonuc.Email;
            user.Surname = sonuc.Surname;
            //user.Password = sonuc.PasswordHash;
            user.Id = id;
            //user.Roles=sonuc.Roles.Where(u=>u)
            //  user.ConfirmPassword = sonuc.Password;
            //ApplicationUser user = userManager.Users.Where(x => x.Id==id).FirstOrDefault();
            return View(user);
        }

        [HttpPost]
        [Authorize(Roles = "AppAdmin,Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult EditUsers(Register model)
        {
            if (ModelState.IsValid)
            {
                //var usercontrol = db.Users.Where(u => u.UserName == model.Username);
                //if (usercontrol.Count() != 0)
                //{
                //    AddItemDropDownList();
                //    TempData["userMessage"] = "Lütfen kullanıcı adınız değiştiriniz";
                //    return View(model);
                //}
                //var emailcontrol = db.Users.Where(u => u.Email == model.Email);
                //if (emailcontrol.Count() != 0)
                //{
                //    AddItemDropDownList();
                //    TempData["emailMessage"] = "Lütfen email adresini değiştiriniz";
                //    return View(model);
                //}
                ApplicationUser user = userManager.FindById(model.Id);
                user.Email = model.Email;
                user.UserName = model.Username;
                user.Name = model.Name;
                user.Surname = model.Surname;
                userManager.Update(user);
                if (!model.Password.Equals(user.PasswordHash))
                {
                    //var sonuc=userManager.Users.Where(i=>i.UserName==model.Username).Select(x=>x.PasswordHash);

                    var removeresult = userManager.RemovePassword(model.Id);
                    if (removeresult.Succeeded)
                    {
                        var sonuc1 = userManager.AddPassword(model.Id, model.Password);

                        if (sonuc1.Succeeded)
                        {

                            return RedirectToAction("index", "Account");
                        }
                        else
                        {
                            return View(model);
                        }
                    }

                }

                //ApplicationUser user = userManager.Users.Where(x => x.Id == model.Id).FirstOrDefault();
                //user.Name = model.Name;
                //user.Surname = model.Surname;
                //user.PasswordHash = model.Password;
                //user.Email = model.Email;
                //user.UserName = model.Username;

                //userManager.Users.a

                return RedirectToAction("index", "Account");

            }

            return View(model);

        }

        [Authorize(Roles = "AppAdmin,Admin")]
        public async Task<ActionResult> DeleteUser(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            Register user = new Register();
            var sonuc = userManager.FindById(id);
            user.Username = sonuc.UserName;
            user.Name = sonuc.Name;
            user.Email = sonuc.Email;
            user.Surname = sonuc.Surname;
            user.Password = sonuc.PasswordHash;
            user.Id = id;



            if (user != null)
            {
                return View(user);

            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


        }

        [Authorize(Roles = "AppAdmin,Admin")]
        [HttpPost, ActionName("DeleteUser")]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> DeleteConfirmed(Register model)
        {


            ApplicationUser user = userManager.FindById(model.Id);


            if (user != null)
            {

                user.Active = "0";

                userManager.Update(user);

                return RedirectToAction("Logout");
            }
            else
            {
                return View(model);
            }


            return RedirectToAction("Logout");
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            //string currentUserId = User.Identity.GetUserId();
            //ApplicationUser currentUser = userManager.FindById(currentUserId);
            var useragent = Request.UserAgent;
            var ipAddress = string.Empty;
            if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                ipAddress = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_CLIENT_IP"] != null )
        {
                ipAddress = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_CLIENT_IP"];
            }
        else if (System.Web.HttpContext.Current.Request.UserHostAddress.Length != 0)
            {
                ipAddress = System.Web.HttpContext.Current.Request.UserHostName;
            }
            var date = DateTime.Now;
            if (db.LogInformation.Where(u => u.Ip == ipAddress).Count() != 0)
            {
                var sorgu = db.LogInformation.Where(u => u.Ip == ipAddress).OrderByDescending(u => u.Id).FirstOrDefault().Date;
                if (sorgu != null)
                {
                    var farksaat = date.Hour - sorgu.Hour;
                    var farkdk = date.Minute - sorgu.Minute;

                    if (farksaat < 2 || farkdk < 5)
                    {

                        LogInformation log = new LogInformation();
                        log.UserAgent = useragent;
                        log.Ip = ipAddress;
                        log.Date = date;
                        db.LogInformation.Add(log);
                        db.SaveChanges();
                    }
                }else
                {
                    LogInformation log = new LogInformation();
                    log.UserAgent = useragent;
                    log.Ip = ipAddress;
                    log.Date = date;
                    db.LogInformation.Add(log);
                    db.SaveChanges();
                }
       
            }
            else
            {
                LogInformation log = new LogInformation();
                log.UserAgent = useragent;
                log.Ip = ipAddress;
                log.Date = date;
                db.LogInformation.Add(log);
                db.SaveChanges();
            }

            return View();

        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login model)
        {
            if (ModelState.IsValid)
            {
                //Validate Google recaptcha here
                var response = Request["g-recaptcha-response"];
                string secretKey = "6LdEMToUAAAAAH8TjQSgLRauNLuEUfPlINtO84q4";
                var client = new WebClient();
                var result = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secretKey, response));
                var obj = JObject.Parse(result);
                var status = (bool)obj.SelectToken("success");
                status = true;

                ViewBag.Message = status ? "Güvenlik Kodu Dogrulandı" : "Güvenlik Kodu Hatası";
                if (status==true)
                {
                    ApplicationUser user = userManager.Find(model.Username, model.Password);
                    if (user != null)
                    {
                        IAuthenticationManager authManager = HttpContext.GetOwinContext().Authentication;
                        ClaimsIdentity identity = userManager.CreateIdentity(user, "ApplicationCookie");
                        AuthenticationProperties authProps = new AuthenticationProperties();
                        authProps.IsPersistent = model.RememberMe;
                        authManager.SignIn(authProps, identity);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("LoginUser", "Böyle bir kullanıcı bulunamadı");
                    }
                }
               
            }
            return View(model);
        }
        private void AddItemDropDownList()
        {
            var kullanici = roleManager.Roles.Select(x => x).ToList();

            List<SelectListItem> roles = new List<SelectListItem>();

            if (kullanici != null)
            {
                foreach (ApplicationRole person in kullanici)
                {
                    roles.Add(new SelectListView
                    {
                        _Text = person.Name,
                        _strValue = person.Name
                    });
                }

            }
            ViewBag.Roles = roles;
        }

        public ActionResult Logout()
        {
            IAuthenticationManager authManager = HttpContext.GetOwinContext().Authentication;
            FormsAuthentication.SignOut();
            Session.Abandon();
            Session.Clear();
            authManager.SignOut();
            return RedirectToAction("login", "Account");

        }


    }
}