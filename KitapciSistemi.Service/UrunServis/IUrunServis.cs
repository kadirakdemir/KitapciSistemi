﻿using KitapciSistemi.Core.DataBase.Entities;
using KitapciSistemi.Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitapciSistemi.Service.UrunServis
{
    public interface IUrunServis:IRepository<Urun>
    {
    }
}
