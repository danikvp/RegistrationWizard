using Microsoft.EntityFrameworkCore;
using RegistrationWizard.Domain.Entities;

namespace RegistrationWizard.Application
{
    public interface IDbContext
    {
        DbSet<Country> Country { get; }
        DbSet<Province> Province{ get; }
        DbSet<RegistrationData> RegistrationData { get; }


        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
