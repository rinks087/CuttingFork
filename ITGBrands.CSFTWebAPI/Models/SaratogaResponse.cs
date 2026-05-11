namespace CSFTWebAPI.Models
{
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

    public class Location
    {
        public string SiemensLocn { get; set; }
        public string ProductIdLocn { get; set; }
        public string Description { get; set; }
        public string LocationType { get; set; }
    }

    public class FeederPurge
    { 
        public string BlendCode { get; set; }
        public string Description { get; set; }

        public string ItemCode { get; set; }
        public string FeederlocationId { get; set; }

        public string subTobacco_type { get; set; }

    
    }

}
