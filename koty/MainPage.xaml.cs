using System.Net;
namespace koty
{
    public partial class MainPage : ContentPage
    {
        public MainPage() {
            this.InitializeComponent();
        }
        private void generuj(object sender, EventArgs e)
        {
            kot.Source = "https://cataas.com/cat";
        }
    }
    
}
