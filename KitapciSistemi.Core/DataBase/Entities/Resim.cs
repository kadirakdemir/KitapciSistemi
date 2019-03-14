using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitapciSistemi.Core.DataBase.Entities
{
    [Table("Resim",Schema ="Kitapci")]
    public class Resim:BaseEntity
    {     
        public string ResimUrl { get; set; }
        public int UrunID { get; set; }
        public Urun Urun { get; set; }
    }
}
