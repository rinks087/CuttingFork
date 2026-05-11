using Android.Bluetooth;
using Android.Nfc;
using Android.Util;
using Android.Widget;
using Com.Zebra.Rfid.Api3;
using Microsoft.Maui.Controls.Compatibility;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using ITGBrands.CSFT.ViewModels;
using static Android.Views.WindowInsetsAnimation;
using System.Windows.Input;
using ITGBrands.CSFT.ViewModels;
using System.ComponentModel;
using Android.SE.Omapi;
using ITGBrands.CSFT.Services;
using Android.AdServices.AdIds;
using static Com.Zebra.Rfid.Api3.TagAccess;
using Java.Lang;
using Xamarin.Google.Crypto.Tink.Prf;
using Android.AdServices.Common;
using Java.IO;


namespace ITGBrands.CSFT.Models
{

    public class ZebraRFIDService : Java.Lang.Object
    {
        private RFIDReader Reader;
        public IList<ReaderDevice> ReadersList = new List<ReaderDevice>();
        public RFIDReader rfidReader;
        private Readers readers;
        ReaderDevice readerDevice;
        public bool isBatchMode = false;
        private bool bluetoothEnabledWithPermission = false;
        //    private EventHandler eventHandler;
        private InventoryListModel _inv;
        //  public static ReaderModel rfidModel = ReaderModel.readerModel;
        private string _connectionStatus, _readerStatus;

        private EventHandler eventHandler;


        public event PropertyChangedEventHandler PropertyChanged;
        public string Status { get; set; }

        public string readerStatus { get => _readerStatus; set { _readerStatus = value; OnPropertyChanged(); } }
        public ICommand StartScanCommand { get; set; }

        internal delegate void TriggerHandler(bool pressed);

        internal event TriggerHandler TriggerEvent;

        public ZebraRFIDService()
        {
            
            //Readers.Attach(this);
            //  MainActivity mainActivity = Platform.CurrentActivity as MainActivity;
            //   mainActivity.CheckPermissions(OnRequestPermissionsResult);
        }

        //private void OnRequestPermissionsResult(bool permissionGranted)
        //{
        //    if (permissionGranted)
        //    {
        //        BluetoothManager bluetoothManager = (BluetoothManager)Android.App.Application.Context.GetSystemService(Android.Content.Context.BluetoothService);
        //        if (!bluetoothManager.Adapter.IsEnabled)
        //        {
        //            MainActivity mainActivity = Platform.CurrentActivity as MainActivity;
        //            mainActivity.CheckBTEnable(OnRequestBTEnable);
        //        }
        //        else
        //        {
        //            Setup();
        //            bluetoothEnabledWithPermission = true;
        //        }
        //    }
        //    else
        //    {
        //        Setup();
        //        bluetoothEnabledWithPermission = false;
        //    }
        //}

        //private void OnRequestBTEnable(bool btEnabled)
        //{
        //    if (btEnabled)
        //    {
        //        Setup();
        //        bluetoothEnabledWithPermission = true;
        //    }
        //    else
        //    {
        //        Setup();
        //        bluetoothEnabledWithPermission = false;
        //    }
        //}


        /// <summary>
        /// Connnect with reader after instance creation
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Setup()
        {
            ThreadPool.QueueUserWorkItem(o =>
            {
                GetAvailableReadersAsync();
                // ConnectReader(0);
            });



            //ThreadPool.QueueUserWorkItem(o =>
            //{
            //    GetAvailableReadersAsync().ContinueWith(_ =>
            //    {
            //        if (rfidReader != null && rfidReader.IsConnected)
            //        {
            //            // Attach the EventHandler to handle read events
            //            rfidReader.Events.AddEventsListener(new ZebraEventHandler(this));
            //            rfidReader.Events.SetTagReadEvent(true);
            //            rfidReader.Events.SetAttachTagDataWithReadEvent(false);

            //            // Optional: ConfigureReader() to start initial scan
            //           ConfigureReader();
            //        }
            //    });
            //});


        }



