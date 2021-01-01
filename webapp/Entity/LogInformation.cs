using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartAdminMvc.Entity
{
    public class LogInformation
    {
        [Display(Name = "Ic")]
        public int? Id { get; set; }
      
        [Display(Name = "UserAgent")]
        public string UserAgent { get; set; }

        [Display(Name = "Ip Bilgisi")]
        public string Ip { get; set; }

        [Display(Name = "Giriş Zamanı")]
        public DateTime Date { get; set; }

    }
}