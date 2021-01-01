#region Using

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;

#endregion

namespace SmartAdminMvc.Models
{
    public class AccountLoginModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class AccountForgotPasswordModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }

    public class AccountResetPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string PasswordConfirm { get; set; }
    }

    public class AccountRegistrationModel
    {
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [EmailAddress]
        [Compare("Email")]
        public string EmailConfirm { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string PasswordConfirm { get; set; }
    }

    public class AccountRegistrationModel_Custom
    {

        [DisplayName("Kullanıcı Adı")]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [DisplayName("E-Mail")]
        [StringLength(51, ErrorMessage = "E-Mail 51 karakterden fazla olamaz.")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Şifre")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [DisplayName("Şifre Tekrarı")]
        public string PasswordConfirm { get; set; }

        [Required]
        [DisplayName("Telefon")]
        [StringLength(16, ErrorMessage = "Telefon 16 karakterden fazla olamaz.")]
        public string PHONENUMBER { get; set; }

        [DisplayName("Kullanıcı ID")]
        public string ID { get; set; } //default oluşacak

        [DisplayName("Rol")]
        public string ROLENAME { get; set; } // USER veya ADMIN 
        public string Name { get; set; }
        public string Surname { get; set; }

        public string USERIMAGEURL { get; set; }

  



    }
}