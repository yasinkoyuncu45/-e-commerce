using MarketHup.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Xml;
using System.Xml.Linq;

namespace MarketHup.Controllers
{
    public class WebAPIController : Controller
    {
        public IActionResult PharmacyOnDuty()
        {
            string json = new WebClient().DownloadString("https://openapi.izmir.bel.tr/api/ibb/nobetcieczaneler");

            var pharmacy = JsonConvert.DeserializeObject<List<Pharmacy>>(json);

            return View(pharmacy);
        }
        public IActionResult ArtAndCulture()
        {
            string json = new WebClient().DownloadString("https://openapi.izmir.bel.tr/api/ibb/kultursanat/etkinlikler");

            var activite = JsonConvert.DeserializeObject<List<Activite>>(json);

            return View(activite);
        }
        public IActionResult ExchangeRate()
        {
            // TCMB'den XML verisini çekeceğimiz URL
            string url = "http://www.tcmb.gov.tr/kurlar/today.xml";

            // XmlDocument nesnesi oluşturuluyor
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(url); // URL'den XML verisi yükleniyor

            // Dolar Alış kurunu XPath ile seçme
            // [@Kod='USD'] ifadesi, Kodu USD olan para birimini bulur.
            string dolaralis = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='USD']/BanknoteBuying").InnerXml;
            // Kuru ViewBag'e atama (ilk 5 karakteri alarak)
            ViewBag.dolaralis = dolaralis.Substring(0, 5);

            // Dolar Satış kurunu XPath ile seçme
            string dolarsatis = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='USD']/BanknoteSelling").InnerXml;
            // Kuru ViewBag'e atama (ilk 5 karakteri alarak)
            ViewBag.dolarsatis = dolarsatis.Substring(0, 5);

            string euroalis = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='EUR']/BanknoteBuying").InnerXml;
            ViewBag.euroalis = euroalis.Substring(0, 5);

            string eurosatis = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='EUR']/BanknoteSelling").InnerXml;
            ViewBag.eurosatis = eurosatis.Substring(0, 5);

            // View'ı (görünümü) döndürme
            return View();
        }
        public IActionResult WeatherForecast()
        {
            // OpenWeatherMap API anahtarınız
            string apikey = "52b72dad903d5a0244a91d029fce3686";
            // Hava durumu bilgisini almak istediğiniz şehir
            string city = "İzmir";

            // API isteği için URL oluşturuluyor.
            // 'q' şehir adı için, 'mode=xml' XML formatında veri için,
            // 'lang=tr' Türkçe açıklamalar için (eğer mevcutsa),
            // 'units=metric' sıcaklığı Celsius olarak almak için,
            // 'appid' ise API anahtarınızdır.
            string url = "https://api.openweathermap.org/data/2.5/weather?q=" + city +
                         "&mode=xml&lang=tr&units=metric&appid=" + apikey;

            // XML verisini yüklemek için XDocument nesnesi kullanılıyor.
            // XmlDocument yerine LINQ to XML için XDocument tercih edilir.
            // Eğer XmlDocument kullanmanız gerekiyorsa:
            // var xmlDoc = new XmlDocument();
            // xmlDoc.Load(url);
            XDocument weather = XDocument.Load(url);

            // XML'den sıcaklık değerini çekme.
            // Descendants("temperature") ile tüm "temperature" elemanları bulunur.
            // ElementAt(0) ilkini seçer (genellikle sadece bir tane olur).
            // Attribute("value") "value" niteliğini alır ve Value ile değeri çekilir.
            ViewBag.temperature = weather.Descendants("temperature").ElementAt(0).Attribute("value").Value;
            ViewBag.weather = weather.Descendants("weather").ElementAt(0).Attribute("value").Value;
            string icon= weather.Descendants("weather").ElementAt(0).Attribute("icon").Value;
            ViewBag.iconurl = "https://openweathermap.org/img/wn/" + icon + ".png";
            ViewBag.city=city;
            // Görünümü (View) döndürme
            return View();
        }

    }
}
