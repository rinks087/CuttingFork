using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITGBrands.Common.Models;

namespace ITGBrands.Common
{

    public class RFIDService
    {

        public ContainerTags ParseContainerTag(string dataString, string taglayoutID)
        {
            int newfieldlength = 0;
            var taglayoutAPI = DataCache.CachedData.TagLayouts;
            var response = taglayoutAPI.FirstOrDefault(tc => tc.TagLayoutId == taglayoutID);

            var rfidData = new ContainerTags();
            var tagDetailsAPI = DataCache.CachedData.TagDetails;


            // Assuming tagDetailsAPI is a collection (like List<TagDetail> or IEnumerable<TagDetail>)
            var result = tagDetailsAPI.Where(lc => lc.TagLayout == taglayoutID).ToList();

            foreach (var layout in result)
            {

                // Calculate the index for substring extraction (0-based index)
                int startIndex = (layout.StartPosition - 1) * 2; // Convert to 0-based and hex characters
                int length = layout.FieldLength * 2; // Convert bytes to hex characters


                // Ensure that the substring extraction does not go out of bounds
                if (startIndex + length <= dataString.Length)
                {
                    string hexValue = dataString.Substring(startIndex, length);


                    //   var value = dataString.Substring(layout.StartPosition -1 , layout.StartPosition );

                    int intValue;
                    if (layout.FieldType == 0)
                    {
                        // Numeric conversion (decimal)
                        intValue = int.Parse(hexValue, System.Globalization.NumberStyles.HexNumber);
                    }
                    else if (layout.FieldType == 1)
                    {
                        // Hexadecimal to integer conversion
                        intValue = Convert.ToInt32(hexValue, 16);
                    }
                    else
                    {
                        throw new InvalidOperationException($"Unknown FieldType: {layout.FieldType}");
                    }


                    // Assign the parsed value to the correct field based on FieldName
                    switch (layout.FieldName)
                    {
                        case "TagLayoutID":
                            rfidData.TagLayoutID = intValue;
                            break;
                        case "ContainerID":
                            rfidData.ContainerID = intValue;
                            break;
                        case "TareWeight":
                            rfidData.TareWeight = intValue;
                            break;
                        case "BlendCode":
                            rfidData.BlendCode = intValue;
                            break;
                        case "FillYear":
                            rfidData.FillYear = intValue;
                            break;
                        case "FillMonth":
                            rfidData.FillMonth = intValue;
                            break;
                        case "FillDDay":
                            rfidData.FillDay = intValue;
                            break;
                        case "FillHour":
                            rfidData.FillHour = intValue;
                            break;
                        case "FillMin":
                            rfidData.FillMin = intValue;
                            break;
                        case "FillSec":
                            rfidData.FillSec = intValue;
                            break;
                        case "NetWeight":
                            rfidData.NetWeight = intValue;
                            break;
                        case "FillStationTobSource":
                            rfidData.FillStationTobSource = intValue;
                            break;
                        case "Item Code":
                            rfidData.ItemCode = intValue;
                            break;
                        case "Moisture":
                            rfidData.Moisture = intValue;
                            break;
                        case "Off Spec Code":
                            rfidData.OffSpecCode = intValue;
                            break;
                        case "NonConformance":
                            rfidData.NonConformance = intValue;
                            break;
                        default:
                            throw new InvalidOperationException($"Unknown FieldName: {layout.FieldName}");
                    }
                }
                else
                {
                    throw new ArgumentOutOfRangeException("DataString is too short for the given layout.");
                }
            }

            return rfidData;
        }

