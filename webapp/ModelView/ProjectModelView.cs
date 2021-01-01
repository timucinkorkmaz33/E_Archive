using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartAdminMvc.ModelView
{
    public class ProjectModelView
    {
        public int? Id { get; set; }
        public int p_Id { get; set; }

        [StringLength(50)]
        [Display(Name = "Proje Tipi")]
        public string p_Type { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Proje Oranı")]

        public string p_Rate { get; set; }
        [StringLength(50)]
        [Display(Name = "Proje Durumu")]
        public string p_ProjectStatus { get; set; }
        [StringLength(50)]
        [Required]
        [Display(Name = "Tamamlanma Tarihi")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public string p_FinishDate { get; set; }
        [Required]

        [Display(Name = "Firma")]
        public int? p_Company_Id { get; set; }
        [StringLength(50)]
        [Required]
        [Display(Name = "Başlık")]
        public string p_Header { get; set; }
        [StringLength(50)]
        [Display(Name = "Özel Başlık")]
        public string p_Special_Header { get; set; }
        [StringLength(50)]
        [Display(Name = "Açıklama")]
        public string p_Project_Description { get; set; }

        [StringLength(50)]
        
        [Display(Name = "Firma Adı")]
        public string c_Company_Name { get; set; }
      
        [Display(Name = "Firma Yetkilisi")]
        public string c_Owner_NameSurname { get; set; }
        [Display(Name = "Ünvanı")]
        public string c_Owner_Title { get; set; }
        [Display(Name = "Yetkili E-Mail")]
       
        public string c_Owner_EMail { get; set; }
       
        [Display(Name = "Yetkili Telefon1")]
        public string c_Owner_Phone { get; set; }

        public string ProjectCompanyStatus { get; set; }

    }
}