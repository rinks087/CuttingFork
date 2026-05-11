using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITGBrands.CSPF.Models
{
    /// <summary>
    /// This class is used to hold values of Tag in Write operation.
    /// It has collection of attributes of Container and Dolly Tag
    /// </summary>
    public class clsTagInfo
    {
        #region "Data Members"

        // Message Action Read/Write
        private string mstrAction = string.Empty;

        // Tag Serial number 
        private string mstrTagSerialNumber = string.Empty;

        // Tag Container Type 
        private string mstrContainerType = string.Empty;

        // Tag Container ID
        private string mstrContainerID = string.Empty;

        // Tag Tare Weight
        private string mstrTareWeight = string.Empty;

        // Moisture of the Container Tag
        private string mstrMoisture = string.Empty;

        // Net Weight of the Container Tag
        private string mstrNetWeight = string.Empty;

        // Item Code of the Container Tag
        private string mstrItemCode = string.Empty;

        // Blend Code of the Container & Dolly Tag
        private string mstrBlendCode = string.Empty;

        // Fill Station of the Container Tag
        private string mstrFillStation = string.Empty;

        // Fill Date of the Container Tag
        private string mstrFillDate = string.Empty;

        // Off-spec Code of the Container Tag
        private string mstrOffSpec = string.Empty;

        // Conformance of the Container Tag
        private string mstrConformance = string.Empty;

        // PO of the Dolly Tag
        private string mstrPO = string.Empty;

        // Destination of the Dolly Tag
        private string mstrDestination = string.Empty;

        // HeadID1 of the Dolly Tag
        private string mstrHead1 = string.Empty;

        // Weight1 of the Dolly Tag
        private string mstrHead1Weight = string.Empty;

        // HeadID2 of the Dolly Tag
        private string mstrHead2 = string.Empty;

        // Weight2 of the Dolly Tag
        private string mstrHead2Weight = string.Empty;

        // HeadID3 of the Dolly Tag
        private string mstrHead3 = string.Empty;

        // Weight3 of the Dolly Tag
        private string mstrHead3Weight = string.Empty;

        // HeadID4 of the Dolly Tag
        private string mstrHead4 = string.Empty;

        // Weight4 of the Dolly Tag
        private string mstrHead4Weight = string.Empty;

        // Severity of the Floor Tag
        private string mstrSeverity = string.Empty;

        // Location code of the Floor Tag
        private string mstrLocationCode = string.Empty;

        // Default Date const
        private const string STR_DEF_DATE = @"00/00/0000 00:00:00";

        // Default for New tag
        private const string STR_NEW_TAG_UK_TYPE = @"UK";


        /// <summary>
        /// Gets/Sets mstrAction
        /// </summary>
        public string Action
        {
            get { return mstrAction; }
            set { mstrAction = value; }
        }

        /// <summary>
        /// Gets/Sets mstrTagSerialNumber
        /// </summary>
        public string TagSerialNumber
        {
            get { return mstrTagSerialNumber; }
            set { mstrTagSerialNumber = value; }
        }

        /// <summary>
        /// Gets/Sets mstrContainerType
        /// </summary>
        public string ContainerType
        {
            get { return mstrContainerType; }
            set { mstrContainerType = value; }
        }

        /// <summary>
        /// Gets/Sets mstrContainerID
        /// </summary>
        public string ContainerID
        {
            get { return mstrContainerID; }
            set { mstrContainerID = value; }
        }

        /// <summary>
        /// Gets/Sets mstrTareWeight
        /// </summary>
        public string TareWeight
        {
            get { return mstrTareWeight; }
            set { mstrTareWeight = value; }
        }

        /// <summary>
        /// Gets/Sets mstrMoisture
        /// </summary>
        public string Moisture
        {
            get { return mstrMoisture; }
            set { mstrMoisture = value; }
        }

        /// <summary>
        /// Gets/Sets mstrNetWeight
        /// </summary>
        public string NetWeight
        {
            get { return mstrNetWeight; }
            set { mstrNetWeight = value; }
        }

        /// <summary>
        /// Gets/Sets mstrItemCode
        /// </summary>
        public string ItemCode
        {
            get { return mstrItemCode; }
            set { mstrItemCode = value; }
        }

        /// <summary>
        /// Gets/Sets mstrBlendCode
        /// </summary>
        public string BlendCode
        {
            get { return mstrBlendCode; }
            set { mstrBlendCode = value; }
        }

        /// <summary>
        /// Gets/Sets mstrFillStation
        /// </summary>
        public string FillStation
        {
            get { return mstrFillStation; }
            set { mstrFillStation = value; }
        }

        /// <summary>
        /// Gets/Sets mstrFillDate
        /// </summary>
        public string FillDate
        {
            get { return mstrFillDate; }
            set { mstrFillDate = value; }
        }

        /// <summary>
        /// Gets/Sets mstrOffSpec
        /// </summary>
        public string OffSpec
        {
            get { return mstrOffSpec; }
            set { mstrOffSpec = value; }
        }

        /// <summary>
        /// Gets/Sets mstrConformance
        /// </summary>
        public string Conformance
        {
            get { return mstrConformance; }
            set { mstrConformance = value; }
        }

        /// <summary>
        /// Gets/Sets mstrPO
        /// </summary>
        public string PO
        {
            get { return mstrPO; }
            set { mstrPO = value; }
        }

        /// <summary>
        /// Gets/Sets mstrDestination
        /// </summary>
        public string Destination
        {
            get { return mstrDestination; }
            set { mstrDestination = value; }
        }

        /// <summary>
        /// Gets/Sets mstrHead1
        /// </summary>
        public string Head1
        {
            get { return mstrHead1; }
            set { mstrHead1 = value; }
        }

        /// <summary>
        /// Gets/Sets mstrHead1Weight
        /// </summary>
        public string Head1Weight
        {
            get { return mstrHead1Weight; }
            set { mstrHead1Weight = value; }
        }

        /// <summary>
        /// Gets/Sets mstrHead2
        /// </summary>
        public string Head2
        {
            get { return mstrHead2; }
            set { mstrHead2 = value; }
        }

        /// <summary>
        /// Gets/Sets mstrHead2Weight
        /// </summary>
        public string Head2Weight
        {
            get { return mstrHead2Weight; }
            set { mstrHead2Weight = value; }
        }

        /// <summary>
        /// Gets/Sets mstrHead3
        /// </summary>
        public string Head3
        {
            get { return mstrHead3; }
            set { mstrHead3 = value; }
        }

        /// <summary>
        /// Gets/Sets mstrHead3Weight
        /// </summary>
        public string Head3Weight
        {
            get { return mstrHead3Weight; }
            set { mstrHead3Weight = value; }
        }

        /// <summary>
        /// Gets/Sets mstrHead4
        /// </summary>
        public string Head4
        {
            get { return mstrHead4; }
            set { mstrHead4 = value; }
        }

        /// <summary>
        /// Gets/Sets mstrHead4Weight
        /// </summary>
        public string Head4Weight
        {
            get { return mstrHead4Weight; }
            set { mstrHead4Weight = value; }
        }

        /// <summary>
        /// Gets/Sets mstrSeverity
        /// </summary>
        public string Severity
        {
            get { return mstrSeverity; }
            set { mstrSeverity = value; }
        }

        /// <summary>
        /// Gets/Sets mstrLocationCode
        /// </summary>
        public string LocationCode
        {
            get { return mstrLocationCode; }
            set { mstrLocationCode = value; }
        }

        #endregion "Data Members"

        # region "Constants"

        // Constant for Act
        public const string XML_ACTION = "Act";

        // Constant for TagID
        public const string XML_TAGID = "TagID";

        // Constant for CTyp
        public const string XML_CONTTYPE = "CTyp";

        // Constant for Cont
        public const string XML_CONTAINER = "Cont";

        // Constant for Tare
        public const string XML_TAREWT = "Tare";

        // Constant for Mst
        public const string XML_MOISTURE = "Mst";

        // Constant for Qty
        public const string XML_QTY = "Qty";

        // Constant for TTyp
        public const string XML_TOB_TYPE = "TTyp";

        // Constant for Bld
        public const string XML_BLEND = "Bld";

        // Constant for FStn
        public const string XML_FILL_STATION = "FStn";

        // Constant for Fill_Tim
        public const string XML_FILL_TIME = "Fill_Tim";

        // Constant for Spec
        public const string XML_SPEC = "Spec";

        // Constant for NCnf
        public const string XML_NCNF = "NCnf";

        // Constant for PO
        public const string XML_PO = "PO";

        // Constant for DestID
        public const string XML_DESTID = "DestID";

        // Constant for Hd1
        public const string XML_HEAD1 = "Hd1";

        // Constant for Hd1Wt
        public const string XML_HEAD1WT = "Hd1Wt";

        // Constant for Hd2
        public const string XML_HEAD2 = "Hd2";

        // Constant for Hd2Wt
        public const string XML_HEAD2WT = "Hd2Wt";

        // Constant for Hd3
        public const string XML_HEAD3 = "Hd3";

        // Constant for Hd3Wt
        public const string XML_HEAD3WT = "Hd3Wt";

        // Constant for Hd4
        public const string XML_HEAD4 = "Hd4";

        // Constant for Hd4Wt
        public const string XML_HEAD4WT = "Hd4Wt";

        // Constant for Class Name
        private const string CLASS_NAME = "clsTagInfo";

        #endregion "Constants"


        #region "Public Methods" 

        /// <summary>
        /// This method is used to create and return a Name-Value collection 
        /// with Names as constants values of this class and values as 
        /// Properties values of this class 
        /// </summary>
        /// <returns>Name Value collection</returns>
        public NameValueCollection GetTagNameValueCollection()
        {
            string METHOD_NAME = "GetTagNameValueCollection";
            NameValueCollection lnvXmldata = null;
            try
            {

                //  clsCustomLog.Trace(CLASS_NAME, METHOD_NAME, string.Empty);

                lnvXmldata = new NameValueCollection();
                //Add all the required attributes and their values to be used 
                //to create XML message
                lnvXmldata.Add(XML_ACTION, mstrAction);
                lnvXmldata.Add(XML_TAGID, mstrTagSerialNumber);
                lnvXmldata.Add(XML_CONTTYPE, mstrContainerType);
                lnvXmldata.Add(XML_CONTAINER, mstrContainerID);
                lnvXmldata.Add(XML_TAREWT, mstrTareWeight);
                lnvXmldata.Add(XML_MOISTURE, mstrMoisture);
                lnvXmldata.Add(XML_QTY, mstrNetWeight);
                lnvXmldata.Add(XML_TOB_TYPE, mstrItemCode);
                lnvXmldata.Add(XML_BLEND, mstrBlendCode);
                lnvXmldata.Add(XML_FILL_STATION, mstrFillStation);
                lnvXmldata.Add(XML_FILL_TIME, mstrFillDate);
                lnvXmldata.Add(XML_SPEC, mstrOffSpec);
                lnvXmldata.Add(XML_NCNF, mstrConformance);
                lnvXmldata.Add(XML_PO, mstrPO);
                lnvXmldata.Add(XML_DESTID, mstrDestination);
                lnvXmldata.Add(XML_HEAD1, mstrHead1);
                lnvXmldata.Add(XML_HEAD1WT, mstrHead1Weight);
                lnvXmldata.Add(XML_HEAD2, mstrHead2);
                lnvXmldata.Add(XML_HEAD2WT, mstrHead2Weight);
                lnvXmldata.Add(XML_HEAD3, mstrHead3);
                lnvXmldata.Add(XML_HEAD3WT, mstrHead3Weight);
                lnvXmldata.Add(XML_HEAD4, mstrHead4);
                lnvXmldata.Add(XML_HEAD4WT, mstrHead4Weight);
            }
            catch (System.Exception exGeneral)
            {
             //   throw new clsCustomException("TAGINFO_001", CLASS_NAME, METHOD_NAME, exGeneral);
            }
            return lnvXmldata;
        }

        /// <summary>
        /// This method sets default values to the claTagInfo object
        /// This is called when Parse fails in Create Container Tag
        /// All the mandatory elements are updated in this method
        /// </summary>
        public void SetSaratogaDefaultValues()
        {
            string METHOD_NAME = "SetSaratogaDefaultValues";

            // clsCustomLog.Trace(CLASS_NAME, METHOD_NAME, string.Empty);

            this.ContainerID = "New";

            this.TareWeight = "00";

            this.Moisture = "0";

            this.NetWeight = "00";

            this.ItemCode = "New Tag";

            this.BlendCode = "New Tag";

            this.FillStation = "New Tag";

            this.OffSpec = "New Tag";

            this.ContainerType = STR_NEW_TAG_UK_TYPE;

            this.Action = "New";

            this.TagSerialNumber = "New";

            this.FillDate = STR_DEF_DATE;
        }

        /// <summary>
        /// This method sets default values to the claTagInfo object
        /// This is called when Parse fails in Create Dolly Tag
        /// All the mandatory elements are updated in this method
        /// </summary>
        public void SetDollyDefaultValues()
        {
            string METHOD_NAME = "SetDollyDefaultValues";

            //  clsCustomLog.Trace(CLASS_NAME, METHOD_NAME, string.Empty);

            this.ContainerID = "New";

            this.BlendCode = "New Tag";

            this.PO = "New Tag";

            this.Destination = "New Tag";

            this.ContainerType = STR_NEW_TAG_UK_TYPE;

            this.TagSerialNumber = "New";

            this.Action = "New";

            this.FillDate = STR_DEF_DATE;
        }
        #endregion "Public Methods"

    }
}
