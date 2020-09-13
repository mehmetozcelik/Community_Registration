namespace Community_Appeal_Web_Application.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Kullanici")]
    public partial class Kullanici
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Kullanici()
        {
            Basvuru = new HashSet<Basvuru>();
            Guncelle = new HashSet<Guncelle>();
        }

        public int ID { get; set; }

        [StringLength(100)]
        public string adi { get; set; }

        [StringLength(100)]
        public string soyadi { get; set; }

        [StringLength(100)]
        public string ogrMail { get; set; }

        [StringLength(100)]
        public string sifre { get; set; }

        [StringLength(100)]
        public string ogrNo { get; set; }

        [StringLength(100)]
        public string tc { get; set; }

        [Column(TypeName = "date")]
        public DateTime? kayitTarihi { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Basvuru> Basvuru { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Guncelle> Guncelle { get; set; }
    }
}
