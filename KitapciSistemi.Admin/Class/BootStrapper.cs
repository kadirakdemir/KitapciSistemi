using Autofac;
using Autofac.Integration.Mvc;
using KitapciSistemi.Service.KategoriServis;
using KitapciSistemi.Service.KullaniciServis;
using KitapciSistemi.Service.ResimServis;
using KitapciSistemi.Service.RolServis;
using KitapciSistemi.Service.UrunServis;
using KitapciSistemi.Service.YorumServis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KitapciSistemi.Admin.Class
{
    public class BootStrapper
    {
        public static void RunConfig()
        {
            BuildAutofac();
        }

        private static void BuildAutofac()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<KullaniciServis>().As<IKullaniciServis>();
            builder.RegisterType<KategoriServis>().As<IKategoriServis>();
            builder.RegisterType<ResimServis>().As<IResimServis>();
            builder.RegisterType<RolServis>().As<IRolServis>();
            builder.RegisterType<UrunServis>().As<IUrunServis>();
            builder.RegisterType<YorumServis>().As<IYorumServis>();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}