using AutoMapper;
using MZBase.Infrastructure;
using MZBaseSample.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace MZBaseSample.Data.Entity
{
    public class CompanyEntity : Company, IConvertibleDBModelEntity<Company>
    {
        #region AutoMapper setup
        private static readonly IMapper _mapper;
        static CompanyEntity()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CompanyEntity, Company>();
                cfg.CreateMap<Company, CompanyEntity>();
            });
            _mapper = config.CreateMapper();
        }
        #endregion
        public CompanyEntity()
        {
            CompanyBranchEntities = new List<CompanyBranchEntity>();
        }
        public virtual ICollection<CompanyBranchEntity> CompanyBranchEntities { get; set; }


        [NotMapped]
        public override ICollection<CompanyBranch> CompanyBranches
        {
            get => CompanyBranchEntities.Cast<CompanyBranch>().ToList();
        }

        #region IConvertibleDBModelEntity members
        public Company GetDomainObject()
        {
            Company set = _mapper.Map<Company>(this);
            if (this.CompanyBranchEntities != null)
            {
                if (set.CompanyBranches == null)
                    set.CompanyBranches = new List<CompanyBranch>();
                foreach (var dbBranch in CompanyBranchEntities)
                {
                    set.CompanyBranches.Add(dbBranch.GetDomainObject());
                }
            }
            return set;
        }

        public void SetFieldsFromDomainModel(Company domainModelEntity)
        {
            //Setting simple fields
            _mapper.Map(domainModelEntity, this);
            //Setting collections
            if (domainModelEntity.CompanyBranches != null)
            {
                var allNewBranchIds = domainModelEntity.CompanyBranches.Select(uu => uu.ID).ToList();
                var branchesToRemove = this.CompanyBranchEntities.Where(uu => !allNewBranchIds.Contains(uu.ID)).ToList();
                foreach (var itemToRemove in branchesToRemove)
                {
                    this.CompanyBranchEntities.Remove(itemToRemove);
                }
                foreach (var branch in domainModelEntity.CompanyBranches)
                {
                    if (branch.ID < 1)
                    {
                        CompanyBranchEntity be = new CompanyBranchEntity();
                        be.SetFieldsFromDomainModel(branch);
                        this.CompanyBranchEntities.Add(be);
                    }
                    else
                    {
                        var dbBranch = this.CompanyBranchEntities.FirstOrDefault(uu => uu.ID == branch.ID);
                        dbBranch.SetFieldsFromDomainModel(branch);
                        if (this.ID > 0)
                            dbBranch.CompanyId = this.ID;
                    }
                }
            }
        }
        #endregion
    }
}
