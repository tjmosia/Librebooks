using Librebooks.Areas.Admin.Models;
using Librebooks.Areas.Admin.Services;
using Librebooks.CoreLib.Operations;

using Microsoft.AspNetCore.Mvc;

namespace Librebooks.Areas.Admin.Controllers
{
    [Route("taxtypes")]
    [ApiController]
    public class TaxTypesController (ISystemManager systemManager, ILogger<CountriesController> logger)
        : ControllerBase
    {
        private readonly ISystemManager sysManager = systemManager;
        private readonly ILogger<CountriesController> logger = logger;

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateAsync ([FromBody] TaxTypeModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok();
        }

        [HttpPost]
        [Route("edit")]
        public async Task<IActionResult> EditTaxTypeAsync ([FromBody] TaxTypeModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            TransactionResult<CountryDTO>? transResult = TransactionResult<CountryDTO>.Failure(TransactionError.Create("", "Unable to update country. Please try again."));

            return Ok(transResult);
        }

        [HttpGet]
        [Route("{countryCode}")]
        public async Task<CountryDTO?> GetTaxTypeAsync ([FromRoute] string code)
        {
            return null;
        }

        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<object>> GetAsync ()
        {
            var taxTypes = await sysManager.GetTaxTypesAsync();

            return taxTypes.Count > 0 ? taxTypes.Select(p => new
            {
                p.Name,
                p.Rate,
                p.System
            }).ToList() : [];
        }

        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> DeleteCountryAsync ([FromBody] string[] countryCodes)
        {
            return Ok(TransactionResult.Failure());
        }
    }
}
