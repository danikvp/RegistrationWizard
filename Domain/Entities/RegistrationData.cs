namespace RegistrationWizard.Domain.Entities
{
    public class RegistrationData
    {
        public int Id { get; set; }
        public required string Login { get; set; }
        public required string PasswordHash { get; set; }
        public required Country Country { get; set; }
        public required Province Province { get; set; }
    }
}
