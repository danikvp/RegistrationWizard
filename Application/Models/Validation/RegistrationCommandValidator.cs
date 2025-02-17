using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace RegistrationWizard.Application.Models.Validation
{
    public class RegistrationCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegistrationCommandValidator(IDbContext dbContext)
        {
            RuleFor(rc => rc.Login).NotEmpty().EmailAddress();

            RuleFor(rc => rc.Password)
                .NotEmpty()
                .Matches("(?=.*[A-Z])(?=.*\\d).*")
                .WithMessage("Password must contain min 1 digit and min 1 uppercase letter");

            RuleFor(rc => rc.CountryId)
                .MustAsync(async (countrId, token) =>
                {
                    var country = await dbContext.Country.FindAsync(countrId);
                    return country is not null;
                })
                .WithMessage("Specified Country Id is not valid");


            RuleFor(rc => rc.ProvinceId)
                .MustAsync(async (command, provinceId, token) =>
                {
                    var province = await dbContext
                                            .Province
                                            .Where(p => p.Id == provinceId && p.Country.Id == command.CountryId)
                                            .SingleOrDefaultAsync();

                    return province is not null;
                })
                .WithMessage("Specified Province Id is not valid");
        }
    }
}
