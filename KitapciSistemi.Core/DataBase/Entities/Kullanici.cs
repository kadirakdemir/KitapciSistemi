using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitapciSistemi.Core.DataBase.Entities
{
    [Table("Kullanici",Schema="Kitapci")]
    public class Kullanici:BaseEntity
    {
       
        [Display(Name ="Kullanıcı Ad")]
        [MinLength(2,ErrorMessage ="En az 2 karakter giriniz."),MaxLength(50,ErrorMessage = "Lütfen 50 karakterden fazla değer girmeyiniz !")]
        public string Adi { get; set; }

      
        [Display(Name = "Kullanıcı Soyad")]
        [MinLength(2, ErrorMessage = "En az 2 karakter giriniz."), MaxLength(50, ErrorMessage = "Lütfen 50 karakterden fazla değer girmeyiniz !")]
        public string Soyadi { get; set;}
       
        [Required(ErrorMessage = "{0} alanı gereklidir!")]
        [Display(Name = "Takma Ad")]
        [MinLength(2, ErrorMessage = "En az 2 karakter giriniz."), MaxLength(50, ErrorMessage = "Lütfen 50 karakterden fazla değer girmeyiniz !")]
        public string KullaniciAdi { get; set; }

        [Required(ErrorMessage = "{0} alanı gereklidir!")]
        [Display(Name = "E-mail")]        
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Geçerli Bir Mail Adresi Giriniz")]
        public string Email { get; set; }

        [Display(Name = "Şifre")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "{0} alanı gereklidir!")]
        [MaxLength(16, ErrorMessage = "Lütfen 16 karakterden fazla değer girmeyiniz !")]
        public string Sifre { get; set; }
        public DateTime EnSonGirisTarihi { get; set; }
        public string EnSonGirisIP { get; set; }
        public bool IsConfirmed { get; set; }
        public string ProfilResimUrl { get; set; }
        public virtual ICollection<Rol> Roller { get; set; }
        public virtual ICollection<Yorum> Yorumlar { get; set; }
    }
}
