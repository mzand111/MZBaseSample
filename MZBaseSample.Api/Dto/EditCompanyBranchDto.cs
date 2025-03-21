using MZBase.Domain;
using MZBaseSample.Domain;

namespace MZBaseSample.Api.Dto
{
    public class EditCompanyBranchDto : IDto<CompanyBranch, int>
    {
        public int ID { get; set; }
        public int CountryId { get; set; }
        public string PhoneNumber { get; set; }
        public CompanyBranch GetDomainObject()
        {
            CompanyBranch c = new CompanyBranch();
            c.ID = ID;
            c.PhoneNumber = PhoneNumber;
            c.CountryId = CountryId;
            return c;
        }
    }
}
