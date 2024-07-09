using System.ComponentModel.DataAnnotations;

namespace MVC_Template_Net6_Eksta_Ozelliklerin_Eklenmesi.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "fullname boş geçilemez")]
        [StringLength(30)]
        public String Fullname { get; set; }

        [Required(ErrorMessage = "isim boş geçilemez")]
        [StringLength(20)]
        public String Username { get; set; }

        [Required(ErrorMessage = "Şifre boş geçilemez")]
        [MinLength(3, ErrorMessage = "Şifre en az 3 Karakter ollmalı")]
        [MaxLength(8, ErrorMessage = "Şifre en fazla 8 Karakter ollmalı")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Şifre boş geçilemez")]
        [MinLength(3, ErrorMessage = "Şifre en az 3 Karakter ollmalı")]
        [MaxLength(8, ErrorMessage = "Şifre en fazla 8 Karakter ollmalı")]
        [Compare(nameof(Password))]
        public string RePassword { get; set; }

    }

}
