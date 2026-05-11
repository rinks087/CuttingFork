using ITGBrands.CSPF.ViewModels;
using ITGBrands.CSPF.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITGBrands.CSPF.Models;
using ITGBrands.CSFT.ViewModels;
using System.ComponentModel;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Cryptography.X509Certificates;

namespace ITGBrands.CSPF.ViewModels
{
   
    public class CSPFViewModel: BaseViewModel
    {
       
        private clsContainerTag mContainerTag;

         private   clsDataValue resultData;


        public const string MENTHOLFEEDERPURGE = "Mentholated Cut Blend";
        public const string NONMENTHOLFEEDERPURGE = "Non-Mentholated Cut Blend";

        private  ISaratogaService _saratogaService;

 

        private UInt16 mshtItemCode = 0;


        private IRFIDService _rfidService;
       

        public ObservableCollection<clsDataValue> Feederdata { get; set; }

        public ObservableCollection<clsDataValue> NonConformancedata { get; set; }

        public ObservableCollection<clsDataValue> Blendcodedata { get; set; }


        private clsDataValue _selectedNonConformanceItem;
        public clsDataValue SelectedNonConformanceItem
        {
            get => _selectedNonConformanceItem;
            set
            {
                _selectedNonConformanceItem = value;
               
                if (_selectedNonConformanceItem != null)
                {
                    string selectedValue = _selectedNonConformanceItem.Value;
                   
                }
            }
        }


        private string _mstrSerialNumToWrittenTo;

        // Property to store the previously scanned serial number
        public string mstrSerialNumToWrittenTo
        {
            get => _mstrSerialNumToWrittenTo;
           // set => SetProperty(ref _mstrSerialNumToWrittenTo, value);
            set
            {
                SetProperty(ref _mstrSerialNumToWrittenTo, value);
            }
        }

        public string FeederName { get; set; }
        public string BlendCode { get; set; }
        public int ContainerId { get; set; }
        public string Conformance { get; set; }

        public ICommand WriteCommand { get; }

        private readonly IDataService _dataService;
        public ICommand NonMentholFeeder { get; }

        public CSPFViewModel()
        {
            WriteCommand = new Command(async () => await WriteSaratogaTubTagAsync());
        }
        public CSPFViewModel(IDataService dataService, string feederType, bool TestOnEmulator)
        {
            _dataService = dataService;

            if (feederType == "Menthol")
            {
                GetFeederData(_dataService,"fm");
                GetNonConformanceData();
             

            }
            else if (feederType == "Non-Menthol")
            {
                GetFeederData(_dataService, "fr");
                GetNonConformanceData();
            }

            

        }
      

        private async Task GetFeederData(IDataService dataService,string feedertype)
        {
            await _dataService.FetchDataFromApiAsync();


            var feeder = DataCache.CachedData.Locations;

           
            var result = feeder.Where(lc => lc.ProductIdLocn != null &&
                lc.LocationType != null &&
                lc.LocationType.Equals(feedertype, StringComparison.OrdinalIgnoreCase)).ToList();

          
            if (result.Any())
            {
                Feederdata = new ObservableCollection<clsDataValue>();

                foreach (var item in result)
                {
                    Feederdata.Add(new clsDataValue
                    {
                        Text = item.Description,
                        Value = item.SiemensLocn
                    });
                }
            }
        }

        private async Task GetNonConformanceData()
        {
            if (DataCache.CachedData?.BlendCodes != null)
            {
                var nonConformanceData = DataCache.CachedData.BlendCodes;

                var result = nonConformanceData.Where(bc => bc.CodeType == "2425").ToList();

                if (result.Any())
                {
                   
                    NonConformancedata = new ObservableCollection<clsDataValue>();

                 
                    foreach (var item in result)
                    {
                        NonConformancedata.Add(new clsDataValue
                        {
                            Text = item.BlendDescription,
                            Value = item.Code.ToString()
                        });
                    }
                }
            }
        }

        public clsDataValue GetBlendCodeData(string feedertype, string pstrLocation)
        {
                 
            var purgefeeder = DataCache.CachedData.feederPurge;
            string blendCode = string.Empty;
          

                var result = purgefeeder.FirstOrDefault(fp => fp.subTobacco_type == MENTHOLFEEDERPURGE && fp.FeederlocationId == pstrLocation);
            if (result != null) // Ensure result is not null
            {
               
               // Blendcodedata = new ObservableCollection<clsDataValue>
                resultData = new clsDataValue
                {
                    Text = $"{result.BlendCode}-{result.Description}",
                    Value = result.BlendCode.ToString()
                };
            }

            if (feedertype == "fm")
            {
                clsCSPFAppState.MentholItemCode = result.itemcode.ToString();
            }
            else

            if (feedertype == "fr")
            {
                clsCSPFAppState.NonMentholItemCode = result.itemcode.ToString();
            }

            // Return the blend code or an empty string if no match
            return resultData;
        }


