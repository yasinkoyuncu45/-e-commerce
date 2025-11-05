using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketHup.Models.MVVM
{
    public class Status
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("ID")]
        public int StatusID { get; set; }

        [DisplayName("Statü Adı")]
        [Required(ErrorMessage = "Statü Adı Zorunlu Alan")]
        [StringLength(50, ErrorMessage = "En Fazla 50 karakter")]
        public string? StatusName { get; set; }= string.Empty;

        [DisplayName("Aktif/Pasif")]
        public bool Active { get; set; }
    }
}
