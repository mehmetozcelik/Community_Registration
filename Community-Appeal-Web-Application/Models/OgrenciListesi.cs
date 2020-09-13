namespace Community_Appeal_Web_Application.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OgrenciListesi")]
    public partial class OgrenciListesi
    {
        public int ID { get; set; }

        [StringLength(100)]
        public string adi { get; set; }

        [StringLength(100)]
        public string soyadi { get; set; }

        [StringLength(200)]
        public string fak { get; set; }

        [StringLength(100)]
        public string tc { get; set; }

        [StringLength(100)]
        public string ogrNo { get; set; }

        [StringLength(100)]
        public string tel { get; set; }

        [StringLength(100)]
        public string mail { get; set; }

        public int? basvuruID { get; set; }

        public virtual Basvuru Basvuru { get; set; }
    }
}
