using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using ITGBrands.CSFT.Services;
using ITGBrands.CSFT.Models;
using System.Net.Mail;
using System.Net;
using MIIService;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using Microsoft.Maui.ApplicationModel;
using System.Diagnostics;
using Com.Zebra.Rfid.Api3;
using Android.Nfc;
using static Com.Zebra.Rfid.Api3.TagAccess;
using Java.Lang;




namespace ITGBrands.CSFT.ViewModels
{
    public class SaratogaViewModel : BaseViewModel
    {
        private readonly ISaratogaService _saratogaService;
#if ANDROID
        private readonly ZebraRFIDService _zebrasdk;
#endif
        private readonly IRFIDService _rfidService;
        public RFIDReader rfidReader;
        private Color _weightBorderColor;

        private ReaderModel rfidModel;

        private Color _blendcodeBordercolor;

        private Color _containerBorderColor;
        private Color _filldateBorderColor;

        private string _blendDescription;
        public string BlendDescription
        {
            get => _blendDescription;

            set
            {
                SetProperty(ref _blendDescription, value);
            }
        }

        private string _suggestedLocation;

        public string Username { get; set; }
        public string SuggestedLocation
        {
            get => _suggestedLocation;


            set
            {
                SetProperty(ref _suggestedLocation, value);
            }
        }

        // Property for Border Color (UI feedback)
        public Color WeightBorderColor
        {
            get => _weightBorderColor;
          
            set
            {
                SetProperty(ref _weightBorderColor, value);
            }
        }

        // property for  blend code border color

        public Color BlendBorderColor
        {
            get => _blendcodeBordercolor;

            set
            {
                SetProperty(ref _blendcodeBordercolor, value);
            }
        }

        public Color ContainerBorderColor
        {
            get => _containerBorderColor;

            set
            {
                SetProperty(ref _containerBorderColor, value);
            }
        }

        public Color FillDateBorderColor
        {
            get => _filldateBorderColor;

            set
            {
                SetProperty(ref _filldateBorderColor, value);
            }
        }


        private string _dumperlocationcode;
        public string DumperLocationCode
        {
            get => _dumperlocationcode;

            set
            {
                SetProperty(ref _dumperlocationcode, value);
            }
        }


        private string _dumperlocationdesc;
        public string DumperLocationDesc
        {
            get => _dumperlocationdesc;

            set
            {
                SetProperty(ref _dumperlocationdesc, value);
            }
        }

        private int _ContainerId;
        public int ContainerId
        {
            get => _ContainerId;

            set
            {
                SetProperty(ref _ContainerId, value);
            }

        }

        private string _TagId;
        public string TagId
        {
            get => _TagId;

            set
            {
                SetProperty(ref _TagId, value);

            }
         }

        private DateTime _FillDate;
        public DateTime FillDate
        {
            get => _FillDate;

            set
            {
                
                    SetProperty(ref _FillDate, value);
               
            }

        }

        private int _TotalDays;
        public int TotalDays
        {
            get => _TotalDays;

            set
            {

                SetProperty(ref _TotalDays, value);

            }

        }

        private string _Weight;
        public string Weight
        {
            get => _Weight;

            set
            {
                SetProperty(ref _Weight, value);
            }

        }

        private int _BlendCode;
        public int BlendCode
        {
            get => _BlendCode;

            set
            {
                SetProperty(ref _BlendCode, value);
            }

        }

        private int _DumpCount;

        public int DumpCount
        {

            get => _DumpCount;

            set
            {
                SetProperty(ref _DumpCount, value);
            }


        }


       private string _selectedTagValue;

        public string SelectedTagValue
        {
            get => _selectedTagValue;


            set
            {
                SetProperty(ref _selectedTagValue, value);
            }
        }


        private string _selectedTag;
        public string SelectedTag
        {
            get => _selectedTag;
            set
            {
                if (_selectedTag != value)
                {
                    _selectedTag = value;
                    OnPropertyChanged();
                    // Execute command or handle selection logic
                    if (!string.IsNullOrEmpty(_selectedTag))
                    {
                        StartScanCommand.Execute(_selectedTag);
                    }
                }
            }
        }

