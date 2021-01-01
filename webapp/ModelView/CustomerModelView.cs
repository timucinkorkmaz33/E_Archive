using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartAdminMvc.ModelView
{
    public class CustomerModelView
    {
        public int? Id { get; set; }
        [Display(Name = "Müşteri")]
        public int c_Id { get; set; }
        public int CustomerStatus { get; set; }

        
        [StringLength(50)]
        [Display(Name = "İsim")]
        public string c_Name { get; set; }

        
        [StringLength(50)]
        [Display(Name = "Soyisim")]
        public string c_Surname { get; set; }

        
        [StringLength(50)]
        [Display(Name = "E-Mail")]
        public string c_EMail { get; set; }

        [StringLength(11)]
        [Display(Name = "Telefon1")]
        public string c_Phone1 { get; set; }

        [StringLength(11)]
        [Display(Name = "Telefon2")]
        public string c_Phone2 { get; set; }

        [StringLength(50)]
        [Display(Name = "Müşteri Durumu")]
        public string cr_Status { get; set; }


        [Display(Name = "Bitiş Tarihi")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime cr_FinishDate { get; set; }

        [Display(Name = "Arsa")]
        public bool cr_Land { get; set; }

        [Display(Name = "Konut")]
        public bool cr_Housing { get; set; }
        [Display(Name = "Ofis")]
        public bool cr_Office { get; set; }

        [StringLength(50)]
        [Display(Name = "Şehir")]
        public string cr_Country { get; set; }

        [StringLength(50)]
        [Display(Name = "İlçe")]
        public string cr_City { get; set; }

        [Display(Name = "Fiyat Aralığı")]
        public string cr_MinPrice { get; set; }
         [Display(Name = "Fiyat Aralığı")]
        public string cr_MaxPrice { get; set; }

        [Display(Name = "m^2 Aralığı")]
        public string cr_MinArea { get; set; }
        [Display(Name = "m^2 Aralığı")]
        public string cr_MaxArea { get; set; }

        [Display(Name = "Kat Karşılığı")]
        public bool cr_FloorChange { get; set; }

        [Display(Name = "Kentsel Dönüşüm")]
        public bool cr_Transformation { get; set; }
        [Display(Name = "Notlar")]
        public string cr_Note { get; set; }
        [Display(Name = "Müşteri Adı")]
        public string CustomerName { get; set; }
    }
}