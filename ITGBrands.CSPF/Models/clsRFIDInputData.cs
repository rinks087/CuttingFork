using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITGBrands.CSPF.Models
{
    public class clsRFIDInputData
    {
        private string mstrText = string.Empty;

        private string mstrValue = string.Empty;

        private string mstrSerialNumber;

        public string SerialNumber
        {
            get { return mstrSerialNumber; }
            set { mstrSerialNumber = value; }
        }



        public string Text
        {
            get { return mstrText; }
            set { mstrText = value; }
        }

        public string Value
        {
            get { return mstrValue; }
            set { mstrValue = value; }
        }

        public clsRFIDInputData(string pstrText, string pstrValue, string pstrSerialNumber)
        {

            mstrText = pstrText;
            mstrValue = pstrValue;
            mstrSerialNumber = pstrSerialNumber;
        }

        public override string ToString()
        {
            return mstrText;
        }

    }


}
