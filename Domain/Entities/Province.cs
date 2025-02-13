namespace RegistrationWizard.Domain.Entities
{
    public class Province
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required Country Country { get; set; }
    }
}