        public async Task GetAvailableReadersAsync()
        {
            try
            {
                bool serialDeviceNotFound = false;
                ReadersList.Clear();
                // For MC33XX and RFD2000
                try
                {
                    if (readers == null)
                        readers = new Readers(Android.App.Application.Context, ENUM_TRANSPORT.ServiceSerial);
                    ReadersList = readers.AvailableRFIDReaderList;
                }
                catch (System.Exception)
                {
                    serialDeviceNotFound = true;
                    readers.Dispose();
                    readers = null;
                }


                //// Retrieve the available RFID reader list
                ReadersList = readers.AvailableRFIDReaderList;

                if (ReadersList != null && ReadersList.Count > 0)
                {
                    System.Console.Out.WriteLine($"Found {ReadersList.Count} readers.");

                    //if (readers ! == null)
                    //{
                    // Get the first reader from the list
                    readerDevice = ReadersList[0];
                    rfidReader = readerDevice.RFIDReader;

                    // Establish connection to the RFID Reader
                    rfidReader.Connect();
                    if (rfidReader.IsConnected)
                    {
                        System.Console.Out.WriteLine("Reader connected.");
                        Status = "Reader connected";
                        ConfigureReader(); // Configure after connection
                    }
                    else
                    {
                        Status = "Reader not connected.";
                    }
                    //}
                }
                else
                {

                    Status = "No available RFID readers found.";
                }
            }
            catch (InvalidUsageException e)
            {

                Status = $"InvalidUsageException: {e.VendorMessage}";
            }
            catch (OperationFailureException e)
            {

                Status = $"OperationFailureException: {e.VendorMessage}";
            }
            catch (System.Exception ex)
            {

                Status = $"General Exception: {ex.Message}";
            }
        }
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        private void ConfigureReader()

        {

            if (rfidReader.IsConnected)
            {
                //   Reader.Config.SetTriggerMode(ENUM_TRIGGER_MODE.RfidMode, true);

                TriggerInfo triggerInfo = new TriggerInfo();
                triggerInfo.StartTrigger.TriggerType = START_TRIGGER_TYPE.StartTriggerTypeImmediate;
                triggerInfo.StopTrigger.TriggerType = STOP_TRIGGER_TYPE.StopTriggerTypeImmediate;

                rfidReader.Events.SetHandheldEvent(true);
                // Enable RFID tag read events
                rfidReader.Events.SetTagReadEvent(true);
                rfidReader.Events.SetAttachTagDataWithReadEvent(true);

              


                // Set up event listener for RFID read events
                if (eventHandler == null)
                {
                    eventHandler = new EventHandler(rfidReader);
                    rfidReader.Events.AddEventsListener(eventHandler);

                }

                //  Reader.Config.StartTrigger = triggerInfo.StartTrigger; 
                //   Reader.Config.StopTrigger = triggerInfo.StopTrigger;                 // Enable RFID event notifications                Reader.Events.SetTagReadEvent(true);                 Reader.Events.SetAttachTagDataWithReadEvent(true); // Set up event handler eventHandler = new RfidEventHandler(Reader, UpdateStatus); Reader.Events.AddEventsListener(eventHandler);

                UpdateStatus("Reader configured and ready.");

            }
        }

        private void UpdateStatus(string message)
        {
            string msg;


            MainThread.BeginInvokeOnMainThread(() => msg = message);

            //tag.Text = message); 

        }

        public void HHTriggerEvent()
        {
            try
            {
                // perform simple inventory 
                rfidReader.Actions.Inventory.Perform();

                // Sleep or wait
                // Thread.Sleep(5000);

                // stop the inventory 
                rfidReader.Actions.Inventory.Stop();

            }
            catch (InvalidUsageException e)
            {
                e.PrintStackTrace();
            }
            catch (OperationFailureException e)
            {
                e.PrintStackTrace();
            }
        }

        public void StartInventory()
        {
            if (rfidReader != null && rfidReader.IsConnected)
            {
                try
                {
                    rfidReader.Actions.Inventory.Perform();
                    System.Console.WriteLine("Inventory scan started.");
                }
                catch (SystemException ex)
                {
                    System.Console.WriteLine($"Error starting inventory scan: {ex.Message}");
                }
            }
        }

