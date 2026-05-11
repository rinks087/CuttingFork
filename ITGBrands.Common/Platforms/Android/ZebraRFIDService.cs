
using Com.Zebra.Rfid.Api3;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Com.Zebra.Barcode.Sdk.FirmwareUpdateData;
using Android.Content;
using Android.Nfc;

namespace ITGBrands.Common
{
    // All the code in this file is only included on Android.

   
        public class ZebraRFIDService
        {

            private static Readers readers;
            private static IList<ReaderDevice> availableRFIDReaderList;
            private static ReaderDevice readerDevice;
            private static RFIDReader Reader;
            private EventHandler eventHandler;

            public string Status { get;  set; }


            public void GetAvailableReaders()
            {
                ThreadPool.QueueUserWorkItem(o =>
                {
                    try
                    {
                        if (readers != null && readers.AvailableRFIDReaderList != null)
                        {
                            availableRFIDReaderList = readers.AvailableRFIDReaderList;

                            if (availableRFIDReaderList.Count > 0)
                            {
                                if (Reader == null)
                                {
                                    // get first reader from list
                                    readerDevice = availableRFIDReaderList[0];
                                    Reader = readerDevice.RFIDReader;
                                    // Establish connection to the RFID Reader
                                    Reader.Connect();
                                    if (Reader.IsConnected)
                                    {
                                        Console.Out.WriteLine("Reader connected");
                                        Status = "Reader connected";
                                        ConfigureReader();
                                    }

                                }
                            }
                        }
                    }
                    catch (InvalidUsageException e)
                    {
                        e.PrintStackTrace();
                    }
                    catch (OperationFailureException e)
                    {
                        e.PrintStackTrace();
                        Console.Out.WriteLine("OperationFailureException " + e.VendorMessage);
                        Status = "OperationFailureException " + e.VendorMessage;
                    }
                });
            }



            public void ConfigureReader()
            {
                if (Reader.IsConnected)
                {
                    TriggerInfo triggerInfo = new TriggerInfo();
                    triggerInfo.StartTrigger.TriggerType = START_TRIGGER_TYPE.StartTriggerTypeImmediate;
                    triggerInfo.StopTrigger.TriggerType = STOP_TRIGGER_TYPE.StopTriggerTypeImmediate;
                    try
                    {
                        // receive events from reader
                        if (eventHandler == null)
                        {
                            eventHandler = new EventHandler(Reader);
                        }

                        Reader.Events.AddEventsListener(eventHandler);
                        // HH event
                        Reader.Events.SetHandheldEvent(true);
                        // tag event with tag data
                        Reader.Events.SetTagReadEvent(true);
                        Reader.Events.SetAttachTagDataWithReadEvent(false);
                        // set trigger mode as rfid so scanner beam will not come
                        Reader.Config.SetTriggerMode(ENUM_TRIGGER_MODE.RfidMode, true);
                        // set start and stop triggers
                        Reader.Config.StartTrigger = triggerInfo.StartTrigger;
                        Reader.Config.StopTrigger = triggerInfo.StopTrigger;
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
            }

            public class EventHandler : Java.Lang.Object, IRfidEventsListener
            {
                private RFIDReader _reader;
                public EventHandler(RFIDReader Reader)
                {
                    _reader = Reader;

                }
                // Read Event Notification
                public void EventReadNotify(RfidReadEvents e)
                {
                   // TagData[] myTags = Reader.Actions.GetReadTags(100);

                  // Simulated tag data
                    TagData[] myTags = new TagData[]
                    {
                        new TagData() { TagID = "MockTag123"  },
                        new TagData() { TagID = "MockTag456" }
                    };

                    foreach (var tag in myTags)
                    {
                        Console.WriteLine("Simulated Tag ID: " + tag.TagID);
                    }


                }

                // Status Event Notification
                public void EventStatusNotify(RfidStatusEvents rfidStatusEvents)
                {

                }
            }
        }
    }