        private Color _stackLayoutBackgroundColor;

        public Color StackLayoutBackgroundColor
        {
            get => _stackLayoutBackgroundColor;
            set
            {
               
              SetProperty(ref _stackLayoutBackgroundColor, value);
             
            }
        }

        private string _ReworkLabel;
        public string ReworkLabel
        {
            get => _ReworkLabel;

            set
            {
                SetProperty(ref _ReworkLabel, value);
            }

        }

        private string _NonConformanceLabel;
        public string NonConformanceLabel
        {
            get => _NonConformanceLabel;

            set
            {
                SetProperty(ref _NonConformanceLabel, value);
            }

        }

        public List<BlendCode> BlendCodes { get; set; }
        public List<TagLayout> TagLayouts { get; set; }
        public List<TagDetail> TagDetails { get; set; }
        public List<FeederMatrix> FeederMatrices { get; set; }
        public List<Location> Locations { get; set; }

        public ICommand LogOffCommand { get; }

        public ICommand LoadDataCommand { get; }
        public ICommand ResetPageCommand { get; }

        public ObservableCollection<TagItem> Items { get; set; }
        private TagItem _selectedItem;
        public TagItem SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    OnPropertyChanged(nameof(SelectedItem));
                    // Perform action on item selection (optional)
                    if (_selectedItem != null)
                    {
                      //  SelectedText = $"{_selectedItem.TagValue} - {_selectedItem.AdditionalInfo}";


                    }
                }
            }
        }
        private string _selectedText = "Select";
        public string SelectedText
        {
            get => _selectedText;
            set
            {
                if (_selectedText != value)
                {
                  //  _selectedText = value;
                   // _selectedText = $"{_selectedItem.TagValue} - {_selectedItem.AdditionalInfo}";
                  //  OnPropertyChanged(nameof(SelectedText));

                    SetProperty(ref _selectedText, value);
                }
            }
        }


        public SaratogaViewModel() {

            
              StartScanCommand = new Command<string>(async (selectedTag) => await ScanSaratoga(selectedTag));

            // Sample data



        }

        public SaratogaViewModel(bool useSimulator, string username,ISaratogaService saratogaService, IRFIDService rfidService)
        {
                    Items = new ObservableCollection<TagItem>
           {
               //new TagItem { TagValue = "Item 1", AdditionalInfo = "Detail 1" },
               //new TagItem { TagValue = "Item 2", AdditionalInfo = "Detail 2" },
               //new TagItem { TagValue = "Item 3", AdditionalInfo = "Detail 3" }
           };

            _saratogaService = saratogaService;
            Username = username;
            _rfidService = rfidService;

            if (useSimulator)
            {
                StartScanCommand = new Command<string>(async (selectedTag) => await ScanSaratoga(selectedTag));
                Console.WriteLine("Using RFID Simulator");
            }
            else
            {
#if ANDROID

                string Tag;
                string Password = "0";


                   var data =  _saratogaService.FetchDataFromApiAsync();

             //   string selectedTag = "00010166011703B6180B0C09311B00FA0001002200940000000000000000000000000000000000000000000000000000000000000000000000";


           //     var scanResponses =  _rfidService.StartContainerScan(selectedTag);


                // Initialize the Zebra RFID service
                _zebrasdk = new ZebraRFIDService();

              
                // Bind the StartScanCommand to the ScanSaratoga method
             //   StartScanCommand = new Command<string>(async (selectedTag) => await ScanSaratoga(selectedTag));
             

                // Set the command in ZebraRFIDService so it can trigger it
              //  _zebrasdk.StartScanCommand = StartScanCommand;

              //  _zebrasdk.GetAvailableReadersAsync();

               

                Console.WriteLine(_zebrasdk.Status);

              

                // Perform other operations...
                //_zebrasdk.DisconnectReader();

#endif


            }
        }
        public ICommand StartScanCommand { get;  }

       

        public  void HHTriggerEvent(bool pressed)
        {
            if (pressed)
            {
                _zebrasdk.StartInventory();
              
            }
            else
            {
                _zebrasdk.StopInventory();
            }
        }
        public void UpdateIn()
        {
           // _zebrasdk.StartInventory();

            _zebrasdk.TriggerEvent += HHTriggerEvent;

        }

        public async Task ScanSaratoga(string selectedTag)
        {
            try
            {

                string tagid;
                tagid = selectedTag;
                TagId = tagid;
              //  _rfidService.Initialize();
               
                var scanResponses = await _rfidService.StartContainerScan(selectedTag);
                        

                if (scanResponses.SaratogaResponses != null && scanResponses.SaratogaResponses.Any())
                {
                    foreach (var scanResponse in scanResponses.SaratogaResponses)
                    {
                    
                        await ValidateSaratogaContainerAsync(scanResponse);
                    }
                }

                if (scanResponses.DumperResponses != null && scanResponses.DumperResponses.Any())
                {
                    foreach (var dumperResponse in scanResponses.DumperResponses)
                    {
                        // Perform any Dumper-specific validation or processing
                        await ValidateDumperLocation(dumperResponse);
                    }
                }

            }
            catch (System.Exception ex)
            {
                // Handle exceptions (logging, displaying error messages, etc.)
                await Application.Current.MainPage.DisplayAlert("Error", "Failed to start scan: " + ex.Message, "OK");
            }
        }

        public async Task ValidateDumperLocation( DumperTags RfidDumperResponse)
        {

            string locationcode = RfidDumperResponse.LocationCode;
            try
            {
              
                    var location = DataCache.CachedData.Locations;

                    // Find the specific BlendCode
                    //var result = location.FirstOrDefault(lc => lc.ProductIdLocn == locationcode.ToString() &&
                    //            (lc.LocationType == "fr" || lc.LocationType == "ts"));


                var result = location.FirstOrDefault(lc => lc.ProductIdLocn != null &&
            lc.ProductIdLocn == locationcode.ToString() &&
            (lc.LocationType != null &&
             (lc.LocationType.Equals("fr", StringComparison.OrdinalIgnoreCase) ||
              lc.LocationType.Equals("ts", StringComparison.OrdinalIgnoreCase))));


                // Return the locationdescription if found, otherwise return null
                DumperLocationDesc = result?.Description;

                    if (SuggestedLocation == DumperLocationDesc)
                    {
                      //  Application.Current.MainPage.BackgroundColor = Colors.Green;
                           StackLayoutBackgroundColor = Colors.LightGreen;

                    await Application.Current.MainPage.DisplayAlert("Success", CSFTAppConsts.CST_SARATOGA_DUMPED, "OK");

                       
                        // Increment Dump Count if enabled
                        //if (IsDumpCountEnabled)
                        //{
                        //    DumpCount++;
                        //}

                        // Create an example Msg object
                        Msg msg = new Msg
                        {
                            Hdr = new Hdr
                            {
                                MsgTyp = "Tob_Storage",
                                Ver = "1_1",
                                Src = "CSFT_MC3_001",
                                Dest = "SimaticIT"
                            },
                            TobStorage = new TobStorage
                            {
                                Act = "DMP",
                                Whs = "CST",
                                Cont = ContainerId,
                                Typ = "SR",
                                Itm = BlendCode,
                                Loc = DumperLocationDesc,
                                Vld = "Y",
                                Dtm = DateTime.Now, 
                                Usr = Username
                            }
                        };

                        // Send a DUMP message to the Lot Tracking back-end

                        try
                        {
                         
                            var xml = SerializeMsgToXml(msg);

                            DumpCount = DumpCount+1;
                            var client = new MIIService.XacuteWSSoapClient(MIIService.XacuteWSSoapClient.EndpointConfiguration.XacuteWSSoap);
                            string endpoint = client.Endpoint.Address.ToString();
                            // Set up input parameters
                            var inputParams = new MIIService.InputParams
                            {
                                XmlInput = TransformXML(xml, "msmq_message_transformed.xslt") 
                            };

                        
                            var request = new MIIService.XacuteRequest
                            {
                                LoginName = "T_MSMQWebSvc",    
                                LoginPassword = "dc+BphM4R6!xEX5U",     
                                InputParams = inputParams
                            };

                            try
                            {
                                await client.OpenAsync();

                                // Call the service asynchronously
                                var serviceResponse = await client.XacuteAsync(request.LoginName, request.LoginPassword, request.InputParams);

                                
                                // Handle the response
                                if (serviceResponse != null && serviceResponse.Rowset != null && serviceResponse.Rowset.Row != null)
                                {
                                    foreach (var row in serviceResponse.Rowset.Row)
                                    {
                                        Console.WriteLine($"Output: {row.XmlOutput}");
                                        string xmlResponse = row.XmlOutput;

                                        try
                                        {
                                            // Deserialize the XML response
                                            var serializer = new XmlSerializer(typeof(ITGBrands.CSFT.Models.Rowset));
                                            ITGBrands.CSFT.Models.Rowset rowset;

                                            using (var stringReader = new StringReader(xmlResponse))
                                            {
                                                rowset = (ITGBrands.CSFT.Models.Rowset)serializer.Deserialize(stringReader);
                                            }

                                            // Process the deserialized data
                                            if (rowset != null && rowset.Row != null && rowset.Row.Return != null)
                                            {
                                                Console.WriteLine($"Return Code: {rowset.Row.Return.RetCode}");
                                                Console.WriteLine($"Return Description: {rowset.Row.Return.RetDesc}");

                                                // Example check: if Return Code is not 0, handle accordingly
                                                if (rowset.Row.Return.RetCode != "0")
                                                {
                                                    await Application.Current.MainPage.DisplayAlert("Error", $"Failed: {rowset.Row.Return.RetDesc}", "OK");
                                                    return;
                                                }

                                                // Send dump message if the response is successful
                                                await _saratogaService.SendDumpMessage(msg,endpoint, rowset.Row.Return.RetCode, rowset.Row.Return.RetDesc);
                                                await Application.Current.MainPage.DisplayAlert("Success", "Dump message sent successfully", "OK");
                                                 StackLayoutBackgroundColor = Colors.LightGreen;
                                            }
                                            else
                                            {
                                                await Application.Current.MainPage.DisplayAlert("Error", "No data returned from the service.", "OK");
                                            StackLayoutBackgroundColor = Colors.Red;
                                        }
                                        }
                                        catch (System.Exception ex)
                                        {
                                            // Handle any exceptions during deserialization
                                            Console.WriteLine($"Exception: {ex.Message}");
                                            Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                                            await Application.Current.MainPage.DisplayAlert("Error", "An error occurred while processing the response.", "OK");
                                        }
                                    }


                                }

                             


                                else
                                {
                                    Console.WriteLine("No data returned from the service.");
                                StackLayoutBackgroundColor = Colors.Red;
                                }

                               // await _saratogaService.SendDumpMessage(msg);
                               // await Application.Current.MainPage.DisplayAlert("Success", "Dump message sent successfully", "OK");

                            }
                            catch (System.Exception ex)
                            {
                                Console.WriteLine($"An error occurred: {ex.Message}");
                            }
                            finally
                            {
                                await client.CloseAsync();
                            }


                        }
                        catch (System.Exception ex)
                        {
                            // Handle exceptions (logging, displaying error messages, etc.)
                            await Application.Current.MainPage.DisplayAlert("Error", "Failed to load data: " + ex.Message, "OK");
                        }

                        // Wait for 10 seconds
                        // await Task.Delay(10000);

                        // Reset UI
                        ResetUI();
                    }
                    else
                    {
                        // 5.Change screen background color to RED.
                        StackLayoutBackgroundColor = Colors.Red;

                        // Display error message
                        await Application.Current.MainPage.DisplayAlert("Error", CSFTAppConsts.WRONG_FEEDER_MSG, "OK");

                        //Send email notification
                        await SendEmailNotificationAsync("devinder.kalra@itgbrands.com","Wrong Feeder", CSFTAppConsts.WRONG_FEEDER_MSG);

                        //Wait for 10 seconds

                        await Task.Delay(10000);

                        //Reset UI

                        ResetUI();

                    }


                

            }
            catch (System.Exception ex)
            {
                // Handle exceptions (logging, displaying error messages, etc.)
                await Application.Current.MainPage.DisplayAlert("Error", "Failed to load data: " + ex.Message, "OK");
            }
        }


      public  static string TransformXML(string xmlin, string fullFileName)
        {
            string output = string.Empty;
            XPathDocument xpd = new XPathDocument(new StringReader(xmlin));
            XslTransform transform = new XslTransform();
            transform.Load(fullFileName);
            StringWriter sr = new StringWriter();
            transform.Transform(xpd.CreateNavigator(), null, sr);
            output = sr.ToString();
            return output;
        }


        public async Task SendEmailNotificationAsync(string toEmail, string subject, string body)
        {
            try
            {
                var smtpClient = new SmtpClient("smtp.itgbrands.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("devinder.kalra@itgbrands.com", "kFGzAnHgSTj9@LPs"),
                    EnableSsl = true,
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress("devinder.kalra@itgbrands.com"),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true,
                };

                mailMessage.To.Add(toEmail);

                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (System.Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Failed to send email: " + ex.Message, "OK");
            }
        }

        public async Task ValidateSaratogaContainerAsync(SaratogaTags scanResponse)
        {
            try
            {
                // Step 1: Retrieve necessary fields from the RFID scan response
                int containerId = scanResponse.ContainerID;
            //    DateTime? fillDate = scanResponse.FillDate;
                double? weight = scanResponse.NetWeight;
                int blendCode = scanResponse.BlendCode;
                int itemCode = scanResponse.ItemCode;
                int? offSpecCode = scanResponse.OffSpecCode;
                int? nonConformanceIndex = scanResponse.NonConformance;               
                ContainerId = containerId;
                ReworkLabel = "";
                NonConformanceLabel = "";
                //  FillDate = fillDate?.ToString("yyyy-MM-dd") ?? string.Empty;
                var fillDate = CombineFillDate(scanResponse.FillYear, scanResponse.FillMonth, scanResponse.FillDay, scanResponse.FillHour, scanResponse.FillMin, scanResponse.FillSec);
                FillDate = fillDate;              
                Weight = weight?.ToString() ?? string.Empty;
                BlendCode = blendCode;
                DumperLocationDesc = "Cut Storage";

                //To Calulate Total Days below Code
                DateTime currentDate = DateTime.Now;
                TimeSpan difference = currentDate - fillDate;
                TotalDays = difference.Days;


                var blendCodes = DataCache.CachedData.BlendCodes;

                    // Find the specific BlendCode
                    var result = blendCodes.FirstOrDefault(bc => bc.Code == blendCode);

                // Return the BlendDescription if found, otherwise return null
                BlendDescription = result?.BlendDescription;


                var FeederMatrix = DataCache.CachedData.FeederMatrices;

                // Find the specific BlendCode
                var result1 = FeederMatrix.FirstOrDefault(fm => fm.BlendCode == blendCode);

                // Return the BlendDescription if found, otherwise return null
                SuggestedLocation = result1?.SugLocDescription;


                // Step 4: Validation checks
                if (containerId == 0 )
                {
                    ContainerBorderColor = Colors.Red; 
                    await ShowErrorAsync(CSFTAppConsts.TAG_NOT_CONTAINER_TAG_MSG);
                    return;
                }
                else { ContainerBorderColor = Colors.White; }

                if (fillDate == DateTime.MinValue)
                {
                    FillDateBorderColor = Colors.Red;
                    await ShowErrorAsync(CSFTAppConsts.CST_NO_DATE);
                    return;
                }
                else { FillDateBorderColor = Colors.White; }

                if (!weight.HasValue || weight == 0)
                {
                    WeightBorderColor = Colors.Red;
                    await ShowErrorAsync(CSFTAppConsts.CST_NO_WEIGHT + "" + CSFTAppConsts.CST_NO_WEIGHT_PROMPT);
                   


                    return;
                }
                else
                {
                
                    WeightBorderColor = Colors.White;

                    // You can add further logic to process the valid weight
                }

                if (blendCode == 0)
                {
                    BlendBorderColor = Colors.Red;
                    await ShowErrorAsync(CSFTAppConsts.CST_NO_BLEND + "" + CSFTAppConsts.CST_NO_BLEND_PROMPT);
                    BlendDescription = "No Blend";
                    SuggestedLocation = "None";
                    return;
                }
                else { BlendBorderColor = Colors.White; }

                if (itemCode != 33 && itemCode != 34 || offSpecCode > 1)
                {
                    await ShowErrorAsync(CSFTAppConsts.CST_REWORK_MSG + "" + CSFTAppConsts.CST_REWORK_Prompt);
                    Weight = "Rework";
                    SuggestedLocation = "None";
                    return;
                }

                // Step 6: NonConformance Index check
                if (nonConformanceIndex > 0)
                {
                    NonConformanceLabel = "Non Conformance";
                    //NonConformantLabel.IsVisible = true;
                }

                if(itemCode == 33 && TotalDays > 15 || itemCode == 34 && TotalDays > 30)
                {
                    ReworkLabel = "Rework";
                    SuggestedLocation = "None";
                    await ShowErrorAsync(CSFTAppConsts.CST_AGED_INV_MSG + "" + CSFTAppConsts.CST_AGED_INV_PROMPT);
                    return;
                  
                }
                // Step 7: Date validation based on Item Code
                //if ((itemCode == "33" && fillDate.Value.AddDays(15) < DateTime.Today) ||
                //    (itemCode == "34" && fillDate.Value.AddDays(30) < DateTime.Today))
                //{
                //    await ShowErrorAsync(CSFTAppConsts.CST_AGED_INV_MSG + "" + CSFTAppConsts.CST_AGED_INV_PROMPT );
                //    Weight = "Rework";
                //    SuggestedLocation = "None";
                //    return;
                //}
                             

            }
            catch (System.Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Validation failed: " + ex.Message, "OK");
            }
        }

        private async Task ShowErrorAsync(string message)
        {
            // Change the screen background color to GRAY
           // Application.Current.MainPage.BackgroundColor = Color.Gray;

            // Display the error message
            await Application.Current.MainPage.DisplayAlert("Error", message, "OK");

            // Reset the screen after 10 seconds
         //   await Task.Delay(10000);
            //ResetScreen();
        }

        public static DateTime CombineFillDate(int year, int month, int day, int hour, int minute, int second)
        {
            try
            {

                int adjustedYear = year + 2000;
                // Validate and create the DateTime object
                return new DateTime(adjustedYear, month, day, hour, minute, second);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                // Handle invalid date parts (e.g., day > number of days in the month)
                Console.WriteLine($"Error creating date: {ex.Message}");
                return DateTime.MinValue; // Return a default value if invalid
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task GetDumpCountOnload(DateTime recordDate)
        {
            try
            {
                DumpCount = await _saratogaService.GetCount(recordDate);
            }
            catch (System.Exception ex)
            {
                // Handle exceptions
                Console.WriteLine(ex.Message);
            }
        }

        private string SerializeMsgToXml(Msg msg)
        {
            var xmlSerializer = new XmlSerializer(typeof(Msg));

            var settings = new XmlWriterSettings
            {
                Indent = true,
                OmitXmlDeclaration = true // Omit the XML declaration
            };

            using (var stringWriter = new StringWriter())
            using (var xmlWriter = XmlWriter.Create(stringWriter, settings))
            {
                xmlSerializer.Serialize(xmlWriter, msg);
                return stringWriter.ToString();
            }
        }

        private void ResetUI()
        {
            StackLayoutBackgroundColor = Colors.LightGrey;
            ContainerId = 0;
            BlendDescription = "";
            BlendCode = 0 ;
            Weight = "";
            SuggestedLocation = "";
            DumperLocationDesc = "Cut Storage";
            ReworkLabel = "";
            NonConformanceLabel = "";
         //   FillDate = DateTime.MinValue;
            //string IT_Date_String = string.Format("{0:yyyy-MM-dd}");

        }

}
}
