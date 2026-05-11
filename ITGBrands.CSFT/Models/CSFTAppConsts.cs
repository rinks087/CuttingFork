using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITGBrands.CSFT.Models
{
    public class CSFTAppConsts
    {

        // Saratoga data indices as constants
        public const short SARATOGA_CONTAINERID_INDEX = 1;
        public const short SARATOGA_TAREWEIGHT_INDEX = 2;
        public const short SARATOGA_BLENDCODE_INDEX = 3;
        public const short SARATOGA_FILLYEAR_INDEX = 4;
        public const short SARATOGA_FILLMONTH_INDEX = 5;
        public const short SARATOGA_FILLDAY_INDEX = 6;
        public const short SARATOGA_FILLHOUR_INDEX = 7;
        public const short SARATOGA_FILLMIN_INDEX = 8;
        public const short SARATOGA_FILLSEC_INDEX = 9;
        public const short SARATOGA_NETWEIGHT_INDEX = 10;
        public const short SARATOGA_FILLSTATIONTOBACCOSOURCE_INDEX = 11;
        public const short SARATOGA_ITEMCODE_INDEX = 12;
        public const short SARATOGA_MOISTURE_INDEX = 13;
        public const short SARATOGA_OFFSPECCODE_INDEX = 14;
        public const short SARATOGA_NONCONFORMANCE_INDEX = 15;

        // Static array to hold all indices
        public static readonly short[] IndexArray = {
        SARATOGA_CONTAINERID_INDEX,
        SARATOGA_TAREWEIGHT_INDEX,
        SARATOGA_BLENDCODE_INDEX,
        SARATOGA_FILLYEAR_INDEX,
        SARATOGA_FILLMONTH_INDEX,
        SARATOGA_FILLDAY_INDEX,
        SARATOGA_FILLHOUR_INDEX,
        SARATOGA_FILLMIN_INDEX,
        SARATOGA_FILLSEC_INDEX,
        SARATOGA_NETWEIGHT_INDEX,
        SARATOGA_FILLSTATIONTOBACCOSOURCE_INDEX,
        SARATOGA_ITEMCODE_INDEX,
        SARATOGA_MOISTURE_INDEX,
        SARATOGA_OFFSPECCODE_INDEX,
        SARATOGA_NONCONFORMANCE_INDEX
    };

        //Tags ids that will be used for this application
        public const short RFIDTAGTYPEINDEX = 0;
        public const short SARATOGATAGID = 1;
        public const short FLOORTAGID = 2;

        //Floor data indices
        public const short FLOORLOCCODEINDEX = 16;
        public const short FLOORSEVERITYINDEX = 17;



        public static readonly short[] FloorArray =
        {
            FLOORLOCCODEINDEX,
            FLOORSEVERITYINDEX


        };

        //Floor Severity values

        public const short NORMALLOC = 0;
        public const short WARNINGLINE = 1;
        public const short DUMPLINE = 2;
        public const string CUTSTORAGE = "Cut Storage";


        public const string WRONG_FEEDER_MSG = "This is the wrong feeder for the tobacco in the saratoga! Check the suggested locations field to determine a proper feeder for the tobacco in the saratoga!";
        public const string WRONG_FEEDER_PROMPT = "Check the suggested locations field to determine a proper feeder for the tobacco in the saratoga!";
        public const string CST_REWORK_ITEM_CODE_MSG = "Rework tobacco should not be brought into Cut Storage!";
        public const string CST_REWORK_OFFSPEC_CODE_MSG = "Rework tobacco (determined from off spec code) should not be brought into Cut Storage!";
        public const string CST_REWORK_MSG = "Rework tobacco should not be brought into Cut Storage!";
        public const string CST_REWORK_Prompt = "Remove the tobacco from cut storage and take to a rework processing area.";
        public const string CST_AGED_INV_MSG = "The tobacco that you are carrying is considered rework because it has been in cut storage too long!";
        public const string CST_AGED_INV_PROMPT = "Remove the tobacco from cut storage and take to a rework processing area.";
        public const string CST_NO_WEIGHT = "The tobacco needs to be reweighed before it can be dumped at this feeder!";
        public const string CST_NO_WEIGHT_PROMPT = "The tobacco should be reweighed at a weigh station before being dumped into this feeder!";
        public const string CST_NO_DATE = "The date on the container is not valid! Do not dump this container and notify your supervisor!";
        public const string CST_NO_DATE_PROMPT = "Do not dump this container and notify your supervisor!";
        public const string CST_NO_BLEND = "The container does not contain a blend code!";
        public const string CST_NO_BLEND_PROMPT = "Do not dump this container and notify your supervisor!";
        public const string CST_SARATOGA_DUMPED = "The saratoga has been dumped into the feeder.";

        public const string TAG_NOT_CONTAINER_TAG_MSG = "Incorrect Tag Type - Contact a Supervisor";


    }
}
