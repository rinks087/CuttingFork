
using Android.Content;
using AndroidX.Lifecycle;
using ITGBrands.CSFT.Models;
using ITGBrands.CSFT.Services;
using ITGBrands.CSFT.ViewModels;
using System.Windows.Input;



namespace ITGBrands.CSFT
{
    public partial class MainPage : ContentPage
    {
        private int _dumpCount;
        private bool _isDumpCountEnabled;
        private ReaderModel rfidModel;
        private ZebraRFIDService _zebrarfid;

        public MainPage(bool useSimulator, SaratogaViewModel viewModel)
        {
            InitializeComponent();
            rfidModel = ReaderModel.readerModel;
            BindingContext = viewModel;

            // Populate the Picker with values directly
            Tagpicker.Items.Add("");
           Tagpicker.Items.Add("00010166011703B6180B0C09311B00FA0001002200940000000000000000000000000000000000000000000000000000000000000000000000");
            Tagpicker.Items.Add("01029A000003B61807140A0A0A03B6000000220000000000000000000000000000000000000000");
            Tagpicker.Items.Add("0103E90014019B0707191800000000000000000000000000000000000000000000000000000000");
            Tagpicker.Items.Add("01029A001400000707140A0A0A01B90002001F0000000000000000000000000000000000000000");
            Tagpicker.Items.Add("010127001400000707140A0A0A01B9000200200000000000000000000000000000000000000001");
            Tagpicker.Items.Add("0101270000000000000000000001B9000200200000000000000000000000000000000000000001");
            Tagpicker.Items.Add("0100000000000000000000000001B9000200200000000000000000000000000000000000000001");
            Tagpicker.Items.Add("0101000000000000000000000001B9000200200000000000000000000000000000000000000001");


            // Optionally, set a default selected index
            Tagpicker.SelectedIndex = 0;

            // Event handler for selection change
            Tagpicker.SelectedIndexChanged += TagPicker_SelectedIndexChanged;

            Dumperpicker.Items.Add("");
            Dumperpicker.Items.Add("020030003200");

            Dumperpicker.SelectedIndexChanged += DumperPicker_SelectedIndexChanged;

            // Initialize variables
            _dumpCount = 0; // Assuming initial value is 0
            _isDumpCountEnabled = true; // Assuming dump count is enabled by default

            //commented tag & dump picker
            Tagpicker.IsVisible = true;
            Dumperpicker.IsVisible = true;

        }

        public ICommand StartScanCommand { get; private set; }
        public ICommand StartDumperScanCommand { get; private set; }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            CurrentLocationEntry.Text = "Cut Storage";
            ContainerIdEntry.Text = "";
            FillDateEntry.Text = "";
            BlendIdEntry.Text = "";
            Intent intent = new Intent();

            if (intent.Action.Equals("com.symbol.datawedge.api.RESULT_ACTION"))
            {
                string barcode = intent.GetStringExtra("com.symbol.datawedge.data_string");
                Console.WriteLine("Scanned Barcode: " + barcode);

                // Handle the scanned barcode data as needed
            }

            //  ((SaratogaViewModel)BindingContext).StartScanCommand.Execute(this);

            //  ((SaratogaViewModel)BindingContext).StartDumperScanCommand.Execute(this);

            // Fetch or generate your hex values here


            var viewModel = BindingContext as SaratogaViewModel;
            //   viewModel.UpdateIn();
         //   base.OnAppearing();
           viewModel.UpdateIn();

            if (viewModel != null)
            {
               
                if (_isDumpCountEnabled)
                {

                    viewModel.GetDumpCountOnload(DateTime.Now);
                }
            }
            else
            {
                 DisplayAlert("Error", "ViewModel is null", "OK");
            }
        }

        private async void onBackIconClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void OnLogOffClicked(object sender, EventArgs e)
        {
            // Perform any necessary log-off operations here, such as clearing user data
            // Navigate to the login page


            // If you're not using Shell, you can use the following for navigation
            await Navigation.PushAsync(new Login());


          
        }

        private async void OninventoryClicked(object sender, EventArgs e)
        {
            var viewModel = BindingContext as SaratogaViewModel;


            viewModel.UpdateIn();


        }

        private void TagPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Tagpicker.SelectedIndex != -1)
            {
                string selectedTag = Tagpicker.Items[Tagpicker.SelectedIndex];
                // Do something with the selected tag
             //   DisplayAlert("Selected Tag", $"You selected: {selectedTag}", "OK");

              // StartScanCommand.Execute(selectedTag);

                ((SaratogaViewModel)BindingContext).StartScanCommand.Execute(selectedTag);
            }
        }

        private void DumperPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Dumperpicker.SelectedIndex != -1)
            {
                string selectedTag = Dumperpicker.Items[Dumperpicker.SelectedIndex];
                // Do something with the selected tag
                //   DisplayAlert("Selected Tag", $"You selected: {selectedTag}", "OK");

                // StartScanCommand.Execute(selectedTag);

                ((SaratogaViewModel)BindingContext).StartScanCommand.Execute(selectedTag);
            }
        }

        private void OnResetDumpCountClicked(object sender, EventArgs e)
        {
            // Reset Dump Count to 0
            //_dumpCount = 0;
            //// Update the Dump Count Entry (assuming the Entry is bound to _dumpCount)
            //OnPropertyChanged(nameof(_dumpCount));
            ReworkEntry.Text = "";
            NonConformanceEntry.Text = "";
            ContainerIdEntry.Text = "";
            FillDateEntry.Text = "";
            WeightEntry.Text = "";
            BlendIdEntry.Text = "";
            BlendDescriptionEntry.Text = "";
            SuggestedLocationEntry.Text = "";
            DumpCountEntry.Text = "0";
            // Change the ToggleDumpCountButton text to "Enable Dump Count"
            ToggleDumpCountButton.Text = "Enable Dump Count";
            _isDumpCountEnabled = false;                   
        }

        private void OnToggleDumpCountClicked(object sender, EventArgs e)
        {
            if (_isDumpCountEnabled)
            {
                // Disable Dump Count
                ToggleDumpCountButton.Text = "Enable Dump Count";
                _isDumpCountEnabled = false;
            }
            else
            {
                // Enable Dump Count
                ToggleDumpCountButton.Text = "Disable Dump Count";
                _isDumpCountEnabled = false;
            }
        }
    }

}
