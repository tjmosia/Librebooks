using Librebooks.Areas.Admin.Models.BusinessSectorModels;
using Librebooks.Areas.Admin.Services;
using Librebooks.CoreLib.Operations;
using Librebooks.Models.Entity.SystemSpace;

using Microsoft.AspNetCore.Mvc;
namespace Librebooks.Areas.Admin.Controllers
{
	[ApiController]
	//[Authorize]
	[Route("sectors")]
	public class BusinessSectorsController (ISystemManager systemManager)
		: ControllerBase
	{
		private readonly ISystemManager sysManager = systemManager;

		[Route("create")]
		[HttpPost]
		public async Task<IActionResult> CreateAsync ([FromBody] BusinessSectorModel model)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var result = await sysManager.AddBusinessSectorAsync(new BusinessSector(model.Name!));

			if (result.Succeeded)
			{
				return Ok(TransactionResult<BusinessSectorDTO>.Success(new BusinessSectorDTO
				{
					Id = result.Model!.Id,
					Name = result.Model.Name
				}));
			}
			else
			{
				return Ok(TransactionResult.Failure(result.Errors));
			}
		}

		[Route("edit")]
		[HttpPost]
		public async Task<IActionResult> EditAsync ([FromBody] BusinessSectorEditModel model)
		{
			if (!ModelState.IsValid)
				return BadRequest();

			var sector = await sysManager.FindBusinessSectorByIdAsync(model.Id!);

			if (sector != null)
			{
				sector.Name = model.Name;
				var result = await sysManager.UpdateBusinessSectorAsync(sector);

				if (result.Succeeded)
					return Ok(TransactionResult<BusinessSectorDTO>.Success(BusinessSectorDTO.MapFromBusinessSector(result.Model!)));
				else
					return Ok(TransactionResult.Failure(result.Errors));
			}

			return Ok();
		}

		[Route("")]
		[HttpGet]
		public async Task<BusinessSectorDTO[]> GetAsync ()
		{
			var sectors = await sysManager.GetBusinessSectorsAsync();

			return (sectors.Count > 0) ?
				[..sectors.Select(p => new BusinessSectorDTO
				{
					Name = p.Name,
					Id = p.Id
				})] : [];
		}

		[Route("{businessSectorId}")]
		[HttpGet]
		public async Task<BusinessSectorDTO?> GetByIdAsync ([FromRoute] int businessSectorId)
		{
			var sector = await sysManager.FindBusinessSectorByIdAsync(businessSectorId);
			return sector != null ? BusinessSectorDTO.MapFromBusinessSector(sector) : null;
		}

		[Route("delete")]
		[HttpPost]
		public async Task<IActionResult> DeleteAsync ([FromBody] int[] businessSectorIds)
		{
			var sectors = await sysManager.FindBusinessSectorsByIdsAsync(businessSectorIds);

			if (sectors.Count > 0)
				await sysManager.DeleteBusinessSectorsAsync([.. sectors]);

			return Ok();
		}
	}
}
