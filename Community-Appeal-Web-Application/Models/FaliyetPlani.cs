namespace Community_Appeal_Web_Application.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FaliyetPlani")]
    public partial class FaliyetPlani
    {
        public int ID { get; set; }

        public int? faliyetID { get; set; }

        [StringLength(100)]
        public string faliyetTürü { get; set; }

        [StringLength(500)]
        public string faliyetAmaci { get; set; }

        [Column(TypeName = "date")]
        public DateTime? tarih { get; set; }

        [StringLength(50)]
        public string saat { get; set; }

        [StringLength(50)]
        public string yerleske { get; set; }

        public virtual Basvuru Basvuru { get; set; }
    }
}
