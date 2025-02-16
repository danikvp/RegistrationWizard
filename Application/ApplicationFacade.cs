using Microsoft.EntityFrameworkCore;
using RegistrationWizard.Application.Models;
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

        public async Task<List<Country>> GetCountry()
        {
            return await dbContext.Country.ToListAsync();
        }
        public async Task<List<Province>> GetProvince(int? countryId)
        {
            return await dbContext.Province.Where(p => countryId == null || p.Country.Id == countryId).ToListAsync();
        }

        public async Task<int> Register(RegisterCommand registerCommand)
        {
            RegistrationData regData = new RegistrationData()
            {
                Login = registerCommand.Login,
                Password = registerCommand.Password,
                Country = dbContext.Country.First(c => c.Id == registerCommand.CountryId),
                Province = dbContext.Province.First(p => p.Id == registerCommand.ProvinceId)

            };


            dbContext.RegistrationData.Add(regData);
            return await dbContext.SaveChangesAsync();
        }
    }
}
