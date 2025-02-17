using Microsoft.AspNetCore.Mvc;
using RegistrationWizard.Application;
using RegistrationWizard.Application.Models;
using RegistrationWizard.Domain.Entities;

namespace RegistrationWizard.Server.Controllers
{
    [Route("api/registration")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IApplicationFacade applicationFacade;

        public RegistrationController(IApplicationFacade applicationFacade) {
            this.applicationFacade = applicationFacade;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] RegisterCommand registerCommandModel)
        {
            return await applicationFacade.Register(registerCommandModel);
        }


    }
}
