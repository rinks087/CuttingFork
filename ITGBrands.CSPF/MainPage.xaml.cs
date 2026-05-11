using ITGBrands.CSPF.Services;
using ITGBrands.CSPF.ViewModels;

namespace ITGBrands.CSPF
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        bool TestOnEmulator = false;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void MentholFeederButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MentholFeeder(new CSPFViewModel(new DataService(),"Menthol", TestOnEmulator),"Menthol Feeder","fm"));
        }


        private async void NonMentholFeederButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MentholFeeder(new CSPFViewModel(new DataService(), "Non-Menthol", TestOnEmulator), "Non-Menthol Feeder","fr"));
        }
        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            //if (count == 1)
            //    CounterBtn.Text = $"Clicked {count} time";
            //else
            //    CounterBtn.Text = $"Clicked {count} times";

            //SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }

}
