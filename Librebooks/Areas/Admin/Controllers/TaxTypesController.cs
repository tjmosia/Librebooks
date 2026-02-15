using Librebooks.Areas.Admin.Services;

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

	}
}
