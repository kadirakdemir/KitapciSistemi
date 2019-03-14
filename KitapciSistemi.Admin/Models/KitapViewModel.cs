using KitapciSistemi.Core.DataBase.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KitapciSistemi.Admin.Models
{
    public class KitapViewModel
    {
        [Key]
        public int ID { get; set; }
        public string Aciklama { get; set; }
        public DateTime InsertDate { get; set; }
        //    public DateTime UpdateDate { get; set; }
        public int InsertUserId { get; set; }
        public int UpdateUserId { get; set; }
        public bool IsActive { get; set; }
        [Required]
        [Display(Name = "Başlık")]
        [MinLength(2, ErrorMessage = "En az iki karakter giriniz !"), MaxLength(50, ErrorMessage = "Lütfen 50 karakteri geçmeyiniz !")]
        public string Baslik { get; set; }

        [Display(Name = "Kısa Açıklama")]
        public string UzunAciklama { get; set; }

        [Display(Name = "Eski Fiyat")]
        public decimal EskiFiyat { get; set; }

        [Required]
        [Display(Name = "Fiyat")]
        public decimal Fiyat { get; set; }

        [Display(Name = "Maliyet")]
        public decimal Maliyet { get; set; }

        [Display(Name = "Okunma Sayisi")]
        public string OkunmaSayisi { get; set; }
        public int KullaniciID { get; set; }

        [Display(Name = "Resim")]
        [MaxLength(255, ErrorMessage = "Çok fazla girdiniz !")]
        public string Resim { get; set; }
        public int KategoriID { get; set; }
       
        public virtual ICollection<Yorum> Yorumlar { get; set; }
        public virtual ICollection<Resim> Resimler { get; set; }
    }
}