using MZBase.Domain;
using MZBaseSample.Domain;

namespace MZBaseSample.Api.Dto
{
    public class EditCompanyDto : IDto<Company, int>
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public CompanyTypeEnum CompanyOwnershipType { get; set; }
        public int RegistrationNumber { get; set; }
        public DateTime RegistrationDate { get; set; }
        public List<EditCompanyBranchDto> CompanyBranches { get; set; }
        public Company GetDomainObject()
        {
            Company c = new Company();
            c.ID = ID;
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
