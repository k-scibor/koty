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
            kupno0.Text = ""; kupno1.Text = ""; kupno2.Text="";
            sprzedaz0.Text = ""; sprzedaz1.Text = ""; sprzedaz2.Text="";

            Currency c = new Currency();
            double kursZD, kursSD;
            string[] waluta = { waluta0.Text, waluta1.Text, waluta2.Text };
            Label[] kupno = { kupno0, kupno1, kupno2 };
            Label[] sprzedaz = { sprzedaz0, sprzedaz1, sprzedaz2 };
            int i = 0;

            foreach (string w in waluta) {
                if (w != "")
                {
                    c = deserializujJson(pobierzKurs(w, dataPick.Date));
                    kursZD = (double)c.rates[0].bid;
                    kursSD = (double)c.rates[0].ask;
                    kupno[i].Text = $"Kurs kupna {w} w dniu {dataPick.Date.ToString("yyyy-MM-dd")}: {kursZD} zł";
                    sprzedaz[i].Text = $"Kurs sprzedaży {w} w dniu {dataPick.Date.ToString("yyyy-MM-dd")}: {kursSD} zł";
                    i++;
                }
                else
                    break;
            }
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
