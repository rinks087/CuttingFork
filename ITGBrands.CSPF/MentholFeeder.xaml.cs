using ITGBrands.CSPF.ViewModels;
using ITGBrands.CSPF.Models;

namespace ITGBrands.CSPF;

public partial class MentholFeeder : ContentPage
{

    private readonly CSPFViewModel _viewModel;

   public string lstrReadSerialNumber = string.Empty;

    private string blenddescription;
    private string blendCodeValue;
    public MentholFeeder(CSPFViewModel viewModel,string labeltext,string feedertype)
	{
		InitializeComponent();
        BindingContext = viewModel;
        function.Text = labeltext;
        FeederType.Text = feedertype;
        _viewModel = viewModel;
    }

    private async void BackButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }


    // ScanTag button functionality
    private async void ScanTag_Clicked(object sender, EventArgs e)
    {
        try
        {
            var selectedFeeder = cmbfeeder.SelectedItem as clsDataValue;
            var selectedConformance = cmbconformance.SelectedItem as clsDataValue;
          

            string feederName = selectedFeeder?.Value ?? string.Empty;
           
            string conformance = selectedConformance?.Value ?? string.Empty;

            var blendcode = _viewModel.GetBlendCodeData(FeederType.Text, feederName);


            

            if (blendcode != null)
            {
                 blenddescription = blendcode.Text;   // "1234-Description"
                 blendCodeValue = blendcode.Value; // "1234"
            }

            if (string.IsNullOrEmpty(feederName) || string.IsNullOrEmpty(conformance))
            {
                await DisplayAlert("Error", "Please select a feeder and conformance.", "OK");
                return;
            }

            // Create instances of clsRFIDInputData for each tag and add them to the combo box
            clsRFIDInputData lclsRFIDInputData;

            lclsRFIDInputData = new clsRFIDInputData("Tag1-No Timestamp", "01029A001400C800000000000000000000005A0000000000000000000000000000000000000000", "3ED4840100000001");
            lclsRFIDInputData = new clsRFIDInputData("Tag2-With Timestamp", "01029A001400640707140A0A0A0000000000580000000000000000000000000000000000000000", "4FDB840100000001");
            lclsRFIDInputData = new clsRFIDInputData("Tag3-With Timestamp", "0103E90014019B0707191800000000000000000000000000000000000000000000000000000000", "8B90350800000001");
          
            lclsRFIDInputData = new clsRFIDInputData("Menthol Tag4-With Timestamp", "01029A001400000707140A0A0A01B90002001F0000000000000000000000000000000000000000", "3ED4840100000001");
         

            lclsRFIDInputData = new clsRFIDInputData("NonMenthol Tag5-With Timestamp", "010127001400000707140A0A0A01B9000200200000000000000000000000000000000000000001", "8B90350800000001");
         
            lclsRFIDInputData = new clsRFIDInputData("NonMenthol Tag6-Without Timestamp", "0101270000000000000000000001B9000200200000000000000000000000000000000000000001", "AB0E430200000001");
        

            lclsRFIDInputData = new clsRFIDInputData("No Container Id 7", "0100000000000000000000000001B9000200200000000000000000000000000000000000000001", "C188350800000001");
         

            lclsRFIDInputData = new clsRFIDInputData("No Container Id 8", "0101000000000000000000000001B9000200200000000000000000000000000000000000000001", "39E4680200000001");

            int containerId = await _viewModel.GetRFIDTagAsync(lclsRFIDInputData.Value); // Assuming this method gets RFID tag details
                                                                                         // Scan the tag and get the serial number

           
 

            string lstrPreSerialNumber = clsCSPFAppState.mstrPreSerialNumber;
            bool lblnSerialNumCheck = false;
            // string variable to hold the serial number read
          
            lblnSerialNumCheck = _viewModel.IsSameSerialNumber(lstrPreSerialNumber, ref lstrReadSerialNumber,"Scan");


            if (!lblnSerialNumCheck)
            {
                //if (TestOnEmulator)
                //{
                //    mRFID = m_RFIDDevice.ReadRFIDTag();
                //    HandleTagData(mRFID);
                //}
                //else
                //{
                //    frmReadContainer lContainerTag = new frmReadContainer(mRFID);
                //    lContainerTag.Show();
                //    lContainerTag.Visible = true;
                //}
                // If the serial number is different from the last written serial number
                // store the read serial number in the member variable.
               
                await Navigation.PushAsync(new ReadContainerPage(function.Text, feederName, conformance, containerId, blenddescription, blendCodeValue, lstrReadSerialNumber));
            }


           
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
        }
    }

    //private string HandleTagDataOnMains(clsRFIDTag pRFIDTag)
    //{
    //    string containerID;

    //    // Check if the tag layout ID is valid (e.g., for container tags)
    //    if (pRFIDTag.TagLayoutID.ToString() == clsCSPFAppConsts.SARATOGA_TAG_ID)
    //    {
    //        if (Convert.ToInt16(pRFIDTag.TagLayoutID) != 0)
    //        {
    //            // Display the container ID
    //            containerID = pRFIDTag.ContainerID.ToString();
             
    //        }
    //        else
    //        {

    //        }
         
    //    }

    //    return pRFIDTag.ContainerID.ToString();
    //}    
    
}