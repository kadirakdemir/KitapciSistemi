using KitapciSistemi.Core.DataBase.Entities;
using KitapciSistemi.Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitapciSistemi.Service.KullaniciServis
{
    public interface IKullaniciServis:IRepository<Kullanici>
    {
    }
}