        public void StopInventory()
        {
            if (rfidReader != null && rfidReader.IsConnected)
            {
                try
                {
                    rfidReader.Actions.Inventory.Stop();
                    System.Console.WriteLine("Inventory scan stopped.");
                }
                catch (System.Exception ex)
                {
                    System.Console.WriteLine($"Error stopping inventory scan: {ex.Message}");
                }
            }
        }


        // Disconnect the RFID reader
        public void DisconnectReader()
        {
            if (rfidReader != null && rfidReader.IsConnected)
            {
                rfidReader.Disconnect();
                System.Console.WriteLine("Reader disconnected");
                Status = "Reader disconnected";
            }
        }
    }

        public class EventHandler : Java.Lang.Object, IRfidEventsListener
        {
            private readonly RFIDReader _reader;
            private bool hasReadTag = false;
        private SaratogaViewModel _saratogaViewModel =  new SaratogaViewModel(false, "user1", new SaratogaService(), new WindowsRFIDService());
        public EventHandler(RFIDReader reader)
            {
                _reader = reader;
            }
            // Read Event Notification
            public void EventReadNotify(RfidReadEvents e)
            {
                if (!hasReadTag)
                {
                    TagData[] myTags = _reader.Actions.GetReadTags(1); // Limit to 1 tag

                    if (myTags != null && myTags.Length > 0)
                    {
                        // Get and process only the first tag
                        TagData firstTag = myTags[0];
                        Log.Info("ZebraRFIDService", $"Tag read: {firstTag.TagID}");

                        // Set the flag to true and stop the inventory
                        hasReadTag = true;
                        _reader.Actions.Inventory.Stop();
                    ReadMemoryBank(firstTag.TagID);
                }
                }
                else
                {
                    Log.Info("ZebraRFIDService", "Tag already read; ignoring further events.");
                }

            }


            // Status Event Notification
            public void EventStatusNotify(RfidStatusEvents rfidStatusEvents)
            {
            
            }


        // Method to read memory bank of the RFID tag and return tag data to ViewModel
        public void ReadMemoryBank(string tagId, uint accessPassword = 0, int wordCount = 0, int offset = 0)
        {
            try
            {
                // Create tag access instance
                TagAccess tagAccess = new TagAccess();
                

                // Set up read access parameters
                TagAccess.ReadAccessParams readAccessParams = new TagAccess.ReadAccessParams(tagAccess)
                {
                    AccessPassword = accessPassword,    // Set access password
                    Count = wordCount,                  // Number of words to read
                    MemoryBank = MEMORY_BANK.MemoryBankUser,  // User memory bank
                    Offset = offset                     // Start reading from offset
                };

                // Perform read operation
                TagData tagData = _reader.Actions.TagAccess.ReadWait(tagId, readAccessParams, null);

                if (tagData != null)
                {
                    ACCESS_OPERATION_CODE readAccessOperation = tagData.OpCode;

                    // Check if operation was successful
                    if (readAccessOperation != null)
                    {
                        if (tagData.OpStatus != null && tagData.OpStatus != ACCESS_OPERATION_STATUS.AccessSuccess)
                        {
                            string strErr = tagData.OpStatus.ToString().Replace("_", "");
                            System.Console.WriteLine("READ ACCESS OPERATION Failed: " + strErr);
                        }
                        else
                        {
                            if (tagData.OpCode == ACCESS_OPERATION_CODE.AccessOperationRead)
                            {
                                // Successfully read the memory data, now return it to the ViewModel
                                System.Console.WriteLine("READ ACCESS OPERATION Success: " + tagData.MemoryBankData);
                                _saratogaViewModel.TagId = tagData.MemoryBankData;
                                _saratogaViewModel.ScanSaratoga(tagData.MemoryBankData);
                                // Call the ViewModel's callback with the tag data
                                // _updateTagCallback?.Invoke(tagData);  // Update the ViewModel with tag data
                            }
                        }
                    }
                    else
                    {
                        System.Console.WriteLine("ACCESS READ memoryBankData is null");
                    }
                    
                }
                else
                {
                    System.Console.WriteLine("No tag data returned");
                }
                
            }
            catch (OperationFailureException ex)
            {
                System.Console.WriteLine("ACCESS READ ERROR: " + ex.VendorMessage + " " + ex.StatusDescription);
            }
        }
    }


  




}





