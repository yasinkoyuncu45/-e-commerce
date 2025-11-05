using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketHup.Models.MVVM
{
    public class Communication
    {
        //pramiry key, identity yes
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("ID")]//form da label türkçe görünecek 
        public int ID { get; set; }

        [Required(ErrorMessage = "Adres Adı Zorunlu Alan")]
        [DisplayName("Adres")] //form da görünecek hali
        public string? Adress { get; set; }

        [Required(ErrorMessage = "Email Adı Zorunlu Alan")]
        [DisplayName("Email")] //form da görünecek hali
        public string? Email { get; set; }

        [Required(ErrorMessage = "Telefon Saatleri Adı Zorunlu Alan")]
        [DisplayName("Telefon")]
        public int Telephone { get; set; }

        [Required(ErrorMessage = "Çalışma Saatleri Adı Zorunlu Alan")]
        [DisplayName("Çalışma Saatleri")] //form da görünecek hali
        public string? WorkingHours { get; set; }

    }
}