        // Method to scan the RFID tag and return the serial number
        public async Task<string> ScanTagAsync()
        {
            try
            {
                // Simulating RFID scan (replace with real RFID service logic)
                string scannedSerialNumber = "3ED4840100000001"; // Example RFID service call
                if (!string.IsNullOrEmpty(scannedSerialNumber))
                {
                    // Return the scanned serial number
                    return scannedSerialNumber;
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                // Handle errors appropriately
                return string.Empty;
            }
        }

        // Method to check if the scanned serial number matches the previous one
        public bool IsSameSerialNumber(string pstrOldSerialNumber, ref string pstrReadSerialNumber,string type)
        {
            clsRFIDInputData lclsRFIDInputData;


            lclsRFIDInputData = new clsRFIDInputData("Tag2-With Timestamp", "01029A001400640707140A0A0A0000000000580000000000000000000000000000000000000000", "4FDB840100000001");


            pstrReadSerialNumber = lclsRFIDInputData.SerialNumber;
            if (type == "Scan")
            {
                pstrReadSerialNumber = "8B90350800000001";
            }
            if (type == "Write")
            {

                pstrReadSerialNumber = "AB0E430200000001";

            }

            bool lblnResult = false;
            lblnResult = (pstrReadSerialNumber == pstrOldSerialNumber);
            return lblnResult;
        }
        public ICommand StartScanCommand { get; }



        public async Task<int> GetRFIDTagAsync(string selectedTag)
        {
            try
            {
               

                _saratogaService = new SaratogaService();
                _rfidService = new RFIDService();

                // Ensure _saratogaService is not null
                if (_saratogaService == null)
                {
                    throw new InvalidOperationException("SaratogaService is not initialized.");
                }

                // Fetch data from the API asynchronously.
                var data = await _saratogaService.FetchDataFromApiAsync();

                // Start the container scan and wait for the response.
                var scanResponses = await _rfidService.StartContainerScan(selectedTag);

                // Check if any Saratoga responses are available.
                if (scanResponses.SaratogaResponses != null && scanResponses.SaratogaResponses.Any())
                {
                    foreach (var scanResponse in scanResponses.SaratogaResponses)
                    {
                        int containerId = scanResponse.ContainerID;

                        // Return the first containerId.
                        return containerId;
                    }
                }

                // If no responses found, return -1 or another default value.
                return -1;
            }
            catch (Exception ex)
            {
                // Handle exceptions (logging, displaying error messages, etc.).
              //  await Application.Current.MainPage.DisplayAlert("Error", "Failed to start scan: " + ex.Message, "OK");

                // Return a default error value.
                return -1;
            }
        }

      

        public async Task WriteSaratogaTubTagAsync()
        {
          
            string METHOD_NAME = nameof(WriteSaratogaTubTagAsync);
            try
            {
              
                #region "Populate Object Of Container Class for Writing"
                mContainerTag = new clsContainerTag();

              // Populate object of container tag with data for writing to the tag
                mContainerTag.BlendCode = Convert.ToUInt16(BlendCode);
                mContainerTag.TagTime = DateTime.Now;
                mContainerTag.FillDay = Convert.ToUInt16(mContainerTag.TagTime.Day);
                mContainerTag.FillHour = Convert.ToUInt16(mContainerTag.TagTime.Hour);
                mContainerTag.FillMin = Convert.ToUInt16(mContainerTag.TagTime.Minute);
                mContainerTag.FillMonth = Convert.ToUInt16(mContainerTag.TagTime.Month);
                mContainerTag.FillSec = Convert.ToUInt16(mContainerTag.TagTime.Second);
                mContainerTag.FillYear = Convert.ToUInt16(mContainerTag.TagTime.Year.ToString().Substring(2, 2));

                // Empty the net weight
                mContainerTag.NetWeight = 0;
                // Empty the FillStation
                mContainerTag.FillStationTobSource = 0;
                // Populate the Item Code
                mContainerTag.ItemCode = mshtItemCode;
                // Empty the Moisture
                mContainerTag.Moisture = 0;
                // Empty the OffSpec Code
                mContainerTag.OffSpecCode = 0;
                // Populate the Conformance Code
                mContainerTag.NonConformance = Convert.ToUInt16(Conformance);
                mContainerTag.TagSerialNumber = "3ED4840100000001";//lclsRFIDInputData.SerialNumber; ;

                #endregion

                #region "Populate TagInfo to send Msmq Message"

                clsTagInfo lTagInfo = new clsTagInfo
                {
                    BlendCode =BlendCode,
                    Conformance = Conformance,
                    ContainerType = "UK", // Replace with constant if needed
                    ContainerID = ContainerId.ToString()
                };

                #endregion

              

                // Check if this is the same tag that was read earlier by comparing the serial number
                string lstrReadSerialNumber = string.Empty;
                if (!IsSameSerialNumber(mstrSerialNumToWrittenTo, ref lstrReadSerialNumber,"Write"))
                {
                    // Perform the writing asynchronously
                    if (await WriteTagAsync(mContainerTag, lTagInfo, FeederName))
                    {
                       
                      ////  await Application.Current.MainPage.DisplayAlert(
                      //      "Success",
                      //      "Tag written successfully!",
                      //      "OK"
                      //  );

                        // Store the serial number of the tag written to
                        clsCSPFAppState.mstrPreSerialNumber = lstrReadSerialNumber;
                        mstrSerialNumToWrittenTo = string.Empty;

                    }
                    else
                    {
                        // Display failure message
                        //await Application.Current.MainPage.DisplayAlert(
                        //    "Error",
                        //    "Failed to write to RFID tag, Please try again.",
                        //    "OK"
                        //);
                    }

                    mContainerTag = null;
                }
                else
                {
                    // Show a message box if the serial number is different
                    //await Application.Current.MainPage.DisplayAlert(
                    //    "Warning",
                    //    "The serial number is different from the one previously read.",
                    //    "OK"
                    //);
                }
            }
           
            catch (Exception pEx)
            {
                // Handle exceptions by logging and showing an alert
                //await Application.Current.MainPage.DisplayAlert(
                //    "Error",
                //    "An error occurred while writing the tag. Please try again.",
                //    "OK"
                //);

              
            }
        }

        public async Task<bool> WriteTagAsync(clsContainerTag pContainerTag, clsTagInfo pTagInfo, string pstrFeeder)
        {
            bool lblnResult = false;
            bool lblnWriteResult = false;
            bool TestOnEmulator = true;
            const string METHOD_NAME = "WriteTag";

            try
            {
                // Log the method entry
               // LogTrace(CLASS_NAME, METHOD_NAME, string.Empty);

                // Call a method to generate byte array to be passed to services (if needed)
                // byte[] larrBtTagData = pContainerTag.GetTagData(0, clsCSPFAppConsts.LENGTH_OF_TAG); // Placeholder for your logic

                if (TestOnEmulator)
                {
                    // Simulate tag writing on the emulator
                    lblnWriteResult = true;
                }
                else
                {
                    // Access RFID device object and tag instance
                    //clsNETRFIDDevice m_RFIDDevice = clsNETRFIDDevice.GetDeviceObject();
                    //clsRFIDTag m_RFIDTag = new clsRFIDTag
                    //{
                    //    ContainerTagData = pContainerTag,
                    //    TagLayoutID = Convert.ToByte(1),
                    //    TagSerialNumber = pContainerTag.TagSerialNumber
                    //};

                    // Call the method to write to the tag (assumed in the Zebra SDK or custom class)
                  //  lblnWriteResult = m_RFIDDevice.WriteRFIDTag(m_RFIDTag);
                }

                if (lblnWriteResult)
                {
                    // If writing succeeds, send the Purge Feeder MSMQ message
                    if (await SendPurgeFeederMessageAsync(pContainerTag, pTagInfo, pstrFeeder))
                    {
                        lblnResult = true;
                    }
                    else
                    {
                        // Display an error message if the MSMQ message fails to send
                       // throw new clsCustomException("CSPFBL_010", CLASS_NAME, METHOD_NAME, null);
                    }
                }
            }
          
            catch (Exception pEx)
            {
                // Show the error message using MAUI's DisplayAlert
               // await Application.Current.MainPage.DisplayAlert("Error", pEx.Message, "OK");

        
            }

            return lblnResult;
        }

        // Helper method for logging (replace with actual implementation)
        private void LogTrace(string className, string methodName, string message)
        {
            // Implement your logging logic here
            Console.WriteLine($"{className}::{methodName} - {message}");
        }

        private async Task<bool> SendPurgeFeederMessageAsync(clsContainerTag pContainerTag, clsTagInfo pTagInfo, string pstrFeeder)
        {
            const string METHOD_NAME = "SendPurgeFeederMessage";
            string lstrCurrentTime = string.Empty;
            string lsrtNCnf = string.Empty;
            string lstrXML = string.Empty;
            clsProcessXML lProXML = null;
            bool lblnReturn = false;
            System.Collections.Specialized.NameValueCollection lnvcAttribs = null;

            try
            {
              

                // Get the current date time.
                lstrCurrentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                // Store the Nonconformance to be used in the message
                lsrtNCnf = Convert.ToInt16(pContainerTag.NonConformance) == 0 ? "N" : "Y";

                // Create the XML message
                lProXML = new clsProcessXML();
                lstrXML = lProXML.CreateXML(clsCSPFAppConsts.XML_PF_EL_MSG, null, string.Empty, string.Empty, 0);

                lnvcAttribs = new System.Collections.Specialized.NameValueCollection();
                lnvcAttribs.Add(clsCommonConstants.XML_HDR_MSGTYPE, clsCSPFAppConsts.XML_PF_EL_MSGTYPE);
                lnvcAttribs.Add(clsCommonConstants.XML_HDR_VER, clsCSPFAppConsts.XML_VER_NO);
             //   lnvcAttribs.Add(clsCommonConstants.XML_HDR_SOURCE, mConfig.DeviceName);
                lnvcAttribs.Add(clsCommonConstants.XML_HDR_DEST, clsCSPFAppConsts.XML_PF_EL_DEST);

                // Create the header of the message
                lstrXML = lProXML.CreateXML(clsCommonConstants.XML_EL_HDR, lnvcAttribs, lstrXML, clsCSPFAppConsts.XML_PF_EL_MSG, 0);

                // Add the attributes for the message body
                lnvcAttribs.Clear();
                lnvcAttribs.Add(clsCSPFAppConsts.XML_PF_EL_CONT, pTagInfo.ContainerID);
                lnvcAttribs.Add(clsCSPFAppConsts.XML_PF_EL_CTYP, pTagInfo.ContainerType);
                lnvcAttribs.Add(clsCSPFAppConsts.XML_PF_EL_FEEDER, pstrFeeder);
                lnvcAttribs.Add(clsCSPFAppConsts.XML_PF_EL_BLEND, pTagInfo.BlendCode);
                lnvcAttribs.Add(clsCSPFAppConsts.XML_PF_EL_NCnf, lsrtNCnf);
                lnvcAttribs.Add(clsCSPFAppConsts.XML_PF_EL_DTM, lstrCurrentTime);
              //  lnvcAttribs.Add(clsCSPFAppConsts.XML_PF_EL_USR, mConfig.UserName);

                // Create the body of the message
                lstrXML = lProXML.CreateXML(clsCSPFAppConsts.XML_PF_EL_MSGTYPE, lnvcAttribs, lstrXML, clsCSPFAppConsts.XML_PF_EL_MSG, 0);
                lblnReturn = true;
                // Send the message (this assumes you're sending the message via some REST API or other service)
                // Replace MSMQ with a suitable message broker or web service.
                //var lmsmq = new CustomMessageService(); // Placeholder for custom service
                //var result = await lmsmq.SendMessageAsync(lstrXML); // Replace with actual message-sending logic

                //if (result == MessageSendResult.Success)
                //{
                //    lblnReturn = true;
                //    // Log the message sent
                //    LogMessage("Sent Message: " + lstrXML);
                //}
            }
            //catch (clsCustomException exCustException)
            //{
            //    throw exCustException;
            //}
            catch (Exception pEx)
            {
                // Handle error with a user-friendly alert
               // await Application.Current.MainPage.DisplayAlert("Error", pEx.Message, "OK");

               // throw new clsCustomException("CSPFBL_008", CLASS_NAME, METHOD_NAME, pEx);
            }

            return lblnReturn;
        }

        // Helper method for logging
        private void LogMessage(string message)
        {
            // Implement your logging logic here
            Console.WriteLine(message);
        }





        // Implement INotifyPropertyChanged to update UI when Items changes
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

       
    }
}
