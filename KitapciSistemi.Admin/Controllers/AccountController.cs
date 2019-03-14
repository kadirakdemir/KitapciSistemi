using KitapciSistemi.Admin.Class;
using KitapciSistemi.Admin.Models;
using KitapciSistemi.Core.DataBase.Entities;
using KitapciSistemi.Service.KullaniciServis;
using KitapciSistemi.Service.RolServis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KitapciSistemi.Admin.Controllers
{
    public class AccountController : Controller
    {
        #region Data bağlantı
        private readonly IKullaniciServis _kullaniciServis;
        private readonly IRolServis _rolServis;
        public AccountController(IKullaniciServis kullaniciServis, IRolServis rolServis)
        {
            _kullaniciServis = kullaniciServis;
            _rolServis = rolServis;
        }
        #endregion
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel kullanici)
        {
            var KullaniciControl = _kullaniciServis.GetMany(x => x.Email == kullanici.Email && x.Sifre == kullanici.Password && x.IsActive == true).SingleOrDefault();
            if (KullaniciControl != null)
            {
                foreach (var item in KullaniciControl.Roller)
                {
                    if (item.RolAdi == "Admin")
                    {
                        Session["KullaniciEmail"] = KullaniciControl.ID;
                        Session["KullaniciAdSoyad"] = KullaniciControl.KullaniciAdi + " " + KullaniciControl.Soyadi;
                        return RedirectToAction("Index", "Home");
                    }

                }
            }

            ViewBag.Mesaj = "Kullanici Bulunamadı !";
            return View(kullanici);
        }

        [HttpPost]
        public JsonResult Create(Kullanici User)
        {
            Rol rol = _rolServis.Get(x => x.RolAdi == "Admin");
            if (ModelState.IsValid)
            {
                User.InsertDate = DateTime.Now;
                User.IsActive = true;
                User.UpdateDate = DateTime.Now;
                User.EnSonGirisTarihi = DateTime.Now;
                User.IsConfirmed = true;
                _kullaniciServis.Insert(User);
                rol.Kullanicilar.Add(User);
                _rolServis.Save();
                return Json(new ResultJson { Success = true, Message = "Kayıt işleminiz başarıyla gerçekleşti." });
            }
            return Json(new ResultJson { Success = true, Message = "Kayıt işleminiz başarısız." });
        }

        [HttpGet]
        public ActionResult LogOut()
        {
            Session["KullaniciEmail"] = null;
            return RedirectToAction("Login","Account");
        }
    }
}