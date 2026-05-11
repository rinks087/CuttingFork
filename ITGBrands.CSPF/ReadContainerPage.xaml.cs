using ITGBrands.CSPF.Models;
using ITGBrands.CSFT.ViewModels;
using ITGBrands.CSPF.ViewModels;

namespace ITGBrands.CSPF;

public partial class ReadContainerPage : ContentPage
{
    public const string MENTHOLFEEDERPURGE = "Mentholated Cut Blend";
    public const string NONMENTHOLFEEDERPURGE = "Non-Mentholated Cut Blend";
    public ReadContainerPage(string feedertype,string feedername,string conformance,int containerid,string blendcodedesc,string blendcodevalue,string serialnumber)
	{
		InitializeComponent();

       FeederNameEntry.Text = feedername;   
       ConformanceEntry.Text = conformance;
        ContainerIdEntry.Text = containerid.ToString();
        BlendDescriptionEntry.Text = blendcodedesc.ToString();

        var viewModel = new CSPFViewModel
        {
            FeederName = feedername,
            Conformance = conformance,
            ContainerId = containerid,
            BlendCode = blendcodevalue,
            mstrSerialNumToWrittenTo = serialnumber

        };

        BindingContext = viewModel; // Set the BindingContext to the existing ViewModel

       


    }

    private void OnBackClicked(object sender, EventArgs e)
    {
        // Navigate back to the previous page
        Navigation.PopAsync();
    }
   
    private void OnWriteClicked(object sender, EventArgs e)
    {
        // Handle the logic for writing data here
        var feederName = FeederNameEntry.Text;
        var blendCode = BlendDescriptionEntry.Text;
        var containerId = ContainerIdEntry.Text;
        var conformance = ConformanceEntry.Text;

        var viewModel = BindingContext as CSPFViewModel;
        viewModel?.WriteCommand.Execute(null);
    }
}