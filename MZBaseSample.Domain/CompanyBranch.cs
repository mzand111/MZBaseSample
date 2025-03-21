using MZBase.Domain;

namespace MZBaseSample.Domain
{
    public class CompanyBranch : Model<int>
    {
        public int CompanyId { get; set; }
        public int CountryId { get; set; }
        public string PhoneNumber { get; set; }
        public virtual Company Company { get; set; }
        public virtual Country Country { get; set; }
    }
}
