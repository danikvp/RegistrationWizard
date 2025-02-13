using RegistrationWizard.Domain.Entities;

namespace RegistrationWizard.Application
{
    public interface IApplicationFacade
    {
        IQueryable<Country> GetCountry();
        IQueryable<Province> GetProvince(int? countryId);
    }
}
