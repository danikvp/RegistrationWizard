using RegistrationWizard.Domain;
using RegistrationWizard.Domain.Entities;

namespace RegistrationWizard.Application
{
    public class ApplicationFacade : IApplicationFacade
    {
        private readonly IDbContext dbContext;

        public ApplicationFacade(IDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IQueryable<Country> GetCountry()
        {
            return dbContext.Country;
        }
        public IQueryable<Province> GetProvince(int? countryId)
        {
            return dbContext.Province.Where(p => countryId == null || p.Country.Id == countryId);
        }
    }
}
