using Microsoft.AspNetCore.Mvc;
using RegistrationWizard.Application;
using RegistrationWizard.Domain.Entities;
using RegistrationWizard.Server.Models;

namespace RegistrationWizard.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DictionariesController : ControllerBase
    {
        private readonly IApplicationFacade _applicationFacade;


        public DictionariesController(IApplicationFacade applicationFacade)
        {
            this._applicationFacade = applicationFacade;
        }

        // GET: api/Dictionaries/Countries
        [HttpGet("countries")]
        public async Task<ActionResult<IEnumerable<Country>>> GetCountry()
        {
            return await _applicationFacade.GetCountry();
        }

        // GET: api/Dictionaries/Provinces
        [HttpGet("provinces")]
        public async Task<ActionResult<IEnumerable<Province>>> GetProvince([FromQuery] ProvinceQueryParams queryParams)
        {
            return await _applicationFacade.GetProvince(queryParams.CountryId);
        }
    }
}
