using System.Net;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace KursyWalut
{
    public class Currency
    {
        public string? table { get; set; }
        public string? currency { get; set; }
        public string? code { get; set; }
        public IList<Rate> rates { get; set; }
    }
    public class Rate
    {
        public string? no { get; set; }
        public string? effectiveDate { get; set; }
        public double? bid { get; set; }
        public double? ask { get; set; }
    }
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            this.InitializeComponent();
        }
        private void wyswietl(object sender, EventArgs e)
        {
            Currency c = new Currency();
            double kursZD, kursSD;
            DateTime data = DateTime.Today;
            string waluta = picker.SelectedItem.ToString();
            c = deserializujJson(pobierzKurs(waluta, data));
            kursZD = (double)c.rates[0].bid;
            kursSD = (double)c.rates[0].ask;
            kupno.Text = $"Kurs kupna {waluta} w dniu {data.ToString("yyyy-MM-dd")}: {kursZD} zł";
            sprzedaz.Text = $"Kurs sprzedaży {waluta} w dniu {data.ToString("yyyy-MM-dd")}: {kursSD} zł";
                    
        }
        private Currency deserializujJson(string json)
        {
        return JsonSerializer.Deserialize<Currency>(json);
        }
        private string pobierzKurs(string waluta, DateTime data)
        {
        string d = data.ToString("yyyy-MM-dd");
        string url = "https://api.nbp.pl/api/exchangerates/rates/c/"+waluta+"/?format=json";
        string json;
        using(var webClient = new WebClient())
        {
        json = webClient.DownloadString(url);
        }
        return json;
        }
    }

}
