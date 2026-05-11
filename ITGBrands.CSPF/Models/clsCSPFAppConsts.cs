using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITGBrands.CSPF.Models
{
    class clsCSPFAppConsts
    {
        #region "General"

        // The arraylist containg RFID Data where 0th element correspond to the type of tag
        public const int POSITION_TAG_TYPE = 0;

        // ID of Saratoga tag
        public const string SARATOGA_TAG_ID = "1";

        // Index of Container ID element in the arraylist containg RFID Data     
        public const int SARATOGA_CONTAINERID_INDEX = 1;

        // The default length of the tag.
        public const int LENGHT_OF_TAG = 48;

        // Non menthol feeder type
        public const string NONMENTHOLFEEDERS = "FR";

        // Menthol feeder type
        public const string MENTHOLFEEDERS = "FM";

        //Application Name
        public static string APPLICATION_NAME = "CSPF";

        //Application ID
        public const string APPLICATION_ID = "CST_PURG_HH";

        #endregion "General"

        #region "MSMQ"

        // XML Message Element
        public const string XML_PF_EL_MSG = "Msg";

        // XML Message Type Element
        public const string XML_PF_EL_MSGTYPE = "Purge_Fdr";

        // XML Message CTyp Element
        public const string XML_PF_EL_CTYP = "CTyp";

        // XML Message Destination value
        public const string XML_PF_EL_DEST = "MES_Sim_IT";

        // XML Message Fdr Element
        public const string XML_PF_EL_FEEDER = "Fdr";

        // XML Message Cont Element
        public const string XML_PF_EL_CONT = "Cont";



        // XML Message NCnf Element
        public const string XML_PF_EL_NCnf = "NCnf";

        // XML Message Dtm Element
        public const string XML_PF_EL_DTM = "Dtm";

        // XML Message Usr Element
        public const string XML_PF_EL_USR = "Usr";

        // XML Message Bld Element
        public const string XML_PF_EL_BLEND = "Bld";

        // XML Message Ver value
        public const string XML_VER_NO = "1.0";

        #endregion  

    }
}
