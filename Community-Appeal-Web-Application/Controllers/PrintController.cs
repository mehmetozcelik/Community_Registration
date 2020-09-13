using Community_Appeal_Web_Application.Models;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Community_Appeal_Web_Application.Controllers
{
    public class PrintController : Controller
    {

        CommunityContext db = new CommunityContext();

        // Başvuru Print

        public ActionResult Form1_admin(int ID)
        {
            Basvuru b = db.Basvuru.Where(x => x.kullanıcıID == ID).FirstOrDefault();
            var report = new ViewAsPdf("Form1_admin", b)
            {

            };
            return report;
        }

        public ActionResult Form1()
        {
            Kullanici k = (Kullanici)Session["Kullanici"];
            Basvuru b = db.Basvuru.Where(x => x.kullanıcıID == k.ID).FirstOrDefault();
            var report = new ViewAsPdf("Form1",b)
            {

            };
            return report;
        }


        public ActionResult Form2_admin(int ID)
        {
            Basvuru b = db.Basvuru.Where(x => x.kullanıcıID == ID).FirstOrDefault();
            List<OgrenciListesi> ol = db.OgrenciListesi.Where(x => x.basvuruID == b.ID).ToList();
            ViewBag.ol = ol;
            var report = new ViewAsPdf("Form2_admin",b)
            {
                PageMargins = { Left = 20, Bottom = 20, Right = 20, Top = 20 },
                PageOrientation = Rotativa.Options.Orientation.Landscape,
            };
            return report;
        }

        public ActionResult Form2()
        {
            Kullanici k = (Kullanici)Session["Kullanici"];
            Basvuru b = db.Basvuru.Where(x => x.kullanıcıID == k.ID).FirstOrDefault();
            List<OgrenciListesi> ol = db.OgrenciListesi.Where(x => x.basvuruID == b.ID).ToList();
            ViewBag.ol = ol;
            var report = new ViewAsPdf("Form2")
            {
                PageMargins = { Left = 20, Bottom = 20, Right = 20, Top = 20 },
                PageOrientation = Rotativa.Options.Orientation.Landscape,
            };
            return report;
        }

        public ActionResult Form3_admin(int ID)
        {
            Basvuru b = db.Basvuru.Where(x => x.kullanıcıID == ID).FirstOrDefault();
            var ogrnci = db.YonetimKurulu.Where(x => x.basvuruID == b.ID && x.Baskan == true).FirstOrDefault(); ;
            ViewBag.baskan = ogrnci;
            var report = new ViewAsPdf("Form3_admin", b)
            {

            };
            return report;
        }

        public ActionResult Form3()
        {
            Kullanici k = (Kullanici)Session["Kullanici"];
            Basvuru b = db.Basvuru.Where(x => x.kullanıcıID == k.ID).FirstOrDefault();
            var ogrnci = db.YonetimKurulu.Where(x => x.basvuruID == b.ID && x.Baskan == true).FirstOrDefault(); ;
            ViewBag.baskan = ogrnci;
            var report = new ViewAsPdf("Form3",b)
            {
               
            };
            return report;
        }

        public ActionResult Form4()
        {
            Kullanici k = (Kullanici)Session["Kullanici"];
            Basvuru g = db.Basvuru.Where(x => x.kullanıcıID == k.ID).FirstOrDefault();
            List<FaliyetPlani> fp = db.FaliyetPlani.Where(x => x.faliyetID == g.ID).ToList();
            ViewBag.fp = fp;

            var report = new ViewAsPdf("Form4", g)
            {

            };
            return report;
        }

        public ActionResult Form4_admin(int ID)
        {
            Basvuru g = db.Basvuru.Where(x => x.kullanıcıID == ID).FirstOrDefault();
            List<FaliyetPlani> fp = db.FaliyetPlani.Where(x => x.faliyetID == g.ID).ToList();
            ViewBag.fp = fp;
            var report = new ViewAsPdf("Form4_admin", g)
            {

            };
            return report;
        }


        public ActionResult Form5()
        {
            Kullanici k = (Kullanici)Session["Kullanici"];
            Basvuru g = db.Basvuru.Where(x => x.kullanıcıID == k.ID).FirstOrDefault();
            List<Danisman> DL = db.Danisman.Where(x => x.basvuruID == g.ID).ToList();
            ViewBag.DL = DL;
            var report = new ViewAsPdf("Form5", g)
            {

            };
            return report;
        }

        public ActionResult Form5_admin(int ID)
        {
            Basvuru g = db.Basvuru.Where(x => x.kullanıcıID == ID).FirstOrDefault();
            List<Danisman> DL = db.Danisman.Where(x => x.basvuruID == g.ID).ToList();
            ViewBag.DL = DL;
            var report = new ViewAsPdf("Form5_admin", g)
            {

            };
            return report;
        }

        public ActionResult Form6()
        {
            Kullanici k = (Kullanici)Session["Kullanici"];
            Basvuru g = db.Basvuru.Where(x => x.kullanıcıID == k.ID).FirstOrDefault();
            Danisman dl1 = db.Danisman.FirstOrDefault(x => x.basvuruID == g.ID && x.aktif == true);
            ViewBag.dl1 = dl1;
            var report = new ViewAsPdf("Form6", g)
            {

            };
            return report;
        }

        public ActionResult Form6_admin(int ID)
        {
            Basvuru g = db.Basvuru.Where(x => x.kullanıcıID == ID).FirstOrDefault();
            Danisman dl1 = db.Danisman.FirstOrDefault(x => x.basvuruID == g.ID && x.aktif == true);
            ViewBag.dl1 = dl1;
            var report = new ViewAsPdf("Form6_admin", g)
            {

            };
            return report;
        }

        public ActionResult Form7()
        {
            Kullanici k = (Kullanici)Session["Kullanici"];
            Basvuru g = db.Basvuru.Where(x => x.kullanıcıID == k.ID).FirstOrDefault();

            var BaskanYar = db.YonetimKurulu.FirstOrDefault(x => x.unvan == "Başkan Yardımcısı");
            ViewBag.BaskanYar = BaskanYar;

            var yk1 = db.YonetimKurulu.Where(x => x.basvuruID == g.ID).ToList();
            ViewBag.yk1 = yk1;

            var dk = db.DenetimKurulu.Where(x => x.basvuruID == g.ID).ToList();
            ViewBag.dk = dk;

            var report = new ViewAsPdf("Form7", g)
            {

            };
            return report;
        }

        public ActionResult Form7_admin(int ID)
        {
            Basvuru g = db.Basvuru.Where(x => x.kullanıcıID == ID).FirstOrDefault();

            var BaskanYar = db.YonetimKurulu.FirstOrDefault(x => x.unvan == "Başkan Yardımcısı");
            ViewBag.BaskanYar = BaskanYar;

            var yk1 = db.YonetimKurulu.Where(x => x.basvuruID == g.ID).ToList();
            ViewBag.yk1 = yk1;

            var dk = db.DenetimKurulu.Where(x => x.basvuruID == g.ID).ToList();
            ViewBag.dk = dk;

            var report = new ViewAsPdf("Form7_admin", g)
            {

            };
            return report;
        }

        public ActionResult Form8()
        {
            Kullanici k = (Kullanici)Session["Kullanici"];
            Basvuru g = db.Basvuru.Where(x => x.kullanıcıID == k.ID).FirstOrDefault();

            ViewBag.yonetimKurulu = db.YonetimKurulu.Where(x => x.basvuruID == g.ID).ToList();

            var report = new ViewAsPdf("Form8", g)
            {

            };
            return report;
        }

        public ActionResult Form8_admin(int ID)
        {
            Basvuru g = db.Basvuru.Where(x => x.kullanıcıID == ID).FirstOrDefault();

            ViewBag.yonetimKurulu = db.YonetimKurulu.Where(x => x.basvuruID == g.ID).ToList();

            var report = new ViewAsPdf("Form8_Admin", g)
            {

            };
            return report;
        }

        // Güncelleme Print

        public ActionResult GForm1()
        {
            Kullanici k = (Kullanici)Session["Kullanici"];
            Guncelle g = db.Guncelle.Where(x => x.kullanıcıID == k.ID).FirstOrDefault();
            var report = new ViewAsPdf("GForm1", g)
            {

            };
            return report;
        }

        public ActionResult GForm1_admin(int ID)
        {
            Guncelle g = db.Guncelle.Where(x => x.kullanıcıID == ID).FirstOrDefault();
            var report = new ViewAsPdf("GForm1_admin", g)
            {

            };
            return report;
        }


        public ActionResult GForm2()
        {
            Kullanici k = (Kullanici)Session["Kullanici"];
            Guncelle g = db.Guncelle.Where(x => x.kullanıcıID == k.ID).FirstOrDefault();
            List<GOgrenciListesi> ol = db.GOgrenciListesi.Where(x => x.GuncelleID == g.ID).ToList();
            ViewBag.ol = ol;
            var report = new ViewAsPdf("GForm2", g)
            {
                PageMargins = { Left = 20, Bottom = 20, Right = 20, Top = 20 },
                PageOrientation = Rotativa.Options.Orientation.Landscape,
            };
            return report;
        }

        public ActionResult GForm2_admin(int ID)
        {
            Guncelle g = db.Guncelle.Where(x => x.kullanıcıID == ID).FirstOrDefault();
            List<GOgrenciListesi> ol = db.GOgrenciListesi.Where(x => x.GuncelleID == g.ID).ToList();
            ViewBag.ol = ol;
            var report = new ViewAsPdf("GForm2_admin", g)
            {
                PageMargins = { Left = 20, Bottom = 20, Right = 20, Top = 20 },
                PageOrientation = Rotativa.Options.Orientation.Landscape,
            };
            return report;
        }


        public ActionResult GForm3()
        {
            Kullanici k = (Kullanici)Session["Kullanici"];
            Guncelle g = db.Guncelle.Where(x => x.kullanıcıID == k.ID).FirstOrDefault();
            List<GFaliyetPlani> fp = db.GFaliyetPlani.Where(x => x.faliyetID == g.ID).ToList();
            ViewBag.fp = fp;
            var report = new ViewAsPdf("GForm3", g)
            {
                PageMargins = { Left = 20, Bottom = 20, Right = 20, Top = 20 },
                PageOrientation = Rotativa.Options.Orientation.Landscape,
            };
            return report;
        }

        public ActionResult GForm3_admin(int ID)
        {
            Guncelle g = db.Guncelle.Where(x => x.kullanıcıID == ID).FirstOrDefault();
            List<GFaliyetPlani> fp = db.GFaliyetPlani.Where(x => x.faliyetID == g.ID).ToList();
            ViewBag.fp = fp;
            var report = new ViewAsPdf("GForm3_admin", g)
            {
                PageMargins = { Left = 20, Bottom = 20, Right = 20, Top = 20 },
                PageOrientation = Rotativa.Options.Orientation.Landscape,
            };
            return report;
        }


        public ActionResult GForm4()
        {
            Kullanici k = (Kullanici)Session["Kullanici"];
            Guncelle g = db.Guncelle.Where(x => x.kullanıcıID == k.ID).FirstOrDefault();
            List<GDanisman> DL = db.GDanisman.Where(x => x.GuncelleID == g.ID).ToList();
            ViewBag.DL = DL;
            var report = new ViewAsPdf("GForm4", g)
            {

            };
            return report;
        }

        public ActionResult GForm4_admin(int ID)
        {
            Guncelle g = db.Guncelle.Where(x => x.kullanıcıID == ID).FirstOrDefault();
            List<GDanisman> DL = db.GDanisman.Where(x => x.GuncelleID == g.ID).ToList();
            ViewBag.DL = DL;
            var report = new ViewAsPdf("GForm4_admin", g)
            {

            };
            return report;
        }

        public ActionResult GForm7()
        {
            Kullanici k = (Kullanici)Session["Kullanici"];
            Guncelle g = db.Guncelle.Where(x => x.kullanıcıID == k.ID).FirstOrDefault();

            var BaskanYar = db.GYonetimKurulu.FirstOrDefault(x => x.unvan == "Başkan Yardımcısı");
            ViewBag.BaskanYar = BaskanYar;

            var yk1 = db.GYonetimKurulu.Where(x => x.GuncelleID == g.ID).ToList();
            ViewBag.yk1 = yk1;

            var dk = db.GDenetimKurulu.Where(x => x.GuncelleID == g.ID).ToList();
            ViewBag.dk = dk;

            var report = new ViewAsPdf("GForm7", g)
            {

            };
            return report;
        }

        public ActionResult GForm7_admin(int ID)
        {
            Guncelle g = db.Guncelle.Where(x => x.ID == ID).FirstOrDefault();

            var BaskanYar = db.GYonetimKurulu.FirstOrDefault(x => x.unvan == "Başkan Yardımcısı");
            ViewBag.BaskanYar = BaskanYar;

            var yk1 = db.GYonetimKurulu.Where(x => x.GuncelleID == g.ID).ToList();
            ViewBag.yk1 = yk1;

            var dk = db.GDenetimKurulu.Where(x => x.GuncelleID == g.ID).ToList();
            ViewBag.dk = dk;

            var report = new ViewAsPdf("GForm7_admin", g)
            {

            };
            return report;
        }

        public ActionResult GForm8()
        {
            Kullanici k = (Kullanici)Session["Kullanici"];
            Guncelle g = db.Guncelle.Where(x => x.kullanıcıID == k.ID).FirstOrDefault();

            ViewBag.yonetimKurulu = db.GYonetimKurulu.Where(x => x.GuncelleID == g.ID).ToList();

            var report = new ViewAsPdf("GForm8", g)
            {

            };
            return report;
        }

        public ActionResult GForm8_admin(int ID)
        {
            Guncelle g = db.Guncelle.Where(x => x.ID == ID).FirstOrDefault();

            ViewBag.yonetimKurulu = db.GYonetimKurulu.Where(x => x.GuncelleID == g.ID).ToList();

            var report = new ViewAsPdf("GForm8_Admin", g)
            {

            };
            return report;
        }
    }
}