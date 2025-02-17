using Microsoft.EntityFrameworkCore;
using RegistrationWizard.Domain.Entities;

namespace RegistrationWizard.Infrastructure
{
    public static class ModelBuilderExtensions
    {
        public static void customizeModel(this ModelBuilder modelBuilder) {

            var country = modelBuilder.Entity<Country>();
            country.Property(c => c.Name).HasMaxLength(64).IsRequired();
            country.HasIndex(c => c.Name).IsUnique();

            var province = modelBuilder.Entity<Province>();
            province.Property(p => p.Name).HasMaxLength(64).IsRequired();
            province.HasIndex(p=>p.Name).IsUnique(true);
            province.HasOne(p=>p.Country).WithMany().OnDelete(DeleteBehavior.NoAction);


            var registrationData = modelBuilder.Entity<RegistrationData>();
            registrationData.Property(rd => rd.Login).HasMaxLength(64).IsRequired();
            registrationData.Property(rd => rd.Password).HasMaxLength(64).IsRequired();

            registrationData.HasOne(rd=>rd.Province).WithMany().OnDelete(DeleteBehavior.NoAction);
            registrationData.HasOne(rd => rd.Country).WithMany().OnDelete(DeleteBehavior.NoAction);


        }
    }
}
