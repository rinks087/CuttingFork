using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ITGBrands.CSPF.Models
{
    public class clsProcessXML
    {
        #region "Constants"

        // Header element 
        public const string XML_EL_HDR_XPATH = @"Msg/Hdr";

        // Header Attribute
        public const string XML_HDR_MSGTYPE = "MsgTyp";

        // Header Attribute
        public const string XML_HDR_VER = "Ver";

        // Header Attribute
        public const string XML_HDR_SOURCE = "Src";

        //  Header Attribute
        public const string XML_HDR_DEST = "Dest";

        public const string XML_EL_MSG = "Msg";

        public const string XML_VER_NO = "1.0";

        // Class Name
        private const string CLASS_NAME = "clsProcessXML";

        # endregion "Constants"

        #region "Public Methods"


        /// <summary>
        /// This method parses the element data with the XPath given as input and files the attributes
        /// to be returned with the respective values.
        /// </summary>
        /// <param name="pstrMessageBody">This string is input XML.</param>
        /// <param name="pstrElementName">XPath of the XML element needing to be parsed.</param>
        /// <param name="phtAttributes">This Hashtable contains attributes as keys and values initialized with “”. 
        /// The values of attributes are filled once element is parsed and used by the caller function.
        /// </param>
        public void ParseData(string pstrMessageBody, string pstrElementName, Hashtable phtAttributes)
        {
            //Method Name
            const string METHOD_NAME = "ParseData";

            XmlDocument lxmlDoc = new XmlDocument();
            XmlNode lxmlNode = null;
            XmlAttributeCollection lxmlAttribs = null;

            try
            {
                //Trace in Debug Mode.
              //  clsCustomLog.Trace(CLASS_NAME, METHOD_NAME, string.Empty);

                //Load the input XML string(pstrElementName) in XML document from which we need to 
                //extract element->attribute information.
                lxmlDoc.LoadXml(pstrMessageBody);

                //Get element pstrElementName from the XML document.
                lxmlNode = lxmlDoc.SelectSingleNode(pstrElementName);

                //If element found in XML document then extract attributes 
                //from the node and fill the attributes which need to be parsed.                
                if (lxmlNode != null)
                {
                    lxmlAttribs = lxmlNode.Attributes;
                    string lstrAttribute;

                    //Loop through each attribute and check whether it exists in phtAttributes.
                    //If that attribute exist then fill its value in phtAttributes.
                    for (int i = 0; i < lxmlAttribs.Count; i++)
                    {
                        lstrAttribute = lxmlAttribs[i].Name.ToString();

                        if (phtAttributes.ContainsKey(lstrAttribute))
                        {
                            phtAttributes[lstrAttribute] = lxmlAttribs[lstrAttribute].Value;
                        }
                    }
                }
            }
            catch (System.Exception exGeneral)
            {
            //    clsCustomLog.WriteEntry(METHOD_NAME + " caught exception = " + exGeneral.Message, clsDeviceConfiguration.GetLogMetadata());
            //    throw new clsCustomException("XML_001", CLASS_NAME, METHOD_NAME, exGeneral);
            }
        }

        /// <summary>
        /// [Currently unused in TOBW application]
        /// This method queries into XMLDocument with the XPath given as input and files the 
        /// attributes to be returned with the respective values.
        /// </summary>
        /// <param name="pxmlDoc">This document object is loaded with required XML file.  </param>
        /// <param name="pstrElementXpath">XPath of the XML element needing to be parsed  </param>
        /// <param name="phtAttributes">This Hashtable contains attributes as keys and values initialized with “”. 
        /// The values of attributes are filled once element is parsed and used by the caller function.
        /// </param>
        public void ParseData(XmlDocument pxmlDoc, string pstrElementXpath, Hashtable phtAttributes)
        {
            //Method Name
            const string METHOD_NAME = "ParseData";
            XmlNode lxmlNode = null;
            XmlAttributeCollection lxmlAttribs = null;
            try
            {
                //Trace in Debug Mode.
               // clsCustomLog.Trace(CLASS_NAME, METHOD_NAME, string.Empty);

                //Query for element to be parsed (pstrElementName) in xDOC. It will return 	XMLNode.
                lxmlNode = pxmlDoc.SelectSingleNode(pstrElementXpath);

                // If element found in XML document then extract attributes 
                // from the node and fill the attributes which need to be parsed.   
                if (lxmlNode != null)
                {
                    lxmlAttribs = lxmlNode.Attributes;

                    // Loop through each attribute and check whether it exists in phtAttributes.
                    // If that attribute exist then fill its value in phtAttributes.
                    for (int i = 0; i < lxmlAttribs.Count; i++)
                    {
                        string lstrAttribute = lxmlAttribs[i].Name.ToString();

                        // If attribute exists in phtAttributes then 
                        // Store value of this attribute in phtAttributes.   
                        if (phtAttributes.ContainsKey(lstrAttribute))
                        {
                            phtAttributes[lstrAttribute] = lxmlAttribs[lstrAttribute].Value;
                        }

                    }
                }
            }
            catch (System.Exception exGeneral)
            {
            //    clsCustomLog.WriteEntry(METHOD_NAME + " caught exception = " + exGeneral.Message, clsDeviceConfiguration.GetLogMetadata());
            //    throw new clsCustomException("XML_001", CLASS_NAME, METHOD_NAME, exGeneral);
           }
        }

        /// <summary>
        /// This method extracts attributes form the input XMLNode object.
        /// </summary>
        /// <param name="pxmlNode">XML node from which user needs to extract attributes.</param>
        /// <param name="phtAttributes">
        /// This Hashtable contains attributes as keys and values initialized with “”. 
        /// The values of attributes are filled once element is parsed and used by the caller function.
        /// </param>
        public void ParseData(XmlNode pxmlNode, Hashtable phtAttributes)
        {
            //Method Name
            const string METHOD_NAME = "ParseData";

            XmlAttributeCollection lxmlAttributeCollection = null;

            try
            {
                //Trace in Debug Mode.
             //   clsCustomLog.Trace(CLASS_NAME, METHOD_NAME, string.Empty);

                if (pxmlNode != null)
                {
                    lxmlAttributeCollection = pxmlNode.Attributes;

                    //Loop through each attribute and check whether it exists in phtAttributes.
                    //If that attribute exist then fill its value in phtAttributes. 
                    for (int i = 0; i < lxmlAttributeCollection.Count; i++)
                    {
                        string lstrAttribute = lxmlAttributeCollection[i].Name.ToString();

                        //If attribute exists in phtAttributes
                        //then Store value of this attribute in phtAttributes. 
                        if (phtAttributes.ContainsKey(lstrAttribute))
                        {
                            phtAttributes[lstrAttribute] = pxmlNode.Attributes[lstrAttribute].Value;
                        }
                    }
                }
            }
            catch (System.Exception exGeneral)
            {
               // clsCustomLog.WriteEntry(METHOD_NAME + " caught exception = " + exGeneral.Message, clsDeviceConfiguration.GetLogMetadata());
               // throw new clsCustomException("XML_001", CLASS_NAME, METHOD_NAME, exGeneral);
            }
        }


        /// <summary>
        /// This method parse the input XML string with the input element and 
        /// returns the required attribute value. 
        /// </summary>
        /// <param name="pstrMessageBody">  This string is input XML. </param>
        /// <param name="pstrElementXpath"> XPath of XML element needing to be parsed.</param>
        /// <param name="pstrAttribute">  Attribute to find values  </param>
        /// <returns>Attribute Value</returns>
        public string ExtractAttribute(string pstrMessageBody, string pstrElementXpath, string pstrAttribute)
        {
            //Method Name
            const string METHOD_NAME = "ExtractAttribute";

            //Create XMLDocument object 
            XmlDocument lxmlDoc = new XmlDocument();
            XmlNode lxmlNode = null;
            XmlAttributeCollection lxmlAttribs = null;
            XmlAttribute lAttrib = null;

            //Declare return String variable attribValue and initialize it with null.
            string lstrAttributeValue = null;

            try
            {
                //Trace in Debug Mode.
              //  clsCustomLog.Trace(CLASS_NAME, METHOD_NAME, string.Empty);

                //Load input XML (pstrMessageBody).
                lxmlDoc.LoadXml(pstrMessageBody);

                //Query for element to be parsed (pstrElementName). It will return XMLNode
                lxmlNode = lxmlDoc.SelectSingleNode(pstrElementXpath);

                if (lxmlNode != null)
                {
                    // Create an attribute collection from the Xml element.
                    lxmlAttribs = lxmlNode.Attributes;

                    if (lxmlAttribs.Count > 0)
                    {
                        lAttrib = lxmlAttribs[pstrAttribute];
                        if (lAttrib != null)
                        {
                            lstrAttributeValue = lAttrib.Value;
                        }
                    }
                }
            }
            catch (System.Exception exGeneral)
            {
             //   clsCustomLog.WriteEntry(METHOD_NAME + " caught exception = " + exGeneral.Message, clsDeviceConfiguration.GetLogMetadata());
               // throw new clsCustomException("XML_002", CLASS_NAME, METHOD_NAME, exGeneral);
            }

            //Return attribValue
            return lstrAttributeValue;
        }


        /// <summary>
        /// [Currently unused in TOBW application]
        /// This method parse the input XML and returns XML embedded by the element given.
        /// </summary>
        /// <param name="pstrMessageBody">  This string is input XML.  </param>
        /// <param name="pstrElementXPath">
        /// XML element needing to be parsed with XPath information.
        /// </param>
        /// <returns>  XML data within this element.  </returns>
        public string ExtractElement(string pstrMessageBody, string pstrElementXPath)
        {
            //Method Name
            const string METHOD_NAME = "ExtractElement";

            //Create XMLDocument object. 
            XmlDocument lxmlDoc = null;
            XmlNode lxmlNode = null;
            //Create string buffer strExtracted to extract xml inside the element.
            string lstrExtracted = null;

            try
            {
                //Trace in Debug Mode.
               // clsCustomLog.Trace(CLASS_NAME, METHOD_NAME, string.Empty);

                //Create XMLDocument object and load input XML (pstrMessageBody).
                lxmlDoc.LoadXml(pstrMessageBody);

                //Query xml document with pstrElementXPath and If node is found then
                //return inner xml of the node.
                lxmlNode = lxmlDoc.SelectSingleNode(pstrElementXPath);

                if (lxmlNode != null)
                {
                    lstrExtracted = lxmlNode.InnerXml.ToString();
                }
            }
            catch (System.Exception exGeneral)
            {
              //  clsCustomLog.WriteEntry(METHOD_NAME + " caught exception = " + exGeneral.Message, clsDeviceConfiguration.GetLogMetadata());
                //throw new clsCustomException("XML_003", CLASS_NAME, METHOD_NAME, exGeneral);
            }
            return lstrExtracted;
        }

        /// <summary>
        /// This method find header element and parse the attribute information which
        /// are needed by the caller function.
        /// </summary>
        /// <param name="pstrMessageBody"> This string is input XML. </param>
        /// <param name="phtAttributes">  Hashtable of attributes to find values.</param>
        public void ExtractHeader(string pstrMessageBody, Hashtable phtAttributes)
        {
            //Method Name
            const string METHOD_NAME = "ExtractHeader";

            try
            {
                //Trace in Debug Mode.
              //  clsCustomLog.Trace(CLASS_NAME, METHOD_NAME, string.Empty);

                //If phtAttribute is empty then
                if (phtAttributes == null)
                {
                    phtAttributes = new Hashtable();
                    //Load following header attribute keys in pstrAttributes
                    phtAttributes.Add(XML_HDR_MSGTYPE, "");
                    phtAttributes.Add(XML_HDR_VER, "");
                    phtAttributes.Add(XML_HDR_SOURCE, "");
                    phtAttributes.Add(XML_HDR_DEST, "");
                }

                ParseData(pstrMessageBody, XML_EL_HDR_XPATH, phtAttributes);
            }
            //catch (clsCustomException exCustException)
            //{
            //    throw exCustException;
            //}
            catch (System.Exception exGeneral)
            {
            //   clsCustomLog.WriteEntry(METHOD_NAME + " caught exception = " + exGeneral.Message, clsDeviceConfiguration.GetLogMetadata());
              //  throw new clsCustomException("XML_004", CLASS_NAME, METHOD_NAME, exGeneral);
            }
        }

        /// <summary>
        /// This method creates a node based on the element name and attributes and 
        /// tag data (inner text) given as input and it appends as child node in the
        /// input XML and under parent’s XPath. It returns resultant XML as string.
        /// </summary>
        /// <param name="pstrElement">XML element to be created</param>
        /// <param name="phtAttributes">Attributes of the element</param>
        /// <param name="pstrExistingXML">Existing xml data</param>
        /// <param name="pstrParentXPath">XPath of the parent element.</param>
        /// <param name="pstrData">Inner text of the element</param>
        /// <returns>XML created according to supplied input.</returns>
        public string CreateXML(string pstrElement, System.Collections.Specialized.NameValueCollection pnvcAttributes, string pstrExistingXML, string pstrParentXPath, string pstrData)
        {
            //Method Name
            const string METHOD_NAME = "CreateXML";

            //Create XML document object, existingDOC.
            XmlDocument lxmlDoc = new XmlDocument();
            XmlNode lxmlNode = null;
            XmlAttribute lxmlAttrib = null;
            XmlNode lxmlParentNode = null;
            //Create strNewXML string buffer to store output newly created XML string.
            string lstrNewXML = null;

            try
            {
                //Trace in Debug Mode.
             //   clsCustomLog.Trace(CLASS_NAME, METHOD_NAME, string.Empty);

                //Create an element node with name newNode with pstrElement.
                lxmlNode = lxmlDoc.CreateElement(pstrElement);

                //Create attributes
                if (pnvcAttributes != null && pnvcAttributes.Count > 0)
                {
                    for (int i = 0; i < pnvcAttributes.Count; i++)
                    {
                        //Add attributes from phtAttributes into this new node.
                        lxmlAttrib = lxmlDoc.CreateAttribute(pnvcAttributes.GetKey(i));
                        lxmlAttrib.Value = pnvcAttributes.GetValues(i)[0];
                        lxmlNode.Attributes.Append(lxmlAttrib);
                    }
                }
                if (pstrData.Length > 0)
                {
                    lxmlNode.InnerText = pstrData;
                }

                //Create the node under the specified parent (e.g. pstrParentXPath) node
                //in the existing XML.
                if (pstrExistingXML != null && pstrExistingXML.Length > 0)
                {
                    //Load existing XML into XMLDocument object then find the parent node.
                    //If we found parent node then append the child node under that node.

                    lxmlDoc.LoadXml(pstrExistingXML);

                    lxmlParentNode = lxmlDoc.SelectSingleNode(pstrParentXPath);
                    if (lxmlParentNode != null)
                    {
                        lxmlParentNode.AppendChild(lxmlNode);
                    }
                }
                else
                {
                    lxmlDoc.AppendChild(lxmlNode);
                }

                //Get XML document in string.
                lstrNewXML = lxmlDoc.OuterXml.ToString();
            }
            catch (System.Exception exGeneral)
            {
              //  clsCustomLog.WriteEntry(METHOD_NAME + " caught exception = " + exGeneral.Message, clsDeviceConfiguration.GetLogMetadata());
                //throw new clsCustomException("XML_005", CLASS_NAME, METHOD_NAME, exGeneral);
            }
            //Return strNewXML
            return lstrNewXML;

        }

        /// <summary>
        /// This method creates a node based on the element name and attributes given 
        /// as input and it appends as child node in the input XML and under parent’s 
        /// XPath. It returns resultant XML as string.
        /// </summary>
        /// <param name="pstrElement">  XML element to be created  </param>
        /// <param name="phtAttributes">  Attributes of the element  </param>
        /// <param name="pstrExistingXML">  Existing xml data  </param>
        /// <param name="pstrParentXPath">  XPath of the parent element.  </param>
        /// <returns>  XML created according to supplied input.  </returns>
        public string CreateXML(string pstrElement, System.Collections.Specialized.NameValueCollection pnvcAttributes, string pstrExistingXML, string pstrParentXPath, int pintTemp)
        {
            //Method Name
            const string METHOD_NAME = "CreateXML";

            //Create XML document object, existingDOC.
            XmlDocument lxmlDoc = new XmlDocument();
            XmlNode lxmlNode = null;
            XmlAttribute lxmlAttrib = null;
            XmlNode lxmlParentNode = null;
            //Create strNewXML string buffer to store output newly created XML string.
            string lstrNewXML = null;

            try
            {
                //Trace in Debug Mode.
               // clsCustomLog.Trace(CLASS_NAME, METHOD_NAME, string.Empty);

                //Create an element node with name newNode with pstrElement.
                lxmlNode = lxmlDoc.CreateElement(pstrElement);

                //Create attributes
                if (pnvcAttributes != null && pnvcAttributes.Count > 0)
                {
                    for (int i = 0; i < pnvcAttributes.Count; i++)
                    {
                        //Add attributes from phtAttributes into this new node.
                        lxmlAttrib = lxmlDoc.CreateAttribute(pnvcAttributes.GetKey(i));
                        lxmlAttrib.Value = pnvcAttributes.GetValues(i)[0];
                        lxmlNode.Attributes.Append(lxmlAttrib);
                    }
                }

                //Create the node under the specified parent (e.g. pstrParentXPath) node
                //in the existing XML.
                if (pstrExistingXML != null && pstrExistingXML.Length > 0)
                {
                    //Load existing XML into XMLDocument object then find the parent node.
                    //If we found parent node then append the child node under that node.

                    lxmlDoc.LoadXml(pstrExistingXML);

                    lxmlParentNode = lxmlDoc.SelectSingleNode(pstrParentXPath);
                    if (lxmlParentNode != null)
                    {
                        lxmlParentNode.AppendChild(lxmlNode);
                    }
                }
                else
                {
                    lxmlDoc.AppendChild(lxmlNode);
                }

                //Get XML document in string.
                lstrNewXML = lxmlDoc.OuterXml.ToString();
            }
            catch (System.Exception exGeneral)
            {
               // clsCustomLog.WriteEntry(METHOD_NAME + " caught exception = " + exGeneral.Message, clsDeviceConfiguration.GetLogMetadata());
                //throw new clsCustomException("XML_005", CLASS_NAME, METHOD_NAME, exGeneral);
            }
            //Return strNewXML
            return lstrNewXML;
        }
        # endregion "Public Methods"

    }
}
