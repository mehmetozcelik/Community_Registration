namespace Community_Appeal_Web_Application.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GYonetimKurulu")]
    public partial class GYonetimKurulu
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string adi { get; set; }

        [StringLength(50)]
        public string soyadi { get; set; }

        [StringLength(50)]
        public string unvan { get; set; }

        [StringLength(100)]
        public string bolum { get; set; }

        [StringLength(500)]
        public string adres { get; set; }

        [StringLength(50)]
        public string tc { get; set; }

        [StringLength(100)]
        public string fakulte { get; set; }

        [StringLength(50)]
        public string ogrNo { get; set; }

        [StringLength(50)]
        public string gsm { get; set; }

        [StringLength(50)]
        public string evTel { get; set; }

        [StringLength(100)]
        public string eMail { get; set; }

        public int? GuncelleID { get; set; }

        public virtual Guncelle Guncelle { get; set; }
    }
}
