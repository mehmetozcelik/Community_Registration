namespace Community_Appeal_Web_Application.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Basvuru")]
    public partial class Basvuru
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Basvuru()
        {
            Danisman = new HashSet<Danisman>();
            DenetimKurulu = new HashSet<DenetimKurulu>();
            FaliyetPlani = new HashSet<FaliyetPlani>();
            OgrenciListesi = new HashSet<OgrenciListesi>();
            YonetimKurulu = new HashSet<YonetimKurulu>();
        }

        public int ID { get; set; }

        [StringLength(150)]
        public string toplulukAdi { get; set; }

        [StringLength(500)]
        public string toplulukAmac { get; set; }

        [StringLength(100)]
        public string baskanAdi { get; set; }

        [StringLength(100)]
        public string baskanSoyadi { get; set; }

        [Column(TypeName = "date")]
        public DateTime? tarih { get; set; }

        [StringLength(50)]
        public string saat { get; set; }

        [StringLength(100)]
        public string mekan { get; set; }

        public int? toplantiNo { get; set; }

        public int? adimNo { get; set; }

        public int? uyeSayisi { get; set; }

        [StringLength(50)]
        public string akademikYıl { get; set; }

        [Column(TypeName = "date")]
        public DateTime? toplantiTarihi { get; set; }

        [StringLength(100)]
        public string marscı { get; set; }

        [StringLength(100)]
        public string divanBaskanAdi { get; set; }

        [StringLength(100)]
        public string divanBaskanSoyadi { get; set; }

        public bool? toplantiNiteligi { get; set; }

        [Column(TypeName = "date")]
        public DateTime? kurulusTarihi { get; set; }

        public bool? tuzukDegisikligi { get; set; }

        public int? kullanıcıID { get; set; }

        [StringLength(50)]
        public string yazmanAdi { get; set; }

        [StringLength(50)]
        public string yazmanSoyadi { get; set; }

        public int? gUyeSayisi { get; set; }

        [StringLength(400)]
        public string etkinlik { get; set; }

        [StringLength(400)]
        public string butce { get; set; }

        [StringLength(400)]
        public string baskaKurul { get; set; }

        [StringLength(400)]
        public string bkUyeSecimi { get; set; }

        public bool? kapat { get; set; }

        public virtual Kullanici Kullanici { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Danisman> Danisman { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DenetimKurulu> DenetimKurulu { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FaliyetPlani> FaliyetPlani { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OgrenciListesi> OgrenciListesi { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<YonetimKurulu> YonetimKurulu { get; set; }
    }
}
