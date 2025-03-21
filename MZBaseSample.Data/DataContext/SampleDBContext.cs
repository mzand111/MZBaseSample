using Microsoft.EntityFrameworkCore;
using MZBaseSample.Data.Entity;

namespace MZBaseSample.Data.DataContext
{
    public class SampleDBContext : DbContext
    {
        public virtual DbSet<CountryEntity> Country { get; set; }
        public virtual DbSet<CompanyEntity> Company { get; set; }
        public virtual DbSet<CompanyBranchEntity> CompanyBranch { get; set; }
        public SampleDBContext(DbContextOptions<SampleDBContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CountryEntity>()
                .Property(uu => uu.ID)
                .ValueGeneratedNever();
            modelBuilder.Entity<CompanyEntity>()
                .HasMany(uu => uu.CompanyBranchEntities)
                .WithOne(uu => uu.CompanyEntity)
                .HasForeignKey(uu => uu.CompanyId);

            modelBuilder.Entity<CompanyBranchEntity>()
                .HasOne(uu => uu.CountryEntity)
                .WithMany()
                .HasForeignKey(uu => uu.CountryId);

        }

        public static void SeedStaticData(DbContext context)
        {
            var c1 = context.Set<CountryEntity>().FirstOrDefaultAsync(b => b.ID == 1);
            if (c1 == null)
            {
                context.Set<CountryEntity>().Add(new CountryEntity
                {
                    ID = 1,
                    Name = "USA",
                    Code = "us"
                });
            }
            var c2 = context.Set<CountryEntity>().FirstOrDefaultAsync(b => b.ID == 2);
            if (c2 == null)
            {
                context.Set<CountryEntity>().Add(new CountryEntity
                {
                    ID = 2,
                    Name = "Canada",
                    Code = "ca"
                });
            }
            var c3 = context.Set<CountryEntity>().FirstOrDefaultAsync(b => b.ID == 3);
            if (c3 == null)
            {
                context.Set<CountryEntity>().Add(new CountryEntity
                {
                    ID = 3,
                    Name = "Qatar",
                    Code = "qa"
                });
            }
            var c4 = context.Set<CountryEntity>().FirstOrDefaultAsync(b => b.ID == 4);
            if (c4 == null)
            {
                context.Set<CountryEntity>().Add(new CountryEntity
                {
                    ID = 4,
                    Name = "Iran",
                    Code = "ir"
                });
            }
            var c5 = context.Set<CountryEntity>().FirstOrDefaultAsync(b => b.ID == 5);
            if (c5 == null)
            {
                context.Set<CountryEntity>().Add(new CountryEntity
                {
                    ID = 5,
                    Name = "Germany",
                    Code = "de"
                });
            }
            context.SaveChanges();
        }

        public static async Task SeedStaticDataAsync(DbContext context, CancellationToken cancellationToken)
        {
            var c1 = await context.Set<CountryEntity>().FirstOrDefaultAsync(b => b.ID == 1, cancellationToken);
            if (c1 == null)
            {
                context.Set<CountryEntity>().Add(new CountryEntity
                {
                    ID = 1,
                    Name = "USA",
                    Code = "us"
                });
            }
            var c2 = await context.Set<CountryEntity>().FirstOrDefaultAsync(b => b.ID == 2, cancellationToken);
            if (c2 == null)
            {
                context.Set<CountryEntity>().Add(new CountryEntity
                {
                    ID = 2,
                    Name = "Canada",
                    Code = "ca"
                });
            }
            var c3 = await context.Set<CountryEntity>().FirstOrDefaultAsync(b => b.ID == 3, cancellationToken);
            if (c3 == null)
            {
                context.Set<CountryEntity>().Add(new CountryEntity
                {
                    ID = 3,
                    Name = "Qatar",
                    Code = "qa"
                });
            }
            var c4 = await context.Set<CountryEntity>().FirstOrDefaultAsync(b => b.ID == 4, cancellationToken);
            if (c4 == null)
            {
                context.Set<CountryEntity>().Add(new CountryEntity
                {
                    ID = 4,
                    Name = "Iran",
                    Code = "ir"
                });
            }
            var c5 = await context.Set<CountryEntity>().FirstOrDefaultAsync(b => b.ID == 5, cancellationToken);
            if (c5 == null)
            {
                context.Set<CountryEntity>().Add(new CountryEntity
                {
                    ID = 5,
                    Name = "Germany",
                    Code = "de"
                });
            }
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
