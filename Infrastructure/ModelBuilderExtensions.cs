using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RegistrationWizard.Domain.Entities;

namespace RegistrationWizard.Infrastructure
{
    public static class ModelBuilderExtensions
    {
        public static void customizeModel(this ModelBuilder modelBuilder) {

            var countryName = modelBuilder.Entity<Country>();
            countryName.Property(cn => cn.Name).HasMaxLength(64).IsRequired();
            countryName.HasIndex(cn => cn.Name).IsUnique();

            var provinceName = modelBuilder.Entity<Province>();
            provinceName.Property(pn => pn.Name).HasMaxLength(64).IsRequired();
            provinceName.HasIndex(pn=>pn.Name).IsUnique(true);


            var registrationData = modelBuilder.Entity<RegistrationData>();
            registrationData.Property(rd => rd.Login).HasMaxLength(64).IsRequired();
            registrationData.Property(rd => rd.Password).HasMaxLength(64).IsRequired();

            registrationData.HasOne(rd=>rd.Province).WithMany().OnDelete(DeleteBehavior.NoAction);


        }
    }
}
