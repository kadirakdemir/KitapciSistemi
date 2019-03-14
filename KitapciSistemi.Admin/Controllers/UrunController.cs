using KitapciSistemi.Admin.Class;
using KitapciSistemi.Admin.Models;
using KitapciSistemi.Core.DataBase.Entities;
using KitapciSistemi.Service.KategoriServis;
using KitapciSistemi.Service.KullaniciServis;
using KitapciSistemi.Service.ResimServis;
using KitapciSistemi.Service.UrunServis;
using KitapciSistemi.Service.YorumServis;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace KitapciSistemi.Admin.Controllers
{
    public class UrunController : Controller
    {
        #region Veri Bağlantısı

        private readonly IUrunServis _urunServis;
        private readonly IKategoriServis _kategoriServis;
        private readonly IResimServis _resimServis;
        private readonly IYorumServis _yorumServis;
        private readonly IKullaniciServis _kullaniciServis;
        public UrunController(IUrunServis urunServis, IKategoriServis kategoriServis, IResimServis resimServis, IYorumServis yorumServis, IKullaniciServis kullaniciServis)
        {
            _urunServis = urunServis;
            _kategoriServis = kategoriServis;
            _resimServis = resimServis;
            _yorumServis = yorumServis;
            _kullaniciServis = kullaniciServis;
        }
        #endregion
        static int altid = 0;
        // GET: Urun
        [HttpGet]
        [LogiinFilter]
        public ActionResult Index(int sayfa = 1)
        {
            ViewBag.Kullanicilar = _kullaniciServis.GetAll();
            return View(_urunServis.GetAll().OrderBy(x => x.Baslik).ToPagedList(sayfa, 10));
        }

        [HttpGet]
        [LogiinFilter]
        public ActionResult Insert()
        {
            GetListe();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Insert(Urun urun, int KategoriID, HttpPostedFileBase VitrinResmi, IEnumerable<HttpPostedFileBase> DigerResimler)
        {

            Kategori kategori = _kategoriServis.Get(x => x.ID == urun.KategoriID);
            Kullanici kullanici = _kullaniciServis.GetById(Convert.ToInt32(HttpContext.Session["KullaniciEmail"]));

            if (ModelState.IsValid)
            {
                urun.InsertDate = DateTime.Now;
                urun.InsertUserId = kullanici.ID;
                urun.UpdateDate = DateTime.Now;
                urun.KategoriID = kategori.ID;
                if (VitrinResmi != null)
                {
                    string DosyaAdi = Guid.NewGuid().ToString().Replace("-", "");
                    string Uzanti = System.IO.Path.GetExtension(Request.Files[0].FileName);
                    string Tamyol = "/Upload/Urun/" + DosyaAdi + Uzanti;
                    Request.Files[0].SaveAs(Server.MapPath(Tamyol));
                    urun.Resim = Tamyol;
                }
                _urunServis.Insert(urun);
                _urunServis.Save();
                string digerResimler = System.IO.Path.GetExtension(Request.Files[1].FileName);
                if (digerResimler != "")
                {
                    foreach (var item in DigerResimler)
                    {
                        if (item.ContentLength > 0)
                        {
                            string dosyaAdi = Guid.NewGuid().ToString().Replace("-", "");
                            string uzanti = System.IO.Path.GetExtension(Request.Files[1].FileName);
                            string tamYol = "/Upload/Urun/" + dosyaAdi + uzanti;
                            item.SaveAs(Server.MapPath(tamYol));
                            var resim = new Resim
                            {
                                ResimUrl = tamYol
                            };
                            resim.InsertDate = DateTime.Now;
                            resim.UpdateDate = DateTime.Now;
                            resim.InsertUserId = kullanici.ID;
                            resim.UrunID = urun.ID;
                            _resimServis.Insert(resim);
                            _resimServis.Save();
                        }
                        else
                        {
                            break;
                        }

                    }
                }

                ViewBag.Mesaj = "Kitap ekleme işleminiz başarıyla gerçekleşti.";
                return RedirectToAction("Index", "Urun");
            }
            var error = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();

            return View();
        }

        [HttpGet]
        [LogiinFilter]
        public ActionResult Update(int id)
        {
            Urun urun = _urunServis.GetById(id);
            Kategori kategori = _kategoriServis.GetById(urun.KategoriID);
            // altid = urun.KategoriID;
            GetListe();
            ViewBag.AltKategoriList = _kategoriServis.GetMany(x => x.ID == urun.KategoriID).ToList();
            return View(urun);
        }

        [HttpPost]
        [LogiinFilter]
        public ActionResult Update(Urun urun, int KategoriID, HttpPostedFileBase VitrinResmi, IEnumerable<HttpPostedFileBase> DigerResimler)
        {

            int KullaniciID = Convert.ToInt32(Session["KullaniciEmail"]);
            Urun _urun = _urunServis.GetById(urun.ID);
            if (ModelState.IsValid)
            {
                _urun.Baslik = urun.Baslik;
                _urun.Aciklama = urun.Aciklama;
                _urun.UzunAciklama = urun.UzunAciklama;
                _urun.Fiyat = urun.Fiyat;
                _urun.EskiFiyat = urun.EskiFiyat;
                _urun.Maliyet = urun.Maliyet;
                _urun.IsActive = urun.IsActive;
                _urun.OkunmaSayisi = "1";
                _urun.UpdateDate = DateTime.Now;
                _urun.UpdateUserId = KullaniciID;

                if (VitrinResmi != null)
                {
                    string DosyaAdi = _urun.Resim;
                    string DosyaYolu = Server.MapPath(DosyaAdi);
                    FileInfo Dosya = new FileInfo(DosyaYolu);
                    if (Dosya.Exists)
                    {
                        Dosya.Delete();
                    }

                    string dosyaAdi = Guid.NewGuid().ToString().Replace("-", "");
                    string Uzantisi = System.IO.Path.GetExtension(Request.Files[0].FileName);
                    string Tamyol = "/Upload/Urun/" + dosyaAdi + Uzantisi;
                    Request.Files[0].SaveAs(Server.MapPath(Tamyol));
                    _urun.Resim = Tamyol;
                }

                string digerResimler = System.IO.Path.GetExtension(Request.Files[1].FileName);
                if (digerResimler != "")
                {

                    foreach (var item in DigerResimler)
                    {
                        if (item.ContentLength > 0)
                        {
                            string dosyaAdi = Guid.NewGuid().ToString().Replace("-", "");
                            string uzantisi = System.IO.Path.GetExtension(Request.Files[1].FileName);
                            string tamYol = "/Upload/Urun/" + dosyaAdi + uzantisi;
                            item.SaveAs(Server.MapPath(tamYol));
                            var resim = new Resim
                            {
                                ResimUrl = tamYol
                            };
                            _urun.UpdateDate = DateTime.Now;
                            resim.InsertDate = DateTime.Now;
                            resim.UpdateDate = DateTime.Now;
                            resim.InsertUserId = KullaniciID;
                            resim.UrunID = urun.ID;
                            _resimServis.Insert(resim);
                            _resimServis.Save();
                        }
                    }
                }
                _urunServis.Save();
                ViewBag.Mesaj = "Güncelleme işlemi başarıyla gerçekleşti.";
                return RedirectToAction("Index", "Urun");
            }
            ViewBag.HataMesaj = "Güncelleme işlemi başarısız.";
            return View(urun);
        }

        public void GetListe()
        {
            ViewBag.KategoriList = _kategoriServis.GetMany(x => x.AltKategoriID == 0).ToList();
            ViewBag.AltKategoriList = _kategoriServis.GetMany(x => x.AltKategoriID == altid).ToList();
        }

        [HttpPost]
        public JsonResult GetAltKategoriList(int AltID)
        {
            altid = AltID;
            var altkategoriler = _kategoriServis.GetMany(x => x.AltKategoriID == AltID).ToList();
            ViewBag.AltKategoriList = altkategoriler;
            return Json(altkategoriler);
        }


        [HttpGet]
        [LogiinFilter]
        public ActionResult Delete(int id)
        {
            Urun urun = _urunServis.GetById(id);
            var resimler = _resimServis.GetMany(x=>x.UrunID==id);
            if (urun == null)
            {
                throw new Exception("Kitap bulunamadı!");
            }
            string resimadi = urun.Resim;
            string resimyolu = Server.MapPath(resimadi);
            FileInfo dosya = new FileInfo(resimyolu);

            if (dosya.Exists)
            {
                dosya.Delete();
            }
            if (resimler!=null)
            {
                foreach (var item in resimler)
                {
                    string yol = Server.MapPath(item.ResimUrl);
                    FileInfo resim = new FileInfo(yol);
                    if (resim.Exists)
                    {
                        resim.Delete();
                    }
                }
            }
            _urunServis.Delete(id);
            _urunServis.Save();
            ViewBag.Mesaj = "Başarıyla silindi!";
            return RedirectToAction("Index","Urun");
        }

        [HttpGet]
        public ActionResult Durum(int id)
        {
            Urun urun = _urunServis.GetById(id);
            if (urun.IsActive == true)
            {
                urun.IsActive = false;
                _urunServis.Save();
                return RedirectToAction("Index", "Urun");
            }
            urun.IsActive = true;
            _urunServis.Save();
            return RedirectToAction("Index", "Urun");
        }

        [HttpGet]
        public ActionResult ResimSil(int ID)
        {
            Resim resim = _resimServis.GetById(ID);
            
            if (resim == null)
            {
                Urun urun = _urunServis.Get(x => x.ID == ID);
                if (urun==null)
                {
                    throw new Exception("Resim bulunamadı !");
                }
                string dosyaAdi = urun.Resim;
                string Yolu = Server.MapPath(dosyaAdi);
                FileInfo file = new FileInfo(Yolu);
                if (file.Exists)
                {
                    file.Delete();
                }
                urun.Resim = null;
                _urunServis.Save();
                return RedirectToAction("Update", "Urun", new { id = ID });
            }
            string DosyaAdi = resim.ResimUrl;
            string yolu = Server.MapPath(DosyaAdi);
            FileInfo dosya = new FileInfo(yolu);
            if (dosya.Exists)
            {
                dosya.Delete();
            }
            _resimServis.Delete(ID);
            _resimServis.Save();
            return RedirectToAction("Update", "Urun", new { id = resim.UrunID });
        }
    }
}