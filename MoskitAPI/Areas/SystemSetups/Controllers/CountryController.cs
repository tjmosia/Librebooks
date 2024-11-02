using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;

using Moskit.Areas.SystemSetups.Services;
using Moskit.Models.Entity.SystemSpace;

namespace Moskit.Areas.SystemSetups.Controllers
{
    [Route("api/countries")]
    [ApiController]
    public class CountryController (ISystemManager systemManager)
        : ControllerBase
    {
        private readonly ISystemManager systemManager = systemManager;

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddCountryAsync ([FromBody] CountryModel input)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await systemManager.AddCountryAsync(new Country
            {
                Code = input.Code,
                DialingCode = input.DialingCode,
                Name = input.Name
            });

            if (result.Succeeded)
                return Created("", value: result.Model!);
            else
            {
                ModelState.AddModelError("", "Country Code or Name provided already exists.");
            }

            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("update")]
        public IActionResult UpdateCountryAsync ([FromBody] CountryModel input)
        {

            return Ok();
        }

        [HttpGet]
        [Route("{code}")]
        public async Task<object?> GetCountryAsync ([FromQuery] string code)
            => await systemManager.GetCountryByCodeAsync(code);


        [HttpPost]
        [Route("delete")]
        public IActionResult DeleteCountryAsync ([FromBody] string[] codes)
        {

            return Ok();
        }

        public class CountryModel
        {
            [Required(ErrorMessage = "Country code is required.")]
            public virtual string? Code { get; set; }
            public virtual string? DialingCode { get; set; }
            [Required(ErrorMessage = "Country name is required.")]
            public virtual string? Name { get; set; }
        }
    }
}
