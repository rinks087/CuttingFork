using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITGBrands.CSPF.Models
{
    public class clsCommonConstants
    {
        #region "Constants"

        #region "General"
        public const string TOB_SCREEN_TLOGIN = "Login";     //Login screen
        public const string APPLICATION_EXIT = "APP_EXIT";
        public const string APPLICATION_TB = "APP_TSKB";

        // Starting character of Barcode for login barcode
        public const string BC_STARTING_CHARACTER_LOGIN = "7";

        #endregion

        #region "XML message tag/attribute names"
        public const string XML_ACT_DLY_LOAD = "Dolly_Loaded";
        public const string XML_ACT_DLY_REMV = "Dolly_Remove";
        public const string XML_ACT_DLY_ERROR = "Dolly_Error";
        public const string XML_EL_PO_DELETE = "PO_Delete";
        public const string XML_ELEM_MATL_MVMT = "Matl_Mvment";
        public const string XML_ELEM_DOLLY = "Dolly_Msg";
        public const string XML_ACT_PICK_MOVE = "Picklist_Move";
        public const string XML_ACT_MOVE_TOB = "Tob_Move";

        // Message header tags
        public const string XML_HDR_MSG = "Msg";
        public const string XML_HDR_HDR = "Hdr";
        public const string XML_EL_HDR = "Hdr";
        public const string XML_HDR_MSGTYPE = "MsgTyp";
        public const string XML_MSGTYPE_ERROR = "Error";
        // Constant used in file header of log file
        public const string XML_MSGTYPE_LOG = "Log";
        public const string XML_HDR_VER = "Ver";
        public const string XML_HDR_SOURCE = "Src";
        public const string XML_HDR_DEST = "Dest";
        public const string XML_EL_MSG = "Msg";
        public const string XML_VER_NO = "1_0";
        // Element types  
        public const string XML_ELEM_PICK_ITEM = "Pick_Item";
        public const string XML_ELEM_DOLLY_ITEM = "Dolly_Itm";

        //Version values
        public const string XML_ELEM_MATL_MVMT_VER = "1_0";
        public const string XML_ELEM_DOLLY_VER = "1_0";
        public const string XML_ELEM_PICK_LIST_VER = "1_0";
        public const string XML_ELEM_PICK_ITEM_VER = "1_0";
        public const string XML_ELEM_EXCEPTION_LOG_VER = "1_0";
        public const string XML_ELEM_LOG_FILE_VER = "1_0";
        // Destination values
        public const string XML_ELEM_MATL_MVMT_DEST = "Simatic_IT";
        public const string XML_ELEM_DOLLY_DEST = "Simatic_IT";
        // Action types
        public const string XML_ACT_RECV_TOB = "Tob_Receipt";
        public const string XML_ACT_DLY_ADD = "Dolly_Add";
        public const string XML_ACT_UNWRAP = "UNW";
        public const string XML_ACT_CONDITION = "CND";
        // Attribute tags
        public const string XML_ATTRIB_ACTION = "Act";
        public const string XML_ATTRIB_MATERIAL = "Mtl";
        public const string XML_ATTRIB_ID = "Id";
        public const string XML_ATTRIB_LOCATION = "Loc";
        public const string XML_ATTRIB_QA_STAT = "QA";
        public const string XML_ATTRIB_RSN_CODE = "Rsn";
        public const string XML_ATTRIB_DATE = "Dtm";
        public const string XML_ATTRIB_USER = "Usr";
        public const string XML_ATTRIB_ORDER = "Ordr";
        public const string XML_ATTRIB_QUANTITY = "Qty";
        public const string XML_ATTRIB_DOLLY = "Dly";
        public const string XML_ATTRIB_ITEM = "Itm";
        public const string XML_ATTRIB_ORDER_ITEM = "Ordr_Itm";
        public const string XML_ATTRIB_SOURCE = "Src";
        public const string XML_ATTRIB_BALANCE_CUT = "Blc";
        public const string XML_ATTRIB_ERROR = "Err";
        public const string XML_ATTRIB_WAREHOUSE = "Whs";
        public const string XML_ATTRIB_TYPE = "Typ";
        public const string XML_ATTRIB_REQUIRED = "Req";
        public const string XML_ATTRIB_DUMPER = "Dmp";
        public const string XML_ATTRIB_PALLET_RATIO = "Rat";
        // Value tags.
        public const string XML_VALUE_MATERIAL_TOB = "TB";
        public const string XML_VALUE_MATERIAL_TPP = "PT";
        public const string XML_EL_DOLLY = "Dolly_Msg";
        public const string XML_EL_DOLLYITEM = "Dolly_Itm";

        public const string DESCRIPTION = "Description";
        public const string CODE = "Code";
        public const string PRODUCT_CODE = "Product_ID_Locn";
        public const string ITEM_CODE = "Item_Code";
        public const string SUB_TOB_TYPE = "Sub_Tobacco_Type";

        //For MMHH and Weigh Station application
        public const string XML_TAG_DATA = "Tag_Data";

        public const string XML_TAG_LOGING_VERSION_NUMBER = "1_0";

        public const string XML_TAG_LOG_MSGDEST = "CELOGGER";

        public const string XML_FUNCTION = "Fnc";

        // Added for DCC

        public const string XML_ELEM_DCC = "DCC_Msg";
        public const string XML_ELEM_DCC_VER = "1_0";
        public const string XML_ACT_DCC_LOADED = "DCC_Loaded";
        public const string XML_ACT_DCC_PICKED = "DCC_Picked";
        public const string XML_ACT_DCC_ERROR = "DCC_Error";
        public const string XML_ACT_DCC_REMOVE = "DCC_Remove";
        public const string XML_ELEM_DCC_ITEM = "DCC_Itm";
        public const string XML_ELEM_DCC_EXC = "Exc";
        public const string XML_ELEM_DCC_BLCID = "BlcId";
        public const string XML_DCC_GROUP = "TB_DCC";
        public const string DCC_STAGING_AREA = "DSTG";
        public const string DCC_TASM = "TASM";
        public const string TOBACCCO_STAGING_AREA = "TSTG";
        public const string DCC_LASM = "LASM";


        // Added for DCC


        #endregion "XML message tag/attribute names"


        #region "Handshaking constants"
        // Store handshaking indicator.
        public const string HANDSHAKE = "Y";

        // Store Hardware Handshake value.
        public const string HARDWARE = "RTS/CTS";

        // Store Software Handshake value.
        public const string SOFTWATE = "XON/XOFF";
        #endregion

        #endregion "Constants"

        #region "Properties"
        // Maximum Message will live on the device when it is send to the server. 
        private static int msmq_MAX_TIME_TO_LIVE = 2600000;
        public static int MSMQ_MAX_TIME_TO_LIVE
        {
            get { return msmq_MAX_TIME_TO_LIVE; }
            set { msmq_MAX_TIME_TO_LIVE = value; }
        }

        private static int mtobRelayTimeLimit = 50;
        public static int TOB_RELAY_TIMER_LIMIT
        {
            get { return mtobRelayTimeLimit; }
        }

        private static string mAppVersion = string.Empty;

        public static string AppVersion
        {
            get { return clsCommonConstants.mAppVersion; }
            set { clsCommonConstants.mAppVersion = value; }
        }

        //private static string mstrAppName = string.Empty;

        //public static string Application_Name
        //{
        //    get { return mstrAppName; }
        //    set { mstrAppName = value; }
        //}

        #endregion "Properties"

        #region "Constructor"

        private clsCommonConstants()
        {

        }

        #endregion "Constructor"
    }
}