        public DumperTags ParseDumperTag(string dataString, string taglayoutID)
        {
            var taglayoutAPI = DataCache.CachedData.TagLayouts;
            var response = taglayoutAPI.FirstOrDefault(tc => tc.TagLayoutId == taglayoutID);

            var rfidData = new DumperTags();
            var tagDetailsDumperAPI = DataCache.CachedData.TagDetails;

            var result = tagDetailsDumperAPI.Where(lc => lc.TagLayout == taglayoutID).ToList();

            foreach (var layout in result)
            {
                int startIndex = (layout.StartPosition - 1) * 2; // Convert to 0-based and hex characters
                int length = layout.FieldLength * 2; // Convert bytes to hex characters

                // Ensure that the substring extraction does not go out of bounds
                if (startIndex + length <= dataString.Length)
                {
                    string hexValue = dataString.Substring(startIndex, length);

                    // Convert value based on FieldType (0 for numeric, 1 for string, 2 for hexadecimal)
                    if (layout.FieldType == 0)
                    {
                        // Numeric conversion (decimal)
                        int intValue = int.Parse(hexValue);
                        AssignFieldValue(layout.FieldName, intValue, rfidData);
                    }
                    else if (layout.FieldType == 2)
                    {
                        // Hexadecimal to string conversion
                        string stringValue = HexStringToString(hexValue);
                        AssignFieldValue(layout.FieldName, stringValue, rfidData);
                    }
                    //else if (layout.FieldType == 2)
                    //{
                    //    // Hexadecimal to integer conversion
                    //    int intValue = Convert.ToInt32(hexValue, 16);
                    //    AssignFieldValue(layout.FieldName, intValue, rfidData);
                    //}
                    else
                    {
                        throw new InvalidOperationException($"Unknown FieldType: {layout.FieldType}");
                    }
                }
                else
                {
                    throw new ArgumentOutOfRangeException("DataString is too short for the given layout.");
                }
            }

            return rfidData;
        }

        private DateTime ParseFillDate(string fillDateHex)
        {
            // Example: Convert hex date to DateTime. You can modify this as needed.
            int daysFromEpoch = Convert.ToInt32(fillDateHex, 16);
            DateTime epoch = new DateTime(2000, 1, 1);
            return epoch.AddDays(daysFromEpoch);

        }

         public static string HexStringToString(string hexString)
        {
            int i = 0;
            // hexString = hexString.Replace(" ", "").Replace("-", "").ToUpper();
            byte[] byteArray = HexStringToByteArray(hexString, out i);

            byteArray = Array.FindAll(byteArray, b => b >= 32 && b <= 126);

            return System.Text.Encoding.UTF8.GetString(byteArray);





        }
     

        public static byte[] HexStringToByteArray(string hexString, out int pintDiscarded)
        {
            byte[] lbtBytes = null;
            pintDiscarded = 0;

            //const string METHOD_NAME = "GetBytes";
            try
            {

                // If odd number of characters, discard last character
                if (hexString.Length % 2 != 0)
                {
                    pintDiscarded++;
                    hexString = hexString.Substring(0, hexString.Length - 1);
                }

                //Make a array to hold the bytes of lenght as half the number of characters present
                int lintByteLength = hexString.Length / 2;
                lbtBytes = new byte[lintByteLength];
                string lstrHex;
                int intJ = 0;
                //Take two characters at a time and convert them into hex string
                for (int i = 0; i < lbtBytes.Length; i++)
                {
                    lstrHex = new String(new Char[] { hexString[intJ], hexString[intJ + 1] });
                    lbtBytes[i] = byte.Parse(lstrHex, System.Globalization.NumberStyles.HexNumber);
                    intJ = intJ + 2;
                }
            }
            catch (System.Exception exGeneral)
            {

                // MessageBox.Show(exGeneral.Message + exGeneral.StackTrace);
                // This is for inner classes
                //throw new clsCustomException("EMS_002", CLASS_NAME, METHOD_NAME, exGeneral);
            }
            return lbtBytes;
        }


        private void AssignFieldValue(string fieldName, object value, DumperTags rfidData)
        {
            // Assign the parsed value to the correct field based on FieldName
            switch (fieldName)
            {
                case "TagLayoutID":
                    rfidData.TagLayoutID = Convert.ToInt32(value);
                    break;
                case "Severity":
                    rfidData.Severity = Convert.ToInt32(value);
                    break;
                case "Location Code":
                    rfidData.LocationCode = value.ToString();
                    break;
                default:
                    throw new InvalidOperationException($"Unknown FieldName: {fieldName}");
            }
        }


    }
}
