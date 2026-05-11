using Android.Content;
using ITGBrands.CSFT.Services;
using ITGBrands.CSFT.ViewModels;
namespace ITGBrands.CSFT
{
    public partial class App : Application
    {
        public App(SaratogaViewModel saratogaViewModel)
        {
            InitializeComponent();

         //   MainPage = new MainPage(saratogaViewModel);

           MainPage = new NavigationPage(new Login());

         //   MainPage = new NavigationPage(new MainPage(new SaratogaViewModel(new SaratogaService(), new WindowsRFIDService())));
        }

      

    }

    [BroadcastReceiver(Enabled = true)]
    public class BarcodeReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            string barcode = intent.GetStringExtra("com.symbol.datawedge.data_string");
            Console.WriteLine("Scanned Barcode: " + barcode);
        }
    }
}
