namespace RegistrationWizard.Application
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
    }
}
