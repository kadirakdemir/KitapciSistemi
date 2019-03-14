using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitapciSistemi.Core.DataBase.Entities
{
    [Table("Yorum", Schema = "Kitapci")]
    public class Yorum : BaseEntity
    {
        [Required]
        [Display(Name ="İçerik")]
        [MinLength(3,ErrorMessage ="En az 3 karakter giriniz !"),MaxLength(250,ErrorMessage ="250 karekateri aştınız !")]
        public string Icerik { get; set; }
        public int KullaniciID { get; set; }
        public int IrunID { get; set; }
        public virtual Kullanici Kullanici{ get; set; }
        public virtual Urun Urun { get; set; }

    }
}
