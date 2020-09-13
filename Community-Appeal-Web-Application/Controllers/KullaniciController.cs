using Community_Appeal_Web_Application.App_Classes;
using Community_Appeal_Web_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Community_Appeal_Web_Application.Controllers
{
    public class KullaniciController : Controller
    {
        CommunityContext db = new CommunityContext();

        [HttpGet]
        public ActionResult GuncelleAdmin(int ID)
        {
            if (Session["Kullanici"] !=null)
            {
                TempData["Errorum"] = "İlk önce açık olan güncellemeyi veya başvuruyu kapatmanız gerekir.";
                return RedirectToAction("Index", "Admin");
            }
            Kullanici user = db.Kullanici.Where(x => x.ID == ID).FirstOrDefault();
            Session["Kullanici"] = user;
            BasvuruVeGuncelle.guncelleStatus = true;
            TempData["Guncelle"] = "Gitmek istediğiniz güncelleme formları sol menüde açılmıştır.";
            return RedirectToAction("Index", "Admin");
        }

        [HttpGet]
        public ActionResult BasvuruAdmin(int ID)
        {
            if (Session["Kullanici"] != null)
            {
                TempData["Errorum"] = "İlk önce açık olan güncellemeyi veya başvuruyu kapatmanız gerekir.";
                return RedirectToAction("Index", "Admin");
            }
            Kullanici user = db.Kullanici.Where(x => x.ID == ID).FirstOrDefault();
            Session["Kullanici"] = user;
            BasvuruVeGuncelle.basvuruStatus = true;
            TempData["Basvuru"] = "Gitmek istediğiniz başvuru formları sol menüde açılmıştır.";
            return RedirectToAction("Index", "Admin");
        }

        [HttpGet]
        public ActionResult GuncelleAdminCikis()
        {
            Session["Kullanici"] = null;
            BasvuruVeGuncelle.guncelleStatus = false;
            TempData["Guncelle"] = "Güncelleme Kapatılmıştır.";
            return RedirectToAction("Index", "Admin");
        }

        [HttpGet]
        public ActionResult BasvuruAdminCikis()
        {
            Session["Kullanici"] = null;
            BasvuruVeGuncelle.basvuruStatus = false;
            TempData["Basvuru"] = "Başvuru Kapatılmıştır.";
            return RedirectToAction("Index", "Admin");
        }


        // GET: Kullanici
        public ActionResult Login()
        {
            return View();
        }
        
        public ActionResult GirisYap(Kullanici k)
        {
            Kullanici user = db.Kullanici.Where(x => x.ogrMail == k.ogrMail && x.sifre == k.sifre).FirstOrDefault();

            if (user !=null)
            {
                Session["Kullanici"] = user;
                return RedirectToAction("Index", "Home");
            }
            ViewData["Hata"] = "Girdiğiniz bilgiler ile kayıtlı bir kullanıcı bulunamadı.";
            return RedirectToAction("Login");
        }

        public ActionResult CikisYap()
        {
            Session.Abandon();
            return RedirectToAction("Login");
        }

        public ActionResult SifremiUnuttum()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SifremiUnuttum( string mail)
        {
            Kullanici k = db.Kullanici.Where(x => x.ogrMail == mail).FirstOrDefault();
            if (k==null)
            {
                ViewBag.Hata = "Bu mail adresi ile bir kullanıcı Bulunamadı.";
                return View();
            }
            bool Durum = Functions.SifreYenile(mail);
            if (Durum==true)
            {
                ViewBag.Basarili = "Yeni şifreniz mail adresinize gönderilmiştir.";
                return View();
            }
            else
            {
                ViewBag.Basarisiz = "Yeni şifreniz gönderilemedi. Lütfen daha sonra tekrar deneyiniz.";
                return View();
            }
        }

        [HttpGet]
        public ActionResult Kayit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Kayit(Kullanici k, string tel,string fak)
        {
            Kullanici us = db.Kullanici.Where(x => x.ogrMail == k.ogrMail).FirstOrDefault();

            if (us != null)
            {
                ViewBag.Hata = "Bu mail adresi ile bir kayıt bulunmaktadır.";
                return View();
            }

            k.adi= Functions.IlkHarfleriBuyut(k.adi);
            k.soyadi = Functions.IlkHarfleriBuyut(k.soyadi);
            k.kayitTarihi = DateTime.Now;

            Basvuru b = new Basvuru();
            b.kullanıcıID = k.ID;
            b.kapat = false;
            b.adimNo = 1;

            Guncelle g = new Guncelle();
            g.kullanıcıID = k.ID;
            g.kapat = false;
            g.adimNo = 1;

            GOgrenciListesi gol = new GOgrenciListesi();
            gol.adi = k.adi;
            gol.soyadi = k.soyadi;
            gol.tc = k.tc;
            gol.ogrNo = k.ogrNo;
            gol.tel = tel;
            gol.GuncelleID = g.ID;
            gol.mail = k.ogrMail;
            gol.fak = fak;

            OgrenciListesi ol = new OgrenciListesi();
            ol.adi = k.adi;
            ol.soyadi = k.soyadi;
            ol.tc = k.tc;
            ol.ogrNo = k.ogrNo;
            ol.tel = tel;
            ol.basvuruID = b.ID;
            ol.mail = k.ogrMail;
            ol.fak = fak;


            db.Kullanici.Add(k);
            db.Guncelle.Add(g);
            db.GOgrenciListesi.Add(gol);         
            db.Basvuru.Add(b);
            db.OgrenciListesi.Add(ol);
            db.SaveChanges();
            return RedirectToAction("GirisYap", "Kullanici", k);
        }
    }
}