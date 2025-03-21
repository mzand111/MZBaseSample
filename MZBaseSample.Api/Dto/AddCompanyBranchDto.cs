using MZBase.Domain;
using MZBaseSample.Domain;

namespace MZBaseSample.Api.Dto
{
    public class AddCompanyBranchDto : IDto<CompanyBranch, int>
    {
        public int CountryId { get; set; }
        public string PhoneNumber { get; set; }
        public CompanyBranch GetDomainObject()
        {
            CompanyBranch c = new CompanyBranch();
            c.PhoneNumber = PhoneNumber;
            c.CountryId = CountryId;
            return c;
        }
    }
}
