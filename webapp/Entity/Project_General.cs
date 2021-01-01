using System.Collections;

namespace SmartAdminMvc.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Project_General : IEnumerable
    {
        public Project_General()
        {
            ImageList = new List<string>();
            DocList = new List<string>();
           FolderList = new List<string>();
        }
        public int Id { get; set; }

         [Required]
        [StringLength(50)]
        [Display(Name = "Dosya Kodu")]
        public string Name { get; set; }
         [Display(Name = "Tarihi")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime? StartDate { get; set; }
         [Display(Name = "Açýklama")]
        public string Description { get; set; }
         [Display(Name = "Kullanýcý Adý")]
         [Required]
         public string Owner { get; set; }

         [Display(Name = "Resim Yolu")]
        public string ImagePath { get; set; }
        public List<string> ImageList { get; set; }
        public List<string> FolderList { get; set; }
        public List<string> DocList { get; set; }
        [Display(Name = "Dokumanlar")]
        public string Folder { get; set; }
        [Display(Name = "Belgeler")]
        public string Document { get; set; }

        [Display(Name = "Firma Adý")]
        public string CompanyName { get; set; }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
