using System.Xml.Linq;
using System.Xml.Serialization;

namespace CSFTWebAPI.Models
{

    [XmlRoot("InputModel")]
    public class InputModel
    {

        public int TagLayoutID { get; set; }

        public string ToXml()
        {
            XElement xml = new XElement("InputModel",
                new XElement("TagLayoutID", TagLayoutID)
            );

            return xml.ToString();
        }
    }
}
