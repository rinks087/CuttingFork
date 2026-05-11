using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITGBrands.CSFT.Models
{

    public class ApiResponse
    {
        public List<TagLayout> TagLayouts { get; set; }
        public List<TagDetail> TagDetails { get; set; }
        public List<BlendCode> BlendCodes { get; set; }
        public List<FeederMatrix> FeederMatrices { get; set; }
        public List<Locations> Locations { get; set; }
    }
    public class TagLayout
    {
        public string TagLayoutId { get; set; }
        public string TagType { get; set; }
        public int Length { get; set; }
    }

    public class TagDetail
    {
        public string TagLayout { get; set; }
        public string TagType { get; set; }
        public int StartPosition { get; set; }
        public string FieldName { get; set; }
        public int FieldType { get; set; }
        public int FieldLength { get; set; }
    }

    public class BlendCode
    {
        public int Code { get; set; }
        public string CodeType { get; set; }
        public string BlendDescription { get; set; }
    }

    public class FeederMatrix
    {
        public string FeederCode { get; set; }
        public string SiemensLocn { get; set; }
        public string SugLocDescription { get; set; }
        public int BlendCode { get; set; }
    }

    public class Locations
    {
        public string SiemensLocn { get; set; }
        public string ProductIdLocn { get; set; }
        public string Description { get; set; }
        public string LocationType { get; set; }
    }
}
