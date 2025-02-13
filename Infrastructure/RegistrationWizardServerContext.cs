using Microsoft.EntityFrameworkCore;
using RegistrationWizard.Domain;
using RegistrationWizard.Domain.Entities;

namespace RegistrationWizard.Infrastructure
{
    public class RegistrationWizardServerContext : DbContext, IDbContext
    {
        public RegistrationWizardServerContext(DbContextOptions<RegistrationWizardServerContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.customizeModel();
        }

        public DbSet<Country> Country => Set<Country>();
        public DbSet<Province> Province => Set<Province>();



        IQueryable<Country> IDbContext.Country => Country;
        IQueryable<Province> IDbContext.Province => Province;
    }

}
