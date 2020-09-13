namespace Community_Appeal_Web_Application.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GDanisman")]
    public partial class GDanisman
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string adi { get; set; }

        [StringLength(50)]
        public string soyadi { get; set; }

        [StringLength(50)]
        public string unvan { get; set; }

        [StringLength(150)]
        public string akademikBirim { get; set; }

        public int? GuncelleID { get; set; }

        public bool? aktif { get; set; }

        public virtual Guncelle Guncelle { get; set; }
    }
}
