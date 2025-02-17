namespace RegistrationWizard.Application.Models
{
    public class RegisterCommand
    {
        public required string Login { get; set; }
        public required string Password { get; set; }
        public required int CountryId { get; set; }
        public required int ProvinceId { get; set; }
    }
}
