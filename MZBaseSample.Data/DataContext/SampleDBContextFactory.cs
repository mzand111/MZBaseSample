using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MZBaseSample.Data.DataContext
{
    public class SampleDBContextFactory : IDesignTimeDbContextFactory<SampleDBContext>
    {
        public const string DesignTimeConnectionString = "Data Source=.\\sql22;Initial Catalog=MZBaseSample;Integrated Security=True;Trust Server Certificate=True";
        public SampleDBContextFactory()
        {

        }

        public SampleDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SampleDBContext>();
            optionsBuilder.UseSqlServer(DesignTimeConnectionString);
            return new SampleDBContext(optionsBuilder.Options);
        }
    }
}
