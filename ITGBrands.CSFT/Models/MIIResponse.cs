using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace ITGBrands.CSFT.Models
{
    
    [XmlRoot("Rowset")]
    public class Rowset
    {
        [XmlElement("Row")]
        public Row Row { get; set; }
    }

    public class Row
    {
        [XmlElement("Return")]
        public Return Return { get; set; }
    }

    public class Return
    {
        [XmlElement("Ret_Code")]
        public string RetCode { get; set; }

        [XmlElement("Ret_Desc")]
        public string RetDesc { get; set; }
    }

}
