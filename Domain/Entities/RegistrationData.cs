using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationWizard.Domain.Entities
{
    public class RegistrationData
    {
        public int Id { get; set; }
        public required string Login { get; set; }
        public required string Password { get; set; }
        public required Country Country { get; set; }
        public required Province Province { get; set; }
    }
}
