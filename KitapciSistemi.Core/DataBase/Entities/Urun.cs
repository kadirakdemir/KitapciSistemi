using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitapciSistemi.Core.DataBase.Entities
{
    [Table("Urun", Schema = "Kitapci")]
    public class Urun : BaseEntity
    {
        [Required(ErrorMessage ="{0} alanı gereklidir!")]
        [Display(Name ="Başlık")]
        [MinLength(2,ErrorMessage ="En az iki karakter giriniz !"),MaxLength(50,ErrorMessage ="Lütfen 50 karakteri geçmeyiniz !")]
        public string Baslik { get; set; }

        [Display(Name = "Kısa Açıklama")]
        public string UzunAciklama { get; set; }

        [Display(Name ="Eski Fiyat")]
        public decimal EskiFiyat { get; set; }


        [Required(ErrorMessage = "{0} alanı gereklidir!")]
        [Display(Name ="Fiyat")]
        public decimal Fiyat { get; set; }

        [Display(Name = "Maliyet")]
        public decimal Maliyet { get; set; }

        [Display(Name = "Okunma Sayisi")]
        public string OkunmaSayisi { get; set; }

        [Display(Name = "Resim")]
        [MaxLength(255, ErrorMessage = "Çok fazla girdiniz !")]
        public string Resim { get; set; }
        public int KategoriID { get; set; }
        public virtual Kategori Kategori { get; set; }
        public virtual ICollection<Yorum> Yorumlar { get; set; }
        public virtual ICollection<Resim> Resimler { get; set; }


    }
}
