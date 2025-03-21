using AutoMapper;
using MZBase.Infrastructure;
using MZBaseSample.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace MZBaseSample.Data.Entity
{
    public class CompanyBranchEntity : CompanyBranch, IConvertibleDBModelEntity<CompanyBranch>
    {
        #region AutoMapper setup
        private static readonly IMapper _mapper;
        static CompanyBranchEntity()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CompanyBranchEntity, CompanyBranch>();
                cfg.CreateMap<CompanyBranch, CompanyBranchEntity>();
            });
            _mapper = config.CreateMapper();
        }
        #endregion


        public CompanyEntity CompanyEntity { get; set; }
        public CountryEntity CountryEntity { get; set; }
        [NotMapped]
        public override Company Company => CompanyEntity;
        [NotMapped]
        public override Country Country => CountryEntity;


        #region IConvertibleDBModelEntity members
        public CompanyBranch GetDomainObject()
        {
            CompanyBranch set = _mapper.Map<CompanyBranch>(this);
            return set;
        }

        public void SetFieldsFromDomainModel(CompanyBranch domainModelEntity)
        {
            _mapper.Map(domainModelEntity, this);
        }
        #endregion
    }
}
