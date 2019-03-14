using KitapciSistemi.Core.DataBase.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitapciSistemi.Data.DataContext
{
    public class KitapciContext:DbContext
    {
        public KitapciContext():base("KitapciContext")
        {
            //Configuration.LazyLoadingEnabled = true;
        }

        public DbSet<Kategori> Kategori { get; set; }
        public DbSet<Urun> Urun { get; set; }
        public DbSet<Kullanici> Kullanici { get; set; }
        public DbSet<Resim> Resim { get; set; }
        public DbSet<Rol> Rol { get; set; }
        public DbSet<Yorum> Yorum { get; set; }
    }
}
