using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitapciSistemi.Core.DataBase.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public int ID { get; set; }
        public string Aciklama { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int InsertUserId { get; set; }
        public int UpdateUserId { get; set; }
        public bool IsActive { get; set; }
    }
}
