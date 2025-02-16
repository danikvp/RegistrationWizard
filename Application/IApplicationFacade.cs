using RegistrationWizard.Application.Models;
using RegistrationWizard.Domain.Entities;

namespace RegistrationWizard.Application
{
    public interface IApplicationFacade
    {
        Task<List<Country>> GetCountry();
        Task <List<Province>> GetProvince(int? countryId);

        Task<int> Register(RegisterCommand registerCommand);
    }
}
