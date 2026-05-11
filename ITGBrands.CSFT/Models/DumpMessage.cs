
using System;
using System.Xml.Serialization;

namespace ITGBrands.CSFT.Models
{



    [XmlRoot("Msg")]
    public class Msg
    {
        [XmlElement("Hdr")]
        public Hdr Hdr { get; set; }

        [XmlElement("Tob_Storage")]
        public TobStorage TobStorage { get; set; }
    }

    public class Hdr
    {
        [XmlAttribute("MsgTyp")]
        public string MsgTyp { get; set; }

        [XmlAttribute("Ver")]
        public string Ver { get; set; }

        [XmlAttribute("Src")]
        public string Src { get; set; }

        [XmlAttribute("Dest")]
        public string Dest { get; set; }
    }

    public class TobStorage
    {
        [XmlAttribute("Act")]
        public string Act { get; set; }

        [XmlAttribute("Whs")]
        public string Whs { get; set; }

        [XmlAttribute("Cont")]
        public int Cont { get; set; }

        [XmlAttribute("Typ")]
        public string Typ { get; set; }

        [XmlAttribute("Itm")]
        public int Itm { get; set; }

        [XmlAttribute("Loc")]
        public string Loc { get; set; }

        [XmlAttribute("Vld")]
        public string Vld { get; set; }

        [XmlAttribute("Dtm")]
        public DateTime Dtm { get; set; }

        [XmlAttribute("Usr")]
        public string Usr { get; set; }
    }


}
