using System.Text;
using RegistrationWizard.Application;

namespace RegistrationWizard.Infrastructure
{
    public class PasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            // test implementation
            // for demo purposes only
            return Convert.ToBase64String(Encoding.ASCII.GetBytes(password));
        }

    }
}
