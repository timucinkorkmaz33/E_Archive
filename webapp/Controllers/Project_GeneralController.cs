using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SmartAdminMvc.Entity;
using System.IO;
using System.IO.Compression;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SmartAdminMvc.Identity;
using SmartAdminMvc.App_Start;
using Ionic.Zip;
using SmartAdminMvc.ModelView;
using System.Web.Configuration;
using System.Net.Mail;
using System.Text;
using System.Data.Entity.Validation;

namespace SmartAdminMvc.Controllers
{
    [Authorize]
    public class Project_GeneralController : Controller
    {
        private ProjectDb db = new ProjectDb();
        private UserManager<ApplicationUser> userManager;

        public Project_GeneralController()
        {
            UserStore<ApplicationUser> userStore = new UserStore<ApplicationUser>(db);
            userManager = new UserManager<ApplicationUser>(userStore);
        }
        // GET: Project_General
        public ActionResult Index()
        {
            var licence = WebConfigurationManager.AppSettings["ProductKey"];
            var licencecontrol = Licence.GetLicenseCode();
            if (licence != licencecontrol)
            {
                return View("Licences");
            }
            string currentUserId = User.Identity.GetUserId();
            var username = db.Users.Where(u => u.Id == currentUserId).Select(u => u.UserName).FirstOrDefault();
            IList<string> roleNames = userManager.GetRoles(currentUserId);
            if (roleNames[0] == "User")
            {
                var sorgu = db.Project_General.Where(u => u.Owner == username);
                return View(sorgu);
            }
            else
            {
                return View(db.Project_General);
            }
            return View();
        }

        // GET: Project_General/Details/5
        [HttpGet]
        public ActionResult UploadFiles()
        {

            return View();
        }

        public ActionResult ProjectDetail(int id)//Dosyaya yuklenen verilerin gosterilmesi
        {

            var sorgu = db.Project_General.FirstOrDefault(u => u.Id == id);
           // var resimler = sorgu.ImagePath.Split(';');
            //var belgeler = sorgu.Document.Split(';');
            var dokumanlar = sorgu.Folder.Split(';');

            var projectModel = new Project_General();

            //foreach (var item in resimler)
            //{
            //    if (item != "")
            //    {
            //        //var imageFiles = Directory.GetFiles(Server.MapPath(item));
            //        projectModel.ImageList.Add(Path.GetFileName(item));
            //    }
            //}
            foreach (var item in dokumanlar)
            {
                if (item != "")
                {
                    //var imageFiles = Directory.GetFiles(Server.MapPath(item));
                    projectModel.DocList.Add(Path.GetFileName(item));
                }
            }
            //foreach (var item in belgeler)
            //{
            //    if (item != "")
            //    {
            //        //var imageFiles = Directory.GetFiles(Server.MapPath(item));
            //        projectModel.FolderList.Add(Path.GetFileName(item));
            //    }
            //}
            projectModel.Name = sorgu.Name;
            projectModel.StartDate = sorgu.StartDate;
            projectModel.Description = sorgu.Description;
            projectModel.Owner = sorgu.Owner;
            projectModel.CompanyName = sorgu.CompanyName;


            return View(projectModel);

        }

