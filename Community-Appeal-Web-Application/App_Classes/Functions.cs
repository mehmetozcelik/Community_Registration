using Community_Appeal_Web_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Community_Appeal_Web_Application.App_Classes
{
    public class Functions
    {
        public static OgrenciListesi OgrenciSorgula(OgrenciListesi Ogrenci)
        {
            // Ogrenci içerisinden TC veya Numarasından gelen öğrenici bilgileri eklenip döndürülecek. böyle bir öğrenci yok ise null dönülecek.
            // .
            return Ogrenci;
        }

        public static GOgrenciListesi GuncellemeOgrenciSorgula(GOgrenciListesi Ogrenci)
        {
            // Ogrenci içerisinden TC veya Numarasından gelen öğrenici bilgileri eklenip döndürülecek. böyle bir öğrenci yok ise null dönülecek.
            // .
            return Ogrenci;
        }

        public static bool SifreYenile(string mail)
        {
            int _min = 1000;
            int _max = 9999;
            Random _rdm = new Random();
            int rnd = _rdm.Next(_min, _max); // rnd yeni sifre
            // Buraya yeni sifreyi mail gönderecek fonksiyon gelecek

            return true;
        }

        public static string IlkHarfleriBuyut(string metin)
        {
            string Degisen = "";
            try
            {
                System.Globalization.CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
                System.Globalization.TextInfo textInfo = cultureInfo.TextInfo;
                Degisen = textInfo.ToTitleCase(metin);
            }
            catch (Exception e)
            {
                Degisen = metin;
            }
            return Degisen;
        }
    }
}