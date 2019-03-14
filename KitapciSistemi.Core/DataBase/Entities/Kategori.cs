using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitapciSistemi.Core.DataBase.Entities
{
    [Table("Kategori",Schema ="Kitapci")]
    public class Kategori:BaseEntity
    {
          
        [Display(Name = "Kategori Adı")]
        [MinLength(2,ErrorMessage ="En az 2 karakter giriniz !"),MaxLength(50,ErrorMessage ="Lütfen 50 karakteri geçmeyiniz !")]
        public string KategoriAdi { get; set; }
              
        public int AltKategoriID { get; set; }

        //[MinLength(2, ErrorMessage = "{0} karakterden az olamaz."), MaxLength(150, ErrorMessage = "150 karakterden fazla girmeyiniz")]
        //public string URL { get; set; }

       // public int ResimID { get; set; }
        
        public virtual ICollection<Urun> Urunler { get; set; }
    }
}
