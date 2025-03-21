using MZBase.Domain;
using MZBase.EntityFrameworkCore;
using MZBase.Infrastructure;
using MZBaseSample.Data.DataContext;
using MZBaseSample.Domain;
using MZBaseSample.Infrastructure.Repos;

namespace MZBaseSample.Infrastructure
{
    public class SampleDbUnitOfWork : UnitOfWorkAsync<SampleDBContext>, ISampleDbUnitOfWork
    {
        public ICompanyRepository Companies { get; private set; }
        public SampleDbUnitOfWork(SampleDBContext dataContext) : base(dataContext)
        {
            Companies = new CompanyRepository(_dbContext);
        }
        public IBaseLDRCompatibleRepositoryAsync<TModel, TDBModel, PrimKey> GetRepo<TModel, TDBModel, PrimKey>()
                where TModel : Model<PrimKey>
                where TDBModel : TModel, IConvertibleDBModelEntity<TModel>, new()
                where PrimKey : struct
        {
            IBaseLDRCompatibleRepositoryAsync<TModel, TDBModel, PrimKey> repo = null;

            if (typeof(TModel) == typeof(Company))
            {
                repo = Companies as IBaseLDRCompatibleRepositoryAsync<TModel, TDBModel, PrimKey>;

            }
            return repo;
        }
    }
}
