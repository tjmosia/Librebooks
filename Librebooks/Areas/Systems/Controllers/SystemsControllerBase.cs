using Librebooks.Areas.Systems.Services;
using Librebooks.Data;
using Microsoft.AspNetCore.Mvc;

namespace Librebooks.Areas.Systems.Controllers;

[Route("systems/[controller]")]
[ApiController]
public abstract class SystemsControllerBase (ISystemsManager sysManager, AppDbContext? context = null, ILogger<SystemsControllerBase>? logger = null) : ControllerBase
{
	protected readonly ISystemsManager Manager = sysManager;
	protected readonly ILogger<SystemsControllerBase>? Logger = logger;
	protected readonly AppDbContext? Context = context;

}
