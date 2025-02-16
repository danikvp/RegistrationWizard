using Microsoft.AspNetCore.Mvc;
using RegistrationWizard.Application;
using RegistrationWizard.Application.Models;
using RegistrationWizard.Domain.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

        // POST api/<RegistrationController>
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] RegisterCommand registerCommandModel)
        {
            return await applicationFacade.Register(registerCommandModel);
        }


    }
}
