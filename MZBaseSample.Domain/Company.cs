using MZBase.Domain;
using System;
using System.Collections.Generic;

namespace MZBaseSample.Domain
{
    public class Company : Auditable<int>
    {
        public Company()
        {

        }
        public string Name { get; set; }
        public CompanyTypeEnum CompanyOwnershipType { get; set; }
        public int RegistrationNumber { get; set; }
        public DateTime RegistrationDate { get; set; }
        public virtual ICollection<CompanyBranch> CompanyBranches { get; set; } = new List<CompanyBranch>();

    }
}