        [HttpPost]
        public ActionResult UploadFilesMethod()
        {
            if (Request.Files.Count != 0)
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFileBase file = Request.Files[i];
                    int fileSize = file.ContentLength;
                    string fileName = file.FileName;
                    file.SaveAs(Server.MapPath("~/Upload_Files/" + fileName));
                    //ImageGallery imageGallery = new ImageGallery();
                    //imageGallery.ID = Guid.NewGuid();
                    //imageGallery.Name = fileName;
                    //imageGallery.ImagePath = "~/Upload_Files/" + fileName;
                    //db.ImageGallery.Add(imageGallery);
                    //db.SaveChanges();
                }
                return Content("Success");
            }
            return Content("failed");
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project_General project_General = db.Project_General.Find(id);
            if (project_General == null)
            {
                return HttpNotFound();
            }
            return View(project_General);
        }


        // GET: Project_General/Create
        public ActionResult Create()
        {
            AddItemDropDownList();
            var licence = WebConfigurationManager.AppSettings["ProductKey"];
            var licencecontrol = Licence.GetLicenseCode();
            if (licence != licencecontrol)
            {
                return View("Licences");
            }

            return View();
        }

        // POST: Project_General/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Project_General project_General, IEnumerable<HttpPostedFileBase> ImagePath, IEnumerable<HttpPostedFileBase> Folder, IEnumerable<HttpPostedFileBase> Document)
        {

            if (ModelState.IsValid)
            {
                var useridentity = project_General.Owner.Split('-');
                var company = useridentity[0].Trim();
                var user = useridentity[1].Trim();
                var username = user.Split(' ');
                var x = username[0].Trim();
                var sorguusername = db.Users.Where(u => u.Name == x && u.CompanyName == company).Select(u => u.UserName).FirstOrDefault();

                Project_General pg = new Project_General();
                string path = Server.MapPath("~/UploadFiles/" + company + "/" +project_General.StartDate.Value.Year+"/" +project_General.StartDate.Value.Month+"/"+ project_General.StartDate.Value.Day+"/"+ project_General.Name);
                Directory.CreateDirectory(path);
                //  var usercontrol = db.Users.Where(u => u.UserName == project_General.UserName);
                //if (usercontrol.Count() != 0)
                //{
                //    TempData["userMessage"] = "Lütfen kullanıcı adınız değiştiriniz";
                //    return View(project_General);
                //}
                // ApplicationUser user = new ApplicationUser();

                //                user.UserName = project_General.UserName;
                //            
                //          user.Email = "automatic@gmail.com";
                //IdentityResult iResult = userManager.Create(user, project_General.Pass);
                //if (iResult.Succeeded)
                //{
                //    userManager.AddToRole(user.Id, "User");
                //}
                //else
                //{
                //    return View(project_General);
                //}


                if (project_General.ImagePath != null || project_General.Folder != null || project_General.Document != null)
                {
                    //foreach (var file in ImagePath)
                    //{
                    //    if (file != null)
                    //    {

                    //        var filename = Path.GetFileName(file.FileName);
                    //        var kayityeri = Path.Combine(path, filename);
                    //        pg.ImagePath += "/UploadFiles/" + company + "/" + project_General.Name + "/" + filename + ";";

                    //        file.SaveAs(kayityeri);
                    //    }
                    //    else
                    //    {
                    //        pg.ImagePath += "";
                    //    }
                    //}

                    var zippath= Server.MapPath("~/UploadFiles/" + company + "/" + project_General.StartDate.Value.Year + "/" + project_General.StartDate.Value.Month + "/" + project_General.StartDate.Value.Day + "/" + project_General.Name+"/"+"TümDosyalar.zip");

                    using (ZipArchive archive = System.IO.Compression.ZipFile.Open(zippath, ZipArchiveMode.Update))
                    {
                          foreach (var file in Folder)
                        {
                            if (file != null)
                            {

                                var filename = Path.GetFileName(file.FileName);
                                var kayityeri = Path.Combine(path, filename);
                                pg.Folder += "/UploadFiles/" + company + "/" + project_General.StartDate.Value.Year + "/" + project_General.StartDate.Value.Month + "/" + project_General.StartDate.Value.Day + "/" + project_General.Name + "/" + filename + ";";

                                file.SaveAs(kayityeri);
                                archive.CreateEntryFromFile(kayityeri, filename);
                                System.IO.File.Delete(kayityeri);
                               
                               
                                // archive.ExtractToDirectory(extractPath);
                            }
                            else
                            {
                                pg.Folder += "";
                            }
                        }
                       
                    }
                    pg.Folder = zippath;
                    //foreach (var file in Folder)
                    //{
                    //    if (file != null)
                    //    {

                    //        var filename = Path.GetFileName(file.FileName);
                    //        var kayityeri = Path.Combine(path, filename);
                    //        pg.Folder += "/UploadFiles/" + company + "/" + project_General.StartDate.Value.Year + "/" + project_General.StartDate.Value.Month + "/" + project_General.StartDate.Value.Day + "/" + project_General.Name + "/" + filename + ";";

                    //        file.SaveAs(kayityeri);
                    //    }
                    //    else
                    //    {
                    //        pg.Folder += "";
                    //    }
                    //}
                    //foreach (var file in Document)
                    //{
                    //    if (file != null)
                    //    {

                    //        var filename = Path.GetFileName(file.FileName);
                    //        var kayityeri = Path.Combine(path, filename);
                    //        pg.Document += "/UploadFiles/" + company + "/" + project_General.StartDate.Value.Year + "/" + project_General.StartDate.Value.Month + "/" + project_General.StartDate.Value.Day + "/" + project_General.Name + "/" + filename + ";";


                    //        file.SaveAs(kayityeri);
                    //    }
                    //    else
                    //    {
                    //        pg.Document += "";
                    //    }
                    //}
                }
                TempData["success"] = "Lütfen kullanıcı adınız değiştiriniz";
                pg.Owner = sorguusername;
              
                pg.Description = project_General.Description;
                pg.StartDate = project_General.StartDate;
                pg.Name = project_General.Name;
                pg.CompanyName =company;
                db.Project_General.Add(pg);
                try
                {
                    db.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var entityValidationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in entityValidationErrors.ValidationErrors)
                        {
                            Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                        }
                    }
                }
                //////////////////////////////////////////////////////E-Mail Sistemi
                //MailMessage eposta = new MailMessage();
                //var mailAdres= db.Users.Where(u => u.Name == x && u.CompanyName == company).Select(u => u.Email).FirstOrDefault();

                //eposta.From = new MailAddress("earsiv@kaplanlargumrukleme.com", "Kaplanlar Gumrukleme E-Arsiv Bilgi Mesajı");
                //eposta.To.Add(mailAdres);

                //eposta.Subject = "Kaplanlar Gumrukleme E-Arsiv Bilgi Mesajı";
                //eposta.IsBodyHtml = true;
                //eposta.Body = "Sayın "+user+","+"<br>"+"Kaplanlar Gumruk Müşavirliği E-Arşiv Sistemine, firmaniza ait"+project_General.Name +" beyanname numaralı belgeler eklenmiştir.";
                //eposta.BodyEncoding = Encoding.UTF8;


                //eposta.IsBodyHtml = true;
                //SmtpClient smtp = new SmtpClient();
                //smtp.Credentials = new System.Net.NetworkCredential("earsiv@kaplanlargumrukleme.com", "Kplnlr.2017");
                //smtp.Port = 587;
                //smtp.Host = "mail.kaplanlargumrukleme.com";
                //smtp.EnableSsl = false;
                //object userstate = eposta;
                //smtp.Send(eposta);
                //Response.Write("<script>alert('Mesajınız Gönderilmiştir')</script>");
                ////////////////////////////////////////////////////////E-Mail Sistemi Sonu


                Response.Write("<script>alert('Yükleme işlemi basarılı.')</script>");
                return RedirectToAction("index");
            }
            Response.Write("<script>alert('Yükleme işlemi başarısız.')</script>");
            return View(project_General);
        }
        //public ActionResult Download()
        //{
        //    using (ZipFile zip = new ZipFile())
        //    {
        //        zip.AlternateEncodingUsage = ZipOption.AsNecessary;
        //        zip.AddDirectoryByName("Files");

        //        string path = Server.MapPath("~/UploadFiles/www");
        //       // string filePath = "~/UploadFiles/www";
        //        zip.AddFile(path, "Files");


        //        Response.Clear();
        //        Response.BufferOutput = false;
        //        string zipName = String.Format("Zip_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
        //        Response.ContentType = "application/zip";
        //        Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
        //        zip.Save(Response.OutputStream);
        //        Response.End();
        //    }
        //    return View("Index");
        //}

        // GET: Project_General/Edit/5
        public ActionResult Edit(int? id)
        {
            var licence = WebConfigurationManager.AppSettings["ProductKey"];
            var licencecontrol = Licence.GetLicenseCode();
            if (licence != licencecontrol)
            {
                return View("Licences");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project_General project_General = db.Project_General.Find(id);
            if (project_General == null)
            {
                return HttpNotFound();
            }


            var sorgu = db.Project_General.FirstOrDefault(u => u.Id == id);
          //  var resimler = sorgu.ImagePath.Split(';');
           // var belgeler = sorgu.Document.Split(';');
            var dokumanlar = sorgu.Folder.Split(';');

            var projectModel = new Project_General();

            //foreach (var item in resimler)
            //{
            //    if (item != "")
            //    {
            //        //var imageFiles = Directory.GetFiles(Server.MapPath(item));
            //        projectModel.ImageList.Add(Path.GetFileName(item));
            //    }
            //}
            foreach (var item in dokumanlar)
            {
                if (item != "")
                {
                    //var imageFiles = Directory.GetFiles(Server.MapPath(item));
                    projectModel.DocList.Add(Path.GetFileName(item));
                }
            }
            //foreach (var item in belgeler)
            //{
            //    if (item != "")
            //    {
            //        //var imageFiles = Directory.GetFiles(Server.MapPath(item));
            //        projectModel.FolderList.Add(Path.GetFileName(item));
            //    }
            //}
            projectModel.Name = sorgu.Name;
            projectModel.StartDate = sorgu.StartDate;
            projectModel.Description = sorgu.Description;
            projectModel.Owner =sorgu.Owner;
            projectModel.CompanyName = sorgu.CompanyName;


            return View(projectModel);
        }

        // POST: Project_General/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Project_General project_General, IEnumerable<HttpPostedFileBase> ImagePath, IEnumerable<HttpPostedFileBase> Folder, IEnumerable<HttpPostedFileBase> Document)
        {
            if (ModelState.IsValid)
            {
                var pg = db.Project_General.Where(u => u.Id == project_General.Id).FirstOrDefault();
                pg.Name = project_General.Name;
                pg.StartDate = project_General.StartDate;
                pg.Description = project_General.Description;
                pg.Owner = project_General.Owner;
                var sorgucompany = db.Users.Where(u => u.UserName == project_General.Owner).Select(u => u.CompanyName).FirstOrDefault();
                pg.CompanyName = sorgucompany;
                string path = Server.MapPath("~/UploadFiles/" +sorgucompany+"/"+ project_General.StartDate.Value.Year + "/" + project_General.StartDate.Value.Month + "/" + project_General.StartDate.Value.Day + "/" + project_General.Name);
                Directory.CreateDirectory(path);
                if (project_General.ImagePath != null || project_General.Folder != null || project_General.Document != null)
                {
                    //foreach (var file in ImagePath)
                    //{
                    //    if (file != null)
                    //    {

                    //        var filename = Path.GetFileName(file.FileName);
                    //        var kayityeri = Path.Combine(path, filename);
                    //        pg.ImagePath += "/UploadFiles/" + sorgucompany + "/" + project_General.Name + "/" + filename + ";";

                    //        file.SaveAs(kayityeri);
                    //    }
                    //    else
                    //    {
                    //        pg.ImagePath += "";
                    //    }
                    //}
                    var zippath = Server.MapPath("~/UploadFiles/" + sorgucompany + "/" + project_General.StartDate.Value.Year + "/" + project_General.StartDate.Value.Month + "/" + project_General.StartDate.Value.Day + "/" + project_General.Name + "/" + "TümDosyalar.zip");

                    using (ZipArchive archive = System.IO.Compression.ZipFile.Open(zippath, ZipArchiveMode.Update))
                    {
                        foreach (var file in Folder)
                        {
                            if (file != null)
                            {

                                var filename = Path.GetFileName(file.FileName);
                                var kayityeri = Path.Combine(path, filename);
                                pg.Folder += "/UploadFiles/" + sorgucompany + "/" + project_General.StartDate.Value.Year + "/" + project_General.StartDate.Value.Month + "/" + project_General.StartDate.Value.Day + "/" + project_General.Name + "/" + filename + ";";

                                file.SaveAs(kayityeri);
                                archive.CreateEntryFromFile(kayityeri, filename);
                                System.IO.File.Delete(kayityeri);


                                // archive.ExtractToDirectory(extractPath);
                            }
                            else
                            {
                                pg.Folder += "";
                            }
                        }

                    }
                    pg.Folder = zippath;
                    //foreach (var file in Document)
                    //{
                    //    if (file != null)
                    //    {

                    //        var filename = Path.GetFileName(file.FileName);
                    //        var kayityeri = Path.Combine(path, filename);
                    //        pg.Document += "/UploadFiles/" + sorgucompany + "/" + project_General.StartDate.Value.Year + "/" + project_General.StartDate.Value.Month + "/" + project_General.StartDate.Value.Day + "/" + project_General.Name + "/" + filename + ";";


                    //        file.SaveAs(kayityeri);
                    //    }
                    //    else
                    //    {
                    //        pg.Document += "";
                    //    }
                    //}
                }
                db.SaveChanges();
                TempData["success"] = "Lütfen kullanıcı adınız değiştiriniz";
                Response.Write("<script>alert('Yükleme işlemi basarılı.')</script>");
                return RedirectToAction("Index");
            }
            Response.Write("<script>alert('Yükleme işlemi başarısız.')</script>");
            return View(project_General);
        }

        // GET: Project_General/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project_General project_General = db.Project_General.Find(id);
            if (project_General == null)
            {
                return HttpNotFound();
            }
            return View(project_General);
        }

        // POST: Project_General/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project_General project_General = db.Project_General.Find(id);
            db.Project_General.Remove(project_General);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private void AddItemDropDownList()
        {

            var kullanici = userManager.Users.Where(x => x.UserName != "dora"&& x.Active=="1").ToList();
            // var deneme =roleManager.Roles.Select(a=>userManager.Users.Where(x => x.Name!="dora")).ToList();
            // var sq = db.Portfolio_Personal.Select(x => x.UserId).ToList();

            List<SelectListItem> personel = new List<SelectListItem>();

            if (kullanici != null)
            {
                foreach (ApplicationUser person in kullanici)
                {
                    personel.Add(new SelectListView
                    {
                        _Text = person.CompanyName + " - " + person.Name.ToUpper() + " " + person.Surname.ToUpper(),
                        _strValue = person.UserName
                    });
                }

            }
            ViewBag.Personel = personel;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
