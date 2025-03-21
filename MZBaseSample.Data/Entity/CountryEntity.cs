using AutoMapper;
using MZBase.Infrastructure;
using MZBaseSample.Domain;

namespace MZBaseSample.Data.Entity
{
    public class CountryEntity : Country, IConvertibleDBModelEntity<Country>
    {
        #region AutoMapper setup
        private static readonly IMapper _mapper;
        static CountryEntity()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CountryEntity, Country>();
                cfg.CreateMap<Country, CountryEntity>();
            });
            _mapper = config.CreateMapper();
        }
        #endregion
        public Country GetDomainObject()
        {
            Country set = _mapper.Map<Country>(this);
            return set;
        }

        public void SetFieldsFromDomainModel(Country domainModelEntity)
        {
            _mapper.Map(domainModelEntity, this);
        }
    }
}
