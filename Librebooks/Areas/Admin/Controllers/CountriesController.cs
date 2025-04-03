using Librebooks.Areas.Admin.Models;
using Librebooks.Areas.Admin.Services;
using Librebooks.CoreLib.Operations;
using Librebooks.Models.Entity.SystemSpace;

using Microsoft.AspNetCore.Mvc;

namespace Librebooks.Areas.Admin.Controllers
{
    [Route("countries")]
    [ApiController]
    public class CountriesController (ISystemManager systemManager, ILogger<CountriesController> logger)
        : ControllerBase
    {
        private readonly ISystemManager sysManager = systemManager;
        private readonly ILogger<CountriesController> logger = logger;

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateCountryAsync ([FromBody] CountryModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var country = model.MapToCountry(new Country());
            country.NormalizeCode();

            var result = await sysManager.AddCountryAsync(country);

            if (result.Succeeded)
            {
                return Ok(TransactionResult<CountryDTO>.Success(CountryDTO.MapFromCountry(result.Model!)));
            }
            else
            {
                logger.LogError("Error Occured at CreateCountryAsync Method: Country Code or Name provided already exists.");
                ModelState.AddModelError("", "Country Code or Name provided already exists.");
            }

            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("edit")]
        public async Task<IActionResult> UpdateCountryAsync ([FromBody] CountryUpdateModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            TransactionResult<CountryDTO>? transResult = TransactionResult<CountryDTO>.Failure(TransactionError.Create("", "Unable to update country. Please try again."));

            var country = await sysManager.FindCountryByCodeAsync(model.CurrentCode!);

            if (string.IsNullOrEmpty(model.Code) || model.Code.Equals(model.CurrentCode, StringComparison.CurrentCultureIgnoreCase))
            {
                model.Code = model.CurrentCode;

                if (country != null)
                {
                    if (!country.Name!.Equals(model.Name, StringComparison.CurrentCultureIgnoreCase) ||
                        !country.DialingCode!.Equals(model.DialingCode, StringComparison.CurrentCultureIgnoreCase))
                    {
                        country.Name = model.Name;
                        country.DialingCode = model.DialingCode;
                        country.NormalizeCode();

                        var result = await sysManager.UpdateCountryAsync(country);

                        if (result.Succeeded)
                        {
                            country = result.Model!;
                            transResult = TransactionResult<CountryDTO>.Success(CountryDTO.MapFromCountry(country!));
                        }
                    }
                }
            }
            else
            {
                if (await sysManager.FindCountryByCodeAsync(model.Code.ToUpper()) != null)
                {
                    transResult = TransactionResult<CountryDTO>
                        .Failure(TransactionError.Create(nameof(CountryModel.Code), "A country with the same cost already exits."));
                }
                else if (country != null)
                {
                    var deleteResult = await sysManager.DeleteCountryAsync(country);

                    if (deleteResult.Succeeded)
                    {
                        var newCountry = model.MapToCountry(new Country());
                        newCountry.NormalizeCode();
                        var result = await sysManager.AddCountryAsync(newCountry);

                        if (result.Succeeded)
                            transResult = TransactionResult<CountryDTO>.Success(CountryDTO.MapFromCountry(result.Model!));
                    }
                }
            }

            return Ok(transResult);
        }

        [HttpGet]
        [Route("{code}")]
        public async Task<CountryDTO?> GetCountryAsync ([FromRoute] string code)
        {
            var country = await sysManager.FindCountryByCodeAsync(code);
            if (country == null)
                return null;

            return new CountryDTO
            {
                Code = country.Code,
                Name = country.Name,
                DialingCode = country.DialingCode
            };
        }

        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<CountryDTO>> GetCountriesAsync ()
        {
            var countries = await sysManager.GetCountriesAsync();

            return countries.Count > 0 ? countries.Select(p => new CountryDTO
            {
                Name = p.Name,
                Code = p.Code,
                DialingCode = p.DialingCode
            }).ToArray() : [];
        }

        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> DeleteCountryAsync ([FromBody] string[] countryCodes)
        {
            var countries = await sysManager.FindCountriesByCodesAsync(countryCodes);

            if (countries.Count > 0)
            {

                if ((await sysManager.DeleteCountryAsync([.. countries])).Succeeded)
                {
                    logger.LogInformation("Countries were successfully deleted.");
                    return Ok(TransactionResult.Success);
                }
                else
                    logger.LogError("Unable to delete countries.");
            }
            else
                logger.LogError("No Countries were found to be deleted.");

            return Ok(TransactionResult.Failure());
        }
    }
}
