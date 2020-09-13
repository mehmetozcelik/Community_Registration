namespace Community_Appeal_Web_Application.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GDenetimKurulu")]
    public partial class GDenetimKurulu
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string adi { get; set; }

        [StringLength(50)]
        public string soyadi { get; set; }

        [StringLength(50)]
        public string unvan { get; set; }

        public int? GuncelleID { get; set; }

        [StringLength(50)]
        public string ogrNo { get; set; }

        public virtual Guncelle Guncelle { get; set; }
    }
}
