using RegistrationWizard.Domain.Entities;

namespace RegistrationWizard.Domain
{
    public interface IDbContext
    {
        IQueryable<Country> Country { get; }
        IQueryable<Province> Province{ get; }
    }
}
