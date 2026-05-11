using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITGBrands.CSPF.Models
{
    class clsCSPFAppState
    {

        // Member variable to hold serial number on which the write operation was performed.
        public static string mstrPreSerialNumber = string.Empty;

        // Member variable to hold the serial number which was read in the read operation.
        public static string mstrReadSerialNumber = string.Empty;

        // Member variable to hold the item code
        public static string ItemCode = string.Empty;

        // Member variable to hold the item code
        public static string MentholItemCode = string.Empty;

        // Member variable to hold the item code
        public static string NonMentholItemCode = string.Empty;

    }
}
