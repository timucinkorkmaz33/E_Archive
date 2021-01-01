using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SmartAdminMvc.Entity;

namespace SmartAdminMvc.ModelView
{
    public class PortfolioModelView
    {
        [Display(Name = "İlan No ")]
        public int? Id { get; set; }
        [Display(Name = "Gayrimenkul Tipi")]
        public int? pg_Type { get; set; }
         [Display(Name = "Gayrimenkul Durumu")]
        public int? pg_Type_State { get; set; }
     
        [StringLength(50)]
        [Display(Name = "Başlık")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:###-##-####}")]
        public string pg_Header { get; set; }

        [Display(Name = "Açıklama")]
        public string pg_Description { get; set; }
        [Display(Name = "Gayrimenkul Brüt Alanı(m^2)")]
        public int? pg_Area_Brut { get; set; }
        [Display(Name = "Gayrimenkul Net Alanı(m^2)")]
        public int? pg_Area_Net { get; set; }
        [Display(Name = "Aylık Gider(Aydat)")]
        public int? pg_Subscription { get; set; }

        [Display(Name = "Giriş Tarihi")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime pg_Date { get; set; }

        [Display(Name = "Fiyatı")]
        public int? pg_Price { get; set; }
        [Display(Name = "Fiyat Tipi")]
        public int? pg_Price_Type { get; set; }
       
        [Display(Name = "Yorum")]
        public string pg_Comment { get; set; }
        [Display(Name = "Krediye Uygunluk")]
        public bool? pg_Credit { get; set; }
        [Display(Name = "Personel Kimliği")]
        public string pg_Personal_Id { get; set; }
        [Display(Name = "Müşteri Kimliği")]
        public int? pg_Customer_Id { get; set; }
        [Display(Name = "Müşteri Kimliği")]
        public int? pg_Contract_Id { get; set; }
    
        [StringLength(50)]
        [Display(Name = "Personel İsmi")]
        public string pp_Source { get; set; }
    
        [StringLength(50)]
        public int? pa_Id { get; set; }
        
        [StringLength(50)]
        [Display(Name = "Şehir")]
        public string pa_Country { get; set; }
        
        [StringLength(50)]
        [Display(Name = "İlçe")]
        public string pa_City { get; set; }
        
        [StringLength(500)]
        [Display(Name = "Mahalle")]
        public string pa_District { get; set; }
       
        [Display(Name = "Adres")]
        public string pa_Address { get; set; }
        [StringLength(500)]
        [Display(Name = "Site Adı")]
        public string pa_Site_Name { get; set; }
        [Display(Name = "KonumX")]
        public string pa_Latitude { get; set; }
        [Display(Name = "KonumY")]
        public string pa_Longitude { get; set; }
        public int? pd_Id { get; set; }
        [Display(Name = "Kullanım Durumu")]
        public bool pd_Status { get; set; }
        [Display(Name = "Isınma")]
        public int? pd_Heating { get; set; }
        [Display(Name = "Binadaki Kat Sayısı")]
        public int? pd_Building_Floor { get; set; }
        [Display(Name = "Bulunduğu Kat")]
        public int? pd_Floor { get; set; }
        [Display(Name = "Kat Karşılığı")]
        public bool? pd_Floor_Change { get; set; }
        [Display(Name = "Banyo Sayısı")]
        public int? pd_Bathroom_Number { get; set; }
        [Display(Name = "Balkon Sayısı")]
        public int? pd_Balcony_Number { get; set; }
        [Display(Name = "Binanın Yaşı")]
        public int? pd_Building_Age { get; set; }
        [Display(Name = "Oda Sayısı")]
        public int? pd_Room_Number { get; set; }
        [Display(Name = "Salon Sayısı")]
        public int? pd_Saloon_Number { get; set; }
    
        [Display(Name = "Resimler")]
        public string pd_Image { get; set; }
        public int? pe_Id { get; set; }


        [Display(Name = "Kuzey")]
        public bool pe_North { get; set; }
        [Display(Name = "Güney")]
        public bool pe_Sourth { get; set; }
        [Display(Name = "Doğu")]
        public bool pe_East { get; set; }
        [Display(Name = "Batı")]
        public bool pe_West { get; set; }

        [Display(Name = "Fiber")]
        public bool pe_Fiber { get; set; }
        [Display(Name = "Uydu")]
        public bool pe_Satellite { get; set; }
        [Display(Name = "Kablo Tv")]
        public bool pe_Cable_tv { get; set; }
        [Display(Name = "ADSL")]
        public bool pe_Adsl { get; set; }
        [Display(Name = "Fax")]
        public bool pe_Fax { get; set; }
        [Display(Name = "Telefon")]
        public bool pe_Phone { get; set; }
        [Display(Name = "Wi-Fi")]
        public bool pe_WiFi { get; set; }


        [Display(Name = "Asansör")]
        public bool pe_Elevator { get; set; }
        [Display(Name = "Havuz")]
        public bool pe_Pool { get; set; }
        [Display(Name = "Çocuk Parkı")]
        public bool pe_Child_Park { get; set; }
        [Display(Name = "Garaj")]
        public bool pe_Garage { get; set; }
        [Display(Name = "Bahçe")]
        public bool pe_Garden { get; set; }
        [Display(Name = "Yangın Merdiveni")]
        public bool pe_Fire_Stairs { get; set; }
        [Display(Name = "Güvenlik")]
        public bool pe_Securityman { get; set; }
        [Display(Name = "Jeneratör")]
        public bool pe_Generator { get; set; }

        [Display(Name = "Kamera")]
        public bool pe_Camera { get; set; }
        [Display(Name = "Yangın Alarmı")]
        public bool pe_Fire_Alarm { get; set; }
        [Display(Name = "Hırsız Alarmı")]
        public bool pe_Thief_Alarm { get; set; }



        [Display(Name = "Deniz")]
        public bool pe_Sea { get; set; }
        [Display(Name = "Boğaz")]
        public bool pe_Throat { get; set; }
        [Display(Name = "Dağ")]
        public bool pe_Mountain { get; set; }
        [Display(Name = "Şehir")]
        public bool pe_City { get; set; }
        [Display(Name = "Doğa")]
        public bool pe_Nature { get; set; }
        [Display(Name = "Göl")]
        public bool pe_Lake { get; set; }



        public int? pc_Id { get; set; }
        [Display(Name = "Başlangıç Tarihi")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime pc_StartDate { get; set; }
        [Display(Name = "Bitiş Tarihi")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime pc_FinishDate { get; set; }
        [Display(Name = "Alıcıdan Alınacak Oran")]
        public string pc_Buyer_Rate { get; set; }
        [Display(Name = "Satıcıdan Alınacak Oran")]
        public string pc_Seller_Rate { get; set; }
        [Display(Name = "Minimum Alış/Kiralama Bedeli")]

        public string pc_MinPrice { get; set; }
        [Display(Name = "Maksimum Alış/Kiralama Bedeli")]

        public string pc_MaxPrice { get; set; }
        [Display(Name = "Alıcıdan Alınacak Hizmet Bedeli")]
        public string pc_Buyer_ServicePrice { get; set; }
        [Display(Name = "Satıcıdan Alınacak Hizmet Bedeli")]
        public string pc_Seller_ServicePrice { get; set; }
        [Display(Name = "Sözleşme No")]
        public string pc_ContractNumber { get; set; }
        [Display(Name = "Yetki Belgesi")]
        public string pc_AuthorityDocument { get; set; }

        public int? pcus_Id { get; set; }
        [StringLength(50)]
        [Display(Name = "İsim")]
        public string pcus_Name { get; set; } 
        [StringLength(50)]
        [Display(Name = "Soyisim")]
        public string pcus_Surname { get; set; }

    
        [StringLength(50)]
        [Display(Name = "E-Mail")]
        public string pcus_EMail { get; set; }

        [StringLength(50)]
        [Display(Name = "Telefon1")]
       
        public string pcus_Phone1 { get; set; }
        
        [Display(Name = "Mülk Sahibi")]
        public bool pcus_Seller { get; set; }

        public int CustomerStatus { get; set; }
        public string Ev_Durumu { get; set; }

    }
}