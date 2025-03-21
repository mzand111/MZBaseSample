using MZBase.Domain;
using MZBaseSample.Domain;

namespace MZBaseSample.Api.Dto
{
    public class AddCompanyDto : IDto<Company, int>
    {
        public string Name { get; set; }
        public CompanyTypeEnum CompanyOwnershipType { get; set; }
        public int RegistrationNumber { get; set; }
        public DateTime RegistrationDate { get; set; }
        public List<AddCompanyBranchDto> CompanyBranches { get; set; }
        public Company GetDomainObject()
        {
            Company c = new Company();
            c.Name = this.Name;
            c.CompanyOwnershipType = CompanyOwnershipType;
            c.RegistrationDate = RegistrationDate;
            c.RegistrationNumber = RegistrationNumber;
            c.CompanyBranches = new List<CompanyBranch>();
            foreach (var item in CompanyBranches)
            {
                c.CompanyBranches.Add(item.GetDomainObject());
            }
            return c;
        }
    }
}
