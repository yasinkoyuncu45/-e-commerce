using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketHup.Models.MVVM
{
    public class AboutUs
    {
        //pramiry key, identity yes
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("ID")]//form da label türkçe görünecek 
        public int FormID { get; set; }

        [Required(ErrorMessage = " Zorunlu Alan")]
        [DisplayName("Üst Metin")] //form da görünecek hali
        public string? SuperScript { get; set; }

        [Required(ErrorMessage = " Zorunlu Alan")]
        [DisplayName("Alt Metin")] //form da görünecek hali
        public string? Subtext { get; set; }

        [Required(ErrorMessage = " Zorunlu Alan")]
        [DisplayName("Deneyim Metni")] //form da görünecek hali
        public string? Experiencetext { get; set; }

        [Required(ErrorMessage = " Zorunlu Alan")]
        [DisplayName("Çalışmalarımız Metni")] //form da görünecek hali
        public string? OurWork { get; set; }

        [DisplayName("Resim")]
        public string? PhotoPath { get; set; }


    }
}
