using Community_Appeal_Web_Application.App_Classes;
using Community_Appeal_Web_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Community_Appeal_Web_Application.Controllers
{
    public class GuncelleController : Controller
    {
        CommunityContext db = new CommunityContext();
        static int hata { get; set; }

        // form1
        [HttpGet]
        public ActionResult form1()
        {
            Kullanici k = (Kullanici)Session["Kullanici"];
            Guncelle g = new Guncelle();

              g = db.Guncelle.Where(x => x.kullanıcıID == k.ID).FirstOrDefault();


            return View(g);
        }

        [HttpPost]
        public ActionResult form1(GuncelleForm1 guncelle)
        {

            Kullanici k = (Kullanici)Session["Kullanici"];
            Guncelle g = db.Guncelle.Where(x => x.kullanıcıID == k.ID).FirstOrDefault();

            g.toplulukAdi = guncelle.toplulukAdi;
            g.baskanAdi = guncelle.baskanAdi;
            g.baskanSoyadi = guncelle.baskanSoyadi;


            GDanisman d1 = new GDanisman();
            GDanisman d2 = new GDanisman();

            if (g.adimNo == 1)
            {

                if (guncelle.Kontrol == true)
                {
                    d2.aktif = true;
                    d1.aktif = false;
                }
                else
                {
                    d1.aktif = true;
                    d2.aktif = false;
                }

                d1.adi = guncelle.adi1;
                d1.soyadi = guncelle.soyadi1;
                d1.unvan = guncelle.unvan1;
                d1.akademikBirim = guncelle.akademikBirim1;
                d1.GuncelleID = g.ID;


                d2.adi = guncelle.adi2;
                d2.soyadi = guncelle.soyadi2;
                d2.unvan = guncelle.unvan2;
                d2.akademikBirim = guncelle.akademikBirim2;
                d2.GuncelleID = g.ID;
                g.adimNo = 2;
            }
            else
            {
                d1 = db.GDanisman.Where(x => x.GuncelleID == g.ID && x.aktif == true).FirstOrDefault();
                d2 = db.GDanisman.Where(x => x.GuncelleID == g.ID && x.aktif == false).FirstOrDefault();

                if (guncelle.Kontrol == true)
                {
                    d2.aktif = true;
                    d1.aktif = false;
                }
                else
                {
                    d1.aktif = true;
                    d2.aktif = false;
                }

                d1.adi = guncelle.adi1;
                d1.soyadi = guncelle.soyadi1;
                d1.unvan = guncelle.unvan1;
                d1.akademikBirim = guncelle.akademikBirim1;
                d1.GuncelleID = g.ID;

                d2.adi = guncelle.adi2;
                d2.soyadi = guncelle.soyadi2;
                d2.unvan = guncelle.unvan2;
                d2.akademikBirim = guncelle.akademikBirim2;
                d2.GuncelleID = g.ID;
            }

            db.GDanisman.Add(d1);
            db.GDanisman.Add(d2);
            db.SaveChanges();

            return View(g);
        }


        // form2 
        public PartialViewResult ogrenciListesiWidget()
        {
            Kullanici k = (Kullanici)Session["Kullanici"];
            Guncelle g = db.Guncelle.Where(x => x.kullanıcıID == k.ID).FirstOrDefault();
            List<GOgrenciListesi> ol = db.GOgrenciListesi.Where(x => x.GuncelleID == g.ID).ToList();
            ViewBag.ol = ol;
            return PartialView();
        }

        [HttpGet]
        public ActionResult form2()
        {
            Kullanici k = (Kullanici)Session["Kullanici"];
            Guncelle g = db.Guncelle.Where(x => x.kullanıcıID == k.ID).FirstOrDefault();
            if (g.adimNo < 2)
            {
                return View();
            }
            List<GOgrenciListesi> ol = db.GOgrenciListesi.Where(x => x.GuncelleID == g.ID).ToList();
            ViewBag.ol = ol;
            return View(g);
        }

        [HttpPost]
        public ActionResult form2(Guncelle guncelle)
        {
            Kullanici k = (Kullanici)Session["Kullanici"];
            Guncelle b = db.Guncelle.Where(x => x.kullanıcıID == k.ID).FirstOrDefault();

            if (b.GOgrenciListesi.Count < 20)
            {
                List<GOgrenciListesi> ol = db.GOgrenciListesi.Where(x => x.GuncelleID == b.ID).ToList();
                ViewBag.ol = ol;
                ViewBag.hata = "Bu formu kaydetmek için en az 20 kişi eklemelisiniz.";
                return View(b);
            }
            else
            {
                b.akademikYıl = DateTime.Now.Year.ToString() + " - " + DateTime.Now.AddYears(+1).Year.ToString();
                if (b.adimNo == 2)
                {
                    b.adimNo = 3;
                    db.SaveChanges();
                }
                List<GOgrenciListesi> ol = db.GOgrenciListesi.Where(x => x.GuncelleID == b.ID).ToList();
                ViewBag.ol = ol;
                return View(b);
            }
        }

        [HttpPost]
        public ActionResult OgrenciListesiEkle(GOgrenciListesi ol)
        {

            Kullanici k = (Kullanici)Session["Kullanici"];
            Guncelle g = db.Guncelle.Where(x => x.kullanıcıID == k.ID).FirstOrDefault();
         
                if (Functions.GuncellemeOgrenciSorgula(ol) != null)
                {
                    GOgrenciListesi ogrenciL = db.GOgrenciListesi.Where(x => x.ogrNo == ol.ogrNo && x.GuncelleID == g.ID).FirstOrDefault();
                    if (ogrenciL != null)
                    {
                        return Json("hata0");
                    }
                    ol.GuncelleID = g.ID;
                    db.GOgrenciListesi.Add(ol);
                    db.SaveChanges();
                    GOgrenciListesi o = db.GOgrenciListesi.Where(x => x.ogrNo == ol.ogrNo).FirstOrDefault();
                    int ogrno = o.ID;
                    return Json(ogrno);
                }
                else
                {
                    return Json("hata1");
                }
        }

        [HttpPost]
        public ActionResult OgrenciListesiSil(int id)
        {
            Kullanici k = (Kullanici)Session["Kullanici"];
            Guncelle g = db.Guncelle.Where(x => x.kullanıcıID == k.ID).FirstOrDefault();

            GOgrenciListesi ol = db.GOgrenciListesi.Where(x => x.ID == id).FirstOrDefault();
            if (ol == null)
            {
                return Json(1);
            }
            else if (k.ogrNo == ol.ogrNo)
            {
                return Json(2);
            }
            else
            {
                db.GOgrenciListesi.Remove(ol);
                db.SaveChanges();
                return Json(3);
            }
        }

        // form3 
        [HttpGet]
        public ActionResult form3()
        {
            Kullanici k = (Kullanici)Session["Kullanici"];
            Guncelle g = db.Guncelle.Where(x => x.kullanıcıID == k.ID).FirstOrDefault();

            ViewBag.Ogreciler = db.GOgrenciListesi.Where(x => x.GuncelleID == g.ID).ToList();
            if (g.adimNo < 3)
            {
                return View();
            }

            return View(g);
        }

        public PartialViewResult faaliyetPlaniWidget()
        {
            Kullanici k = (Kullanici)Session["Kullanici"];
            Guncelle g = db.Guncelle.Where(x => x.kullanıcıID == k.ID).FirstOrDefault();
            List<GFaliyetPlani> fp = db.GFaliyetPlani.Where(x => x.faliyetID == g.ID).ToList();
            ViewBag.fp = fp;
            return PartialView();
        }

        [HttpPost]
        public ActionResult faaliyetPlaniEkle(GFaliyetPlani fp)
        {
            Kullanici k = (Kullanici)Session["Kullanici"];
            Guncelle g = db.Guncelle.Where(x => x.kullanıcıID == k.ID).FirstOrDefault();

            if (g.GFaliyetPlani.Count != 5)
            {
                fp.faliyetID = g.ID;
                db.GFaliyetPlani.Add(fp);
                db.SaveChanges();
                return Json(true);
            }
            else
            {
                return Json(false); // 5 faaliyetten fazla eklenemez.
            }

        }

        public ActionResult faaliyetSil(int id)
        {
            Kullanici k = (Kullanici)Session["Kullanici"];
            Guncelle g = db.Guncelle.Where(x => x.kullanıcıID == k.ID).FirstOrDefault();


            GFaliyetPlani ol = db.GFaliyetPlani.Where(x => x.ID == id).FirstOrDefault();
            if (ol == null)
            {
                return Json(false);
            }
            else
            {
                db.GFaliyetPlani.Remove(ol);
                db.SaveChanges();
                return Json(true);
            }
        }

        [HttpPost]
        public ActionResult form3(Guncelle gun)
        {
            Kullanici k = (Kullanici)Session["Kullanici"];
            Guncelle g = db.Guncelle.Where(x => x.kullanıcıID == k.ID).FirstOrDefault();
            ViewBag.Ogreciler = db.GOgrenciListesi.Where(x => x.GuncelleID == g.ID).ToList();
            if (g.adimNo < 3)
            {
                return View();
            }

            if (g.GFaliyetPlani.Count < 5)
            {
                ViewBag.Hata = "En az 5 faaliyet eklemeniz gerekmektedir.";
                return View(g);
            }
            g.toplantiNo = gun.toplantiNo;
            g.toplantiTarihi = gun.toplantiTarihi;
            g.saat = gun.saat;
            g.mekan = gun.mekan;

            if (g.adimNo == 3)
            {
                g.adimNo = 4;
            }
           

            ViewBag.Ogreciler = db.GOgrenciListesi.Where(x => x.GuncelleID == g.ID).ToList();
            db.SaveChanges();
            return View(g);
        }

        //form4
        [HttpGet]
        public ActionResult form4()
        {
            Kullanici k = (Kullanici)Session["Kullanici"];
            Guncelle g = db.Guncelle.Where(x => x.kullanıcıID == k.ID).FirstOrDefault();
            if (g.adimNo < 4)
            {
                return View();
            }
            List<GDanisman> DL = db.GDanisman.Where(x => x.GuncelleID == g.ID).ToList();
            ViewBag.DL = DL;
            return View(g);
        }

        [HttpPost]
        public ActionResult form4(Guncelle Gun)
        {
            Kullanici k = (Kullanici)Session["Kullanici"];
            Guncelle g = db.Guncelle.Where(x => x.kullanıcıID == k.ID).FirstOrDefault();
            if (g.adimNo==4)
            {
                g.adimNo = 5;
            }
            List<GDanisman> DL = db.GDanisman.Where(x => x.GuncelleID == g.ID).ToList();
            ViewBag.DL = DL;
            db.SaveChanges();
            return View(g);
        }

        //form5.1
        public ActionResult form7Kayit()
        {
            Kullanici k = (Kullanici)Session["Kullanici"];
            Guncelle b = db.Guncelle.Where(x => x.kullanıcıID == k.ID).FirstOrDefault();

            if (b.adimNo == 5 && db.GYonetimKurulu.Where(x => x.GuncelleID == b.ID).ToList().Count() == 7 && db.GDenetimKurulu.Where(x => x.GuncelleID == b.ID).ToList().Count() == 3
                && b.divanBaskanAdi != null && b.marscı != null && b.butce != null && b.etkinlik != null)
            {
                b.adimNo = 6;
                db.SaveChanges();
            }
            else
            {
                TempData["YönetimHata"] = "Aşağıdaki Formda Bilgilerin Doğru veya Eksiksiz Olduğunu Kontrol Ediniz.";
            }

            return RedirectToAction("form7");
        }

        [HttpGet]
        public ActionResult form7()
        {
            Kullanici k = (Kullanici)Session["Kullanici"];
            Guncelle b = db.Guncelle.Where(x => x.kullanıcıID == k.ID).FirstOrDefault();
            if (b.adimNo < 5)
            {
                ViewBag.Hata = "İlk Önce Diğer Formları Doldurmanız Gerekmektedir.";
                return View();
            }

            var BaskanYar = db.GYonetimKurulu.FirstOrDefault(x => x.unvan == "Başkan Yardımcısı");
            ViewBag.BaskanYar = BaskanYar;

            var yk1 = db.GYonetimKurulu.Where(x => x.GuncelleID == b.ID).ToList();
            ViewBag.yk1 = yk1;

            var dk = db.GDenetimKurulu.Where(x => x.GuncelleID == b.ID).ToList();
            ViewBag.dk = dk;

            switch (hata)
            {
                case 1:
                    TempData["YönetimHata"] = "Başkan Yardımcısı Olarak Seçtiğiniz Üyeyi Daha Önce Yönetim Kurulunda Kullandınız.";
                    break;

                case 2:
                    TempData["YönetimHata"] = "Sayman Olarak Seçtiğiniz Üyeyi Daha Önce Yönetim Kurulunda Kullandınız.";
                    break;

                case 3:
                    TempData["YönetimHata"] = "Sekreter Olarak Seçtiğiniz Üyeyi Daha Önce Yönetim Kurulunda Kullandınız.";
                    break;

                case 4:
                    TempData["YönetimHata"] = "Kaydetmek İstediğiniz Üye Yönetim Kurulu Tablosunda Bulunmaktadır.";
                    break;

                case 5:
                    TempData["YönetimHata"] = "Yönetim Kurulunda En Fazla 3 Üye Bulunabilir.";
                    break;

                case 6:
                    TempData["YönetimHata"] = "Kaydetmek İstediğiniz Üye Yönetim Kurulu veya Denetim Kurulu Tablosunda Bulunmaktadır.";
                    break;

                case 7:
                    TempData["YönetimHata"] = "Denetim Kurulunda En Fazla 2 Üye Bulunabilir.";
                    break;

                case 8:
                    TempData["YönetimHata"] =
                        "Denetim Kurulu Başkanı Olarak Seçtiğiniz Üyeyi Daha Önce Yönetim Kurulunda Kullandınız.";
                    break;

                default:
                    break;
            }

            hata = 0;

            return View(b);
        }

        [HttpPost]
        public ActionResult form7YonetimKuruluKaydet(Guncelle basvuru, int? divanBasId, int? baskanYarId, int? yazmanId, int? sekreterId, int? saymanId)
        {
            Kullanici k = (Kullanici)Session["Kullanici"];
            Guncelle b = db.Guncelle.Where(x => x.kullanıcıID == k.ID).FirstOrDefault();

            if (basvuru.marscı != "null")
            {
                b.marscı = basvuru.marscı;
            }

            if (basvuru.uyeSayisi != null)
            {
                b.uyeSayisi = basvuru.uyeSayisi;
            }

            var divanBaskani = db.GOgrenciListesi.FirstOrDefault(x => x.ID == divanBasId);
            if (divanBaskani != null)
            {
                b.divanBaskanAdi = divanBaskani.adi;
                b.divanBaskanSoyadi = divanBaskani.soyadi;
            }

            var yazman = db.GOgrenciListesi.FirstOrDefault(x => x.ID == yazmanId);
            if (yazman != null)
            {
                b.yazmanAdi = yazman.adi;
                b.yazmanSoyadi = yazman.soyadi;
            }

            db.SaveChanges();

            if (baskanYarId != null)
            {
                GYonetimKurulu baskanYar = db.GYonetimKurulu.FirstOrDefault(x => x.unvan == "Başkan Yardımcısı");
                GOgrenciListesi baskanYard = db.GOgrenciListesi.FirstOrDefault(x => x.ID == baskanYarId);
                if (db.GYonetimKurulu.FirstOrDefault(x =>
                        x.ogrNo == baskanYard.ogrNo && x.unvan != "Başkan Yardımcısı") == null
                    && db.GDenetimKurulu.FirstOrDefault(x => x.ogrNo == baskanYard.ogrNo) == null)
                {
                    if (baskanYar == null)
                    {
                        baskanYar = new GYonetimKurulu();
                        baskanYar.GuncelleID = b.ID;
                        baskanYar.adi = baskanYard.adi;
                        baskanYar.soyadi = baskanYard.soyadi;
                        baskanYar.unvan = "Başkan Yardımcısı";
                        baskanYar.ogrNo = baskanYard.ogrNo;
                        baskanYar.fakulte = baskanYard.fak;
                        baskanYar.tc = baskanYard.tc;
                        baskanYar.gsm = baskanYard.tel;
                        baskanYar.eMail = baskanYard.mail;
                        db.GYonetimKurulu.Add(baskanYar);
                        db.SaveChanges();
                    }
                    else
                    {
                        baskanYar.GuncelleID = b.ID;
                        baskanYar.adi = baskanYard.adi;
                        baskanYar.soyadi = baskanYard.soyadi;
                        baskanYar.unvan = "Başkan Yardımcısı";
                        baskanYar.ogrNo = baskanYard.ogrNo;
                        baskanYar.fakulte = baskanYard.fak;
                        baskanYar.tc = baskanYard.tc;
                        baskanYar.gsm = baskanYard.tel;
                        baskanYar.eMail = baskanYard.mail;
                        db.SaveChanges();
                    }
                }
                else
                {
                    hata = 1;
                }
            }

            if (saymanId != null)
            {
                GYonetimKurulu sayman = db.GYonetimKurulu.FirstOrDefault(x => x.unvan == "Sayman");
                GOgrenciListesi saymann = db.GOgrenciListesi.FirstOrDefault(x => x.ID == saymanId);
                if (db.GYonetimKurulu.FirstOrDefault(x =>
                        x.ogrNo == saymann.ogrNo && x.unvan != "Sayman") == null
                    && db.GDenetimKurulu.FirstOrDefault(x => x.ogrNo == saymann.ogrNo) == null)
                {
                    if (sayman == null)
                    {
                        sayman = new GYonetimKurulu();
                        sayman.GuncelleID = b.ID;
                        sayman.adi = saymann.adi;
                        sayman.soyadi = saymann.soyadi;
                        sayman.unvan = "Sayman";
                        sayman.ogrNo = saymann.ogrNo;
                        sayman.fakulte = saymann.fak;
                        sayman.tc = saymann.tc;
                        sayman.gsm = saymann.tel;
                        sayman.eMail = saymann.mail;
                        db.GYonetimKurulu.Add(sayman);
                        db.SaveChanges();
                    }
                    else
                    {
                        sayman.GuncelleID = b.ID;
                        sayman.adi = saymann.adi;
                        sayman.soyadi = saymann.soyadi;
                        sayman.unvan = "Sayman";
                        sayman.ogrNo = saymann.ogrNo;
                        sayman.fakulte = saymann.fak;
                        sayman.tc = saymann.tc;
                        sayman.gsm = saymann.tel;
                        sayman.eMail = saymann.mail;
                        db.SaveChanges();
                    }
                }
                else
                {
                    hata = 2;
                }
            }

            if (sekreterId != null)
            {
                GYonetimKurulu sekreter = db.GYonetimKurulu.FirstOrDefault(x => x.unvan == "Sekreter");
                GOgrenciListesi sekreter1 = db.GOgrenciListesi.FirstOrDefault(x => x.ID == sekreterId);
                if (db.GYonetimKurulu.FirstOrDefault(x => x.ogrNo == sekreter1.ogrNo && x.unvan != "Sekreter") == null
                    && db.GDenetimKurulu.FirstOrDefault(x => x.ogrNo == sekreter1.ogrNo) == null)
                {
                    if (sekreter == null)
                    {
                        sekreter = new GYonetimKurulu();
                        sekreter.GuncelleID = b.ID;
                        sekreter.adi = sekreter1.adi;
                        sekreter.soyadi = sekreter1.soyadi;
                        sekreter.unvan = "Sekreter";
                        sekreter.ogrNo = sekreter1.ogrNo;
                        sekreter.fakulte = sekreter1.fak;
                        sekreter.tc = sekreter1.tc;
                        sekreter.gsm = sekreter1.tel;
                        sekreter.eMail = sekreter1.mail;
                        db.GYonetimKurulu.Add(sekreter);
                        db.SaveChanges();
                    }
                    else
                    {
                        sekreter.GuncelleID = b.ID;
                        sekreter.adi = sekreter1.adi;
                        sekreter.soyadi = sekreter1.soyadi;
                        sekreter.unvan = "Sekreter";
                        sekreter.ogrNo = sekreter1.ogrNo;
                        sekreter.fakulte = sekreter1.fak;
                        sekreter.tc = sekreter1.tc;
                        sekreter.gsm = sekreter1.tel;
                        sekreter.eMail = sekreter1.mail;
                        db.SaveChanges();
                    }
                }
                else
                {
                    hata = 3;
                }
            }

            TempData["Eklendi"] = "Girmiş Olduğunuz Verileriniz Eklenmiştir. Lütfen Aşağıdaki Formdan Kontrol Ediniz.";

            return RedirectToAction("form7");

        }

        [HttpPost]
        public ActionResult form7YonetimBaskan(int? yonetimbaskan)
        {
            Kullanici k = (Kullanici)Session["Kullanici"];
            Guncelle b = db.Guncelle.Where(x => x.kullanıcıID == k.ID).FirstOrDefault();

            GOgrenciListesi baskan = db.GOgrenciListesi.Where(x => x.ID == yonetimbaskan).SingleOrDefault();


            if(yonetimbaskan != null)
            { 
            if (baskan !=null)
            {
                if (b.GYonetimKurulu.Where(x=>x.unvan == "Yönetim Kurulu Başkanı" && x.GuncelleID==b.ID).SingleOrDefault() != null)
                {
                    GYonetimKurulu gbaskan = db.GYonetimKurulu.Where(x => x.unvan == "Yönetim Kurulu Başkanı" && x.GuncelleID == b.ID).SingleOrDefault();
                    gbaskan.adi = baskan.adi;
                    gbaskan.soyadi = baskan.soyadi;
                    gbaskan.ogrNo = baskan.ogrNo;
                    gbaskan.tc = baskan.tc;
                    gbaskan.unvan = "Yönetim Kurulu Başkanı";
                    gbaskan.gsm = baskan.tel;
                    db.SaveChanges();
                    TempData["Eklendi"] = "Girmiş Olduğunuz Verileriniz Eklenmiştir. Lütfen Aşağıdaki Formdan Kontrol Ediniz.";
                }
                else
                {

                    GYonetimKurulu g = new GYonetimKurulu();
                    g.adi = baskan.adi;
                    g.soyadi = baskan.soyadi;
                    g.ogrNo = baskan.ogrNo;
                    g.tc = baskan.tc;
                    g.unvan = "Yönetim Kurulu Başkanı";
                    g.gsm = baskan.tel;
                    g.GuncelleID = b.ID;
                    db.GYonetimKurulu.Add(g);
                    db.SaveChanges();
                    TempData["Eklendi"] = "Girmiş Olduğunuz Verileriniz Eklenmiştir. Lütfen Aşağıdaki Formdan Kontrol Ediniz.";
                    }

            }
            }
            return RedirectToAction("form7");
        }


        public ActionResult yonetimKuruluUyeEkle(int id)
        {
            Kullanici k = (Kullanici)Session["Kullanici"];
            Guncelle b = db.Guncelle.Where(x => x.kullanıcıID == k.ID).FirstOrDefault();

            int sayac = db.GYonetimKurulu.Where(x => x.unvan == "Üye" && x.GuncelleID == b.ID).Count();
            if (sayac < 3)
            {
                var ogrenci = db.GOgrenciListesi.FirstOrDefault(x => x.ID == id);
                if (db.GYonetimKurulu.FirstOrDefault(x => x.ogrNo == ogrenci.ogrNo) == null
                    && db.GDenetimKurulu.FirstOrDefault(x => x.ogrNo == ogrenci.ogrNo) == null)
                {
                    GYonetimKurulu uye = new GYonetimKurulu();
                    uye.GuncelleID = b.ID;
                    uye.unvan = "Üye";
                    uye.adi = ogrenci.adi;
                    uye.soyadi = ogrenci.soyadi;
                    uye.ogrNo = ogrenci.ogrNo;
                    uye.fakulte = ogrenci.fak;
                    uye.tc = ogrenci.tc;
                    uye.gsm = ogrenci.tel;
                    uye.eMail = ogrenci.mail;
                    db.GYonetimKurulu.Add(uye);
                    db.SaveChanges();
                    TempData["Eklendi"] = "Girmiş Olduğunuz Uye Eklenmiştir. Lütfen Aşağıdaki Formdan Kontrol Ediniz.";

                }
                else
                {
                    hata = 4;
                }

                return RedirectToAction("form7");
            }
            else
            {
                hata = 5;
                return RedirectToAction("form7");
            }

        }

        public PartialViewResult uyeListesiWidget()
        {
            Kullanici k = (Kullanici)Session["Kullanici"];
            Guncelle b = db.Guncelle.Where(x => x.kullanıcıID == k.ID).FirstOrDefault();
            List<GYonetimKurulu> yk = db.GYonetimKurulu.Where(x => x.unvan == "Üye").ToList();
            ViewBag.yk = yk;

            var yk1 = db.GYonetimKurulu.Where(x => x.GuncelleID == b.ID).ToList();
            ViewBag.yk1 = yk1;

            var dk = db.GDenetimKurulu.Where(x => x.GuncelleID == b.ID).ToList();
            ViewBag.dk = dk;
            return PartialView();
        }

        public PartialViewResult yKuruluListesiWidget()
        {
            Kullanici k = (Kullanici)Session["Kullanici"];
            Guncelle b = db.Guncelle.Where(x => x.kullanıcıID == k.ID).FirstOrDefault();

            var yk1 = db.GYonetimKurulu.Where(x => x.GuncelleID == b.ID).ToList();
            ViewBag.yk1 = yk1;

            return PartialView();
        }

        public PartialViewResult dKuruluListesiWidget()
        {
            Kullanici k = (Kullanici)Session["Kullanici"];
            Guncelle b = db.Guncelle.Where(x => x.kullanıcıID == k.ID).FirstOrDefault();

            var dk = db.GDenetimKurulu.Where(x => x.GuncelleID == b.ID).ToList();
            ViewBag.dk = dk;
            return PartialView();
        }

        public ActionResult uyeListesiSil(int id)
        {
            Kullanici k = (Kullanici)Session["Kullanici"];
            Guncelle b = db.Guncelle.Where(x => x.kullanıcıID == k.ID).FirstOrDefault();

            GYonetimKurulu ul = db.GYonetimKurulu.Where(x => x.ID == id).FirstOrDefault();
            db.GYonetimKurulu.Remove(ul);
            db.SaveChanges();
            return RedirectToAction("form7");
        }

        public ActionResult denetimKuruluUyeEkle(int id)
        {
            Kullanici k = (Kullanici)Session["Kullanici"];
            Guncelle b = db.Guncelle.Where(x => x.kullanıcıID == k.ID).FirstOrDefault();

            int sayac = db.GDenetimKurulu.Where(x => x.unvan == "Üye" && x.GuncelleID == b.ID).Count();
            if (sayac < 2)
            {
                var ogrenci = db.GOgrenciListesi.FirstOrDefault(x => x.ID == id);
                if (db.GYonetimKurulu.FirstOrDefault(x => x.ogrNo == ogrenci.ogrNo) == null
                    && db.GDenetimKurulu.FirstOrDefault(x => x.ogrNo == ogrenci.ogrNo) == null)
                {
                    GDenetimKurulu uye = new GDenetimKurulu();
                    uye.GuncelleID = b.ID;
                    uye.unvan = "Üye";
                    uye.adi = ogrenci.adi;
                    uye.soyadi = ogrenci.soyadi;
                    uye.ogrNo = ogrenci.ogrNo;

                    db.GDenetimKurulu.Add(uye);
                    db.SaveChanges();

                    TempData["Eklendi"] = "Girmiş Olduğunuz Uye Eklenmiştir. Lütfen Aşağıdaki Formdan Kontrol Ediniz.";
                }
                else
                {
                    hata = 6;
                }

                return RedirectToAction("form7");
            }
            else
            {
                hata = 7;
                return RedirectToAction("form7");
            }

        }

        [HttpPost]
        public ActionResult form7DenetimKuruluKaydet(int? baskanId)
        {
            Kullanici k = (Kullanici)Session["Kullanici"];
            Guncelle b = db.Guncelle.Where(x => x.kullanıcıID == k.ID).FirstOrDefault();

            var yk1 = db.GYonetimKurulu.Where(x => x.GuncelleID == b.ID).ToList();
            ViewBag.yk1 = yk1;

            var dk = db.GDenetimKurulu.Where(x => x.GuncelleID == b.ID).ToList();
            ViewBag.dk = dk;

            var BaskanYar = db.GYonetimKurulu.FirstOrDefault(x => x.unvan == "Başkan Yardımcısı");
            ViewBag.BaskanYar = BaskanYar;

            if (baskanId != null)
            {
                GDenetimKurulu baskan = db.GDenetimKurulu.FirstOrDefault(x => x.unvan == "Denetim Kurulu Başkanı");
                GOgrenciListesi dBaskan = db.GOgrenciListesi.FirstOrDefault(x => x.ID == baskanId);
                if (db.GDenetimKurulu.FirstOrDefault(
                        x => x.ogrNo == dBaskan.ogrNo && x.unvan != "Denetim Kurulu Başkanı") == null
                    && db.GYonetimKurulu.FirstOrDefault(x => x.ogrNo == dBaskan.ogrNo) == null)
                {
                    if (baskan == null)
                    {
                        baskan = new GDenetimKurulu();
                        baskan.GuncelleID = b.ID;
                        baskan.adi = dBaskan.adi;
                        baskan.soyadi = dBaskan.soyadi;
                        baskan.unvan = "Denetim Kurulu Başkanı";
                        baskan.ogrNo = dBaskan.ogrNo;
                        db.GDenetimKurulu.Add(baskan);
                        db.SaveChanges();

                        TempData["Eklendi"] = "Girmiş Olduğunuz Verileriniz Eklenmiştir. Lütfen Aşağıdaki Formdan Kontrol Ediniz.";
                    }
                    else
                    {
                        baskan.GuncelleID = b.ID;
                        baskan.adi = dBaskan.adi;
                        baskan.soyadi = dBaskan.soyadi;
                        baskan.unvan = "Denetim Kurulu Başkanı";
                        baskan.ogrNo = dBaskan.ogrNo;
                        db.SaveChanges();

                        TempData["Eklendi"] = "Girmiş Olduğunuz Verileriniz Eklenmiştir. Lütfen Aşağıdaki Formdan Kontrol Ediniz.";
                    }
                }
                else
                {
                    hata = 8;
                }
            }
            else
            {
                TempData["YönetimHata"] = "Denetim Kurulu Başkanını Seçmediniz.";
            }

            return RedirectToAction("form7");


        }

        public ActionResult dUyeListesiSil(int id)
        {
            Kullanici k = (Kullanici)Session["Kullanici"];
            Guncelle b = db.Guncelle.Where(x => x.kullanıcıID == k.ID).FirstOrDefault();

            GDenetimKurulu ul = db.GDenetimKurulu.Where(x => x.ID == id).FirstOrDefault();
            db.GDenetimKurulu.Remove(ul);
            db.SaveChanges();
            return RedirectToAction("form7");
        }

        [HttpPost]
        public ActionResult etkButceKaydet(string etkinlik, string butce ,string kurul, string kUyeSecimi)
        {
            Kullanici k = (Kullanici)Session["Kullanici"];
            Guncelle b = db.Guncelle.Where(x => x.kullanıcıID == k.ID).FirstOrDefault();

            if (etkinlik != "")
            {
                b.etkinlik = etkinlik;
            }

            if (butce != "")
            {
                b.butce = butce;
            }

            if (kurul != "")
            {
                b.baskaKurul = kurul;
            }

            if (kUyeSecimi != "")
            {
                b.bkUyeSecimi = kUyeSecimi;
            }

            db.SaveChanges();

            return RedirectToAction("form7");
        }

        [HttpGet]
        public ActionResult form8()
        {
            Kullanici k = (Kullanici)Session["Kullanici"];
            Guncelle b = db.Guncelle.Where(x => x.kullanıcıID == k.ID).FirstOrDefault();
            if (b.adimNo < 6)
            {
                ViewBag.Hata = "İlk Önce Diğer Formları Doldurmanız Gerekmektedir.";
                return View();
            }

            ViewBag.yonetimKurulu = db.GYonetimKurulu.Where(x => x.GuncelleID == b.ID).ToList();
            return View(b);
        }

        [HttpPost]
        public ActionResult form8TBilg(Guncelle veri)
        {
            Kullanici k = (Kullanici)Session["Kullanici"];
            Guncelle b = db.Guncelle.Where(x => x.kullanıcıID == k.ID).FirstOrDefault();

            if (veri.kurulusTarihi != null)
            {
                b.kurulusTarihi = veri.kurulusTarihi;
            }

            if (veri.toplantiNiteligi != null)
            {
                b.toplantiNiteligi = veri.toplantiNiteligi;
            }

            if (veri.tuzukDegisikligi != null)
            {
                b.tuzukDegisikligi = veri.tuzukDegisikligi;
            }

            if (veri.gUyeSayisi != null)
            {
                b.gUyeSayisi = veri.gUyeSayisi;
            }

            db.SaveChanges();

            return RedirectToAction("form8");
        }

        [HttpPost]
        public ActionResult form8Uye(GYonetimKurulu veri)
        {
            Kullanici k = (Kullanici)Session["Kullanici"];
            Guncelle b = db.Guncelle.Where(x => x.kullanıcıID == k.ID).FirstOrDefault();

            var uye = db.GYonetimKurulu.FirstOrDefault(x => x.ID == veri.ID && x.GuncelleID == b.ID);

            if (veri.bolum != null)
            {
                uye.bolum = veri.bolum;
            }

            if (veri.adres != null)
            {
                uye.adres = veri.adres;
            }

            if (veri.evTel != null)
            {
                uye.evTel = veri.evTel;
            }

            db.SaveChanges();

            return RedirectToAction("form8");
        }

        public ActionResult form8Kaydet()
        {
            Kullanici k = (Kullanici)Session["Kullanici"];
            Guncelle b = db.Guncelle.Where(x => x.kullanıcıID == k.ID).FirstOrDefault();

            int sayac = 0;
            bool gecis = false;
            var yonetim = db.GYonetimKurulu.Where(x => x.GuncelleID == b.ID).ToList();

            foreach (var item in yonetim)
            {
                if (item.adres != null && item.bolum != null)
                {
                    sayac++;
                }
            }

            if (b.kurulusTarihi != null && b.gUyeSayisi != null && b.toplantiNiteligi != null &&
                b.tuzukDegisikligi != null)
            {
                sayac++;
            }

            if (b.adimNo == 6 && sayac == 8)
            {
                b.adimNo = 7;
                db.SaveChanges();
            }
            else
            {
                TempData["YönetimHata"] = "Formda Eksik Bilgi Bulunmaktadır. Lütfen Aşağıdaki Formdan Kontrol Ediniz.";
            }

            return RedirectToAction("form8");

        }

        // Güncelleme Tamamlama
        public ActionResult guncellemeTamamla()
        {
            Kullanici k = (Kullanici)Session["Kullanici"];
            Guncelle b = db.Guncelle.Where(x => x.kullanıcıID == k.ID).FirstOrDefault();
            return View(b);
        }
    }
}