using Community_Appeal_Web_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Community_Appeal_Web_Application.App_Classes
{
    public class BasvuruVeGuncelle
    {
        public static bool guncelleStatus { get; set; }
        public static bool basvuruStatus { get; set; }

        public static Guncelle guncelle(int KullaniciID)
        {
            CommunityContext db = new CommunityContext();
            Kullanici k = db.Kullanici.Where(x => x.ID == KullaniciID).FirstOrDefault();

            Guncelle g = db.Guncelle.Where(x => x.kullanıcıID == KullaniciID).FirstOrDefault();
            return g;
        }

        public static Basvuru basvuru(int KullaniciID)
        {
            CommunityContext db = new CommunityContext();
            Basvuru b = db.Basvuru.Where(x => x.kullanıcıID == KullaniciID).FirstOrDefault();
            return b;
        }
    }
}