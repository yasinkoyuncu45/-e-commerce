using Newtonsoft.Json;

namespace MarketHup.Models
{
    public class Activite
    {
        [JsonProperty("Tur")]
        public string Tur { get; set; }

        [JsonProperty("Adi")]
        public string Adı { get; set; }

        [JsonProperty("EtkinlikBaslamaTarihi")]
        public DateTime EtkinlikBaşlamaTarihi { get; set; }

        [JsonProperty("EtkinlikBitisTarihi")]
        public DateTime EtkinlikBitişTarihi { get; set; }

        [JsonProperty("EtkinlikMerkezi")]
        public string EtkinlikMerkezi { get; set; }

        [JsonProperty("KisaAciklama")]
        public string KısaAçıklama { get; set; }

        [JsonProperty("BiletSatisLinki")]
        public string BiletSatışLinki { get; set; }

        [JsonProperty("KucukAfis")]
        public string KucukAfiş { get; set; }
    }
}
