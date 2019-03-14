using KitapciSistemi.Admin.Class;
using KitapciSistemi.Admin.CustomFilter;
using KitapciSistemi.Core.DataBase.Entities;
using KitapciSistemi.Service.KategoriServis;
using KitapciSistemi.Service.KullaniciServis;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KitapciSistemi.Admin.Controllers
{
    public class KategoriController : Controller
    {
        #region Veri Bağlantısı
        private readonly IKategoriServis _kategoriServis;
        private readonly IKullaniciServis _kullaniciServis;
        public KategoriController(IKategoriServis kategoriServis, IKullaniciServis kullaniciServis)
        {
            _kategoriServis = kategoriServis;
            _kullaniciServis = kullaniciServis;
        }
        #endregion
        // GET: Kategori
        [HttpGet]
        [LoginFilter]
        public ActionResult Index(int sayfa = 1)
        {
            return View(_kategoriServis.GetAll().OrderBy(x => x.KategoriAdi).ToPagedList(sayfa, 10));
        }


        #region Kayıt Ekleme

        [HttpGet]
        [LoginFilter]
        public ActionResult Insert()
        {
            GetCategoryList();
            return View();
        }



        [HttpPost]
        [LoginFilter]
        //[ValidateAntiForgeryToken]
        public JsonResult Insert(Kategori kategori)
        {
            try
            {
                Kullanici kullanici = _kullaniciServis.GetById(Convert.ToInt32(HttpContext.Session["KullaniciEmail"]));
                kategori.InsertDate = DateTime.Now;
                kategori.UpdateDate = DateTime.Now;
                kategori.InsertUserId = kullanici.ID;
                kategori.UpdateUserId = kullanici.ID;
                _kategoriServis.Insert(kategori);
                _kategoriServis.Save();
                return Json(new ResultJson { Success = true, Message = "Kategori ekleme işleminiz başarıyla gerçekleşti." });
            }
            catch (Exception)
            {

                return Json(new ResultJson { Success = false, Message = "Kategori ekleme sırasında hata oluştu Hata-500" });
            }
        }

        #endregion

        #region Kategori Düzenle

        [HttpGet]
        [LoginFilter]
        public ActionResult Update(int id)
        {
            Kategori kategori = _kategoriServis.GetById(id);
            if (kategori == null)
            {
                throw new Exception("Kategori Bulunamadı");
            }
            GetCategoryList();
            return View(kategori);
        }

        [HttpPost]
        [LoginFilter]
        public JsonResult Update(Kategori kategori)
        {
            Kategori _kategori = _kategoriServis.GetById(kategori.ID);
            if (ModelState.IsValid)
            {
                _kategori.AltKategoriID = kategori.AltKategoriID;
                _kategori.IsActive = kategori.IsActive;
                _kategori.KategoriAdi = kategori.KategoriAdi;
                _kategori.Aciklama = kategori.Aciklama;
                _kategori.UpdateDate = DateTime.Now;
                _kategori.UpdateUserId = Convert.ToInt32(HttpContext.Session["KullaniciEmail"]);                
                _kategoriServis.Save();
                return Json(new ResultJson { Success = true, Message = "Düzenleme İşlemi Başarılı" });
            }

            //var error = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();

            return Json(new ResultJson { Success = true, Message = "Düzenleme İşlemi Başarısız" });
        }
        #endregion

        #region Kategori Sil

        [HttpPost]
        [LoginFilter]
        public JsonResult Delete(int ID)
        {
            Kategori kategori = _kategoriServis.GetById(ID);
            if (kategori == null)
            {
                return Json(new ResultJson { Success = true, Message = "Kategori Bulunamadı" });
            }
            _kategoriServis.Delete(ID);
            _kategoriServis.Save();

            return Json(new ResultJson { Success = true, Message = "Kategori Silme İşleminiz Başarılı" });
        }
        #endregion

        #region GetCategoryList
        private void GetCategoryList()
        {
            var KategoriList = _kategoriServis.GetMany(x => x.AltKategoriID == 0).ToList();
            ViewBag.KategoriListe = KategoriList;
        }
        #endregion
    }
}