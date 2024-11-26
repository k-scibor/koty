using System.Net;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace kodPocztowy
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            this.InitializeComponent();
        }
        private void wyswietl(object sender, EventArgs e)
        {
            string kraj = k.Text;
            string kodLubMiasto = kplm.Text;
            string place = pobierzInfo(kraj, kodLubMiasto);
            output.Text = place;
        }

        private string pobierzInfo(string pKraj,string kodLM)
        {
            string url = "https://api.zippopotam.us/"+pKraj+"/"+kodLM;
            string json;
            using(var webClient = new WebClient())
            {
                json = webClient.DownloadString(url);
            }
            return json;
        }
    }

}
