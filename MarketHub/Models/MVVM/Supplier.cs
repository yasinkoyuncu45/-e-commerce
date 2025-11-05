using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketHup.Models.MVVM
{
    public class Supplier
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("ID")]
        public int SupplierID { get; set; }

        [Required(ErrorMessage = "Marka Adı Zorunlu Alan")]
        [StringLength(50, ErrorMessage = "En Fazla 50 Karakter")]
        [DisplayName("Marka Adı")]
        public string? BrandName { get; set; }

        [DisplayName("Resim Seçiniz")]
        public string? PhotoPath { get; set; }

        [DisplayName("Aktif/Pasif")]
        public bool Active { get; set; }
    }
}
