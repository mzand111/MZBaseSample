using Microsoft.EntityFrameworkCore;
using MZBase.EntityFrameworkCore;
using MZBaseSample.Data.DataContext;
using MZBaseSample.Data.Entity;
using MZBaseSample.Domain;

namespace MZBaseSample.Infrastructure.Repos
{
    public class CompanyRepository : BaseLDRCompatibleRepositoryAsync<Company, CompanyEntity, int>, ICompanyRepository
    {
        private readonly SampleDBContext _context;

        public CompanyRepository(SampleDBContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<CompanyEntity?> GetByIdAsync(int id)
        {
            return await _context.Company.Include(uu => uu.CompanyBranchEntities).FirstOrDefaultAsync(uu => uu.ID == id);
        }
    }
}
