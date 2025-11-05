using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketHup.Models.MVVM
{
    public class Category
    {
        //pramiry key, identity yes
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("ID")]//form da label türkçe görünecek 
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Kategori Adı Zorunlu Alan")]
        [DisplayName("Kategori Adı")] //form da görünecek hali
        [StringLength(50, ErrorMessage = "En Fazla 50 Karakter Girilebilir.")]
        public string? CategoryName { get; set; }

        [DisplayName("Üst Kategori")]
        public int? ParentID { get; set; }

        [DisplayName("Aktif/Pasif")]
        public bool Active { get; set; }
    }
}
