using MZBaseSample.Infrastructure.Repos;

namespace MZBaseSample.Infrastructure
{
    public interface ISampleDbUnitOfWork : MZBase.Infrastructure.IDynamicTestableUnitOfWorkAsync
    {
        ICompanyRepository Companies { get; }
    }
}
