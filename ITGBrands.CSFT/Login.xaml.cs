using ITGBrands.CSFT.ViewModels;
using AuthenticationService;
using Microsoft.Maui.Controls;
using ITGBrands.CSFT.Services;
using ITGBrands.CSFT.Models;

namespace ITGBrands.CSFT
{


    public partial class Login : ContentPage
    {
        private readonly ISaratogaService _saratogaService;
        private readonly IRFIDService _rfridService;
            private ReaderModel rfidModel;

        private SaratogaViewModel _saratogaViewModel;
        // Stores application name
        public string mstrAppName = "FTKCTNG";

        private UserPrivilegeEx[] apps = null;

        private string[] appsName = null;

       
       

        public Login()
        {
            InitializeComponent();

        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            bool useSimulator = false; // Or false if you want to use the Zebra SDK

            string lstrBc1 = string.Empty;
            string lstrBc = BarcodeEntry.Text + "\r\n\0";
            lstrBc1 = BarcodeEntry.Text.Substring(1, 6);

            //AuthenticationSoapClient authenticationSoapClient = new AuthenticationSoapClient(AuthenticationSoapClient.EndpointConfiguration.AuthenticationSoap);
            //apps = await authenticationSoapClient.GetUserApplicationsPrivilegesExAsync(lstrBc1, "FTKCTNG", "KEYBOARD", lstrBc1, "Cut storage fork truck");

            //appsName = Array.ConvertAll(apps, App => App.Application + "," + App.Role + "," + App.DBUser + "," + App.DBPassword + "," + App.Title);

            //if (apps != null)
            //{
            //    // Navigate to the next page or show success message
            //    await DisplayAlert("Success", "Login successful!", "OK");
            //    // Navigate to the main page

            rfidModel = ReaderModel.readerModel;
            await Navigation.PushAsync(new MainPage(useSimulator, new SaratogaViewModel(useSimulator, lstrBc1, new SaratogaService(), new WindowsRFIDService())));
            //}

            //else
            //{
            //    await DisplayAlert("Error", "Invalid Barcode", "Try Again");
            //}


        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            bool useSimulator = false; // Or false if you want to use the Zebra SDK

            string lstrBc1 = string.Empty;
            string lstrBc = BarcodeEntry.Text + "\r\n\0";
            lstrBc1 = BarcodeEntry.Text.Substring(1, 6);

            Navigation.PushAsync(new MainPage(useSimulator, new SaratogaViewModel(useSimulator, lstrBc1, new SaratogaService(), new WindowsRFIDService())));


        }


    }
}