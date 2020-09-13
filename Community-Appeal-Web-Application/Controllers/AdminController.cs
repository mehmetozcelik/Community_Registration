using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Community_Appeal_Web_Application.Models;

namespace Community_Appeal_Web_Application.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        CommunityContext db = new CommunityContext();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Admin admin)
        {
            Admin a = db.Admin.Where(x => x.eMail == admin.eMail && x.sifre == admin.sifre).FirstOrDefault();
            if (a==null)
            {
                ViewBag.Hata = "Girdiğiniz Bilgilerde bir kullanıcı Bulunamadı.";
                return View();
            }
            Session["Admin"] = a;
            return RedirectToAction("Index");
        }

        public ActionResult CikisYap()
        {
            Session.Abandon();
            return RedirectToAction("Login");

        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Listesi()
        {
            return View(db.Admin.ToList());
        }

        [HttpGet]
        public ActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Sil(int id)
        {
            Admin admin = db.Admin.Where(x => x.ID == id).FirstOrDefault();
            Admin admin2 = (Admin)Session["Admin"]; 
            if (admin!=null)
            {
                if (admin.ID== admin2.ID)
                {
                    return Json(2);
                }
                db.Admin.Remove(admin);
                db.SaveChanges();
                return Json(1);
            }
            return Json(0);
        }

        [HttpGet]
        public ActionResult Duzenle(int id)
        {
            Admin admin = db.Admin.Where(x => x.ID == id).FirstOrDefault();
            if (admin==null)
            {
                return RedirectToAction("Listesi");
            }
            return View(admin);
        }

        [HttpPost]
        public ActionResult Duzenle(Admin admin)
        {
            Admin ad = db.Admin.Where(x => x.ID == admin.ID).SingleOrDefault();
            if (ad !=null)
            {
                ad.adi = admin.adi;
                ad.soyadi = admin.soyadi;
                ad.eMail = admin.eMail;
                ad.sifre = admin.sifre;
                db.SaveChanges();
                return RedirectToAction("Listesi");
            }
            return RedirectToAction("Listesi");
        }

        [HttpPost]
        public ActionResult Ekle(Admin admin)
        {
            db.Admin.Add(admin);
            db.SaveChanges();
            return RedirectToAction("Listesi");
        }

        [HttpGet]
        public ActionResult Basvurular()
        {
            return View(db.Basvuru.Where(x=>x.adimNo>=9 && x.kapat != true).ToList());
        }

        [HttpGet]
        public ActionResult kapatilanBasvurular()
        {
            return View(db.Basvuru.Where(x => x.adimNo >= 9 && x.kapat==true).ToList());
        }

       
        public ActionResult basvuruKapat(int id)
        {
            Basvuru b = db.Basvuru.Where(x=>x.ID==id).SingleOrDefault();
            b.kapat = true;
            db.SaveChanges();
            return RedirectToAction("Basvurular");
        }

        [HttpGet]
        public ActionResult tamamlanmayanBasvurular()
        {
            return View(db.Basvuru.Where(x => x.adimNo <=8).ToList());
        }

        [HttpGet]
        public ActionResult Guncellemeler()
        {
            return View(db.Guncelle.Where(x => x.adimNo >= 7 && x.kapat==false).ToList());
        }


        [HttpGet]
        public ActionResult tamamlanmayanGuncellemer()
        {
            return View(db.Guncelle.Where(x => x.adimNo < 7).ToList());
        }

        [HttpGet]
        public ActionResult kapatilanGuncellemeler()
        {
            return View(db.Guncelle.Where(x => x.adimNo >= 7 && x.kapat == true).ToList());
        }


        public ActionResult guncelleKapat(int id)
        {
            Guncelle b = db.Guncelle.Where(x => x.ID == id).SingleOrDefault();
            b.kapat = true;
            db.SaveChanges();
            return RedirectToAction("Guncellemeler");
        }

        public ActionResult KayitliKullanicilar()
        {
            return View(db.Kullanici.ToList());
        }


        [HttpPost]
        public ActionResult KayitSil(int id)
        {
            Kullanici k = db.Kullanici.Where(x => x.ID == id).FirstOrDefault();
            if (k != null)
            {
                Basvuru b = db.Basvuru.Where(x => x.kullanıcıID == k.ID).SingleOrDefault();
                Guncelle g = db.Guncelle.Where(x => x.kullanıcıID == k.ID).SingleOrDefault();

                if (b!=null)
                {
                    db.Danisman.RemoveRange(db.Danisman.Where(x => x.basvuruID == b.ID).ToList());
                    db.FaliyetPlani.RemoveRange(db.FaliyetPlani.Where(x => x.faliyetID == b.ID).ToList());
                    db.OgrenciListesi.RemoveRange(db.OgrenciListesi.Where(x => x.basvuruID == b.ID).ToList());
                    db.DenetimKurulu.RemoveRange(db.DenetimKurulu.Where(x => x.basvuruID == b.ID).ToList());
                    db.YonetimKurulu.RemoveRange(db.YonetimKurulu.Where(x => x.basvuruID == b.ID).ToList());
                    db.Basvuru.Remove(b);
                    db.SaveChanges();
                }
                if (g!=null)
                {
                    db.GDanisman.RemoveRange(db.GDanisman.Where(x => x.GuncelleID == g.ID).ToList());
                    db.GFaliyetPlani.RemoveRange(db.GFaliyetPlani.Where(x => x.faliyetID == g.ID).ToList());
                    db.GOgrenciListesi.RemoveRange(db.GOgrenciListesi.Where(x => x.GuncelleID == g.ID).ToList());
                    db.GDenetimKurulu.RemoveRange(db.GDenetimKurulu.Where(x => x.GuncelleID == g.ID).ToList());
                    db.GYonetimKurulu.RemoveRange(db.GYonetimKurulu.Where(x => x.GuncelleID == g.ID).ToList());
                    db.Guncelle.Remove(g);
                    db.SaveChanges();
                }
                db.Kullanici.Remove(k);
                db.SaveChanges();
                return Json(true);
            }
            return Json(false);
        }
    }
}