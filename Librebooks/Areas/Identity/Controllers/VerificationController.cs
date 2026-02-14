using Librebooks.Areas.Identity.Models;
using Librebooks.Areas.Identity.Services;
using Librebooks.CoreLib.Operations;
using Librebooks.Models.Entity.GeneralSpace;
using Librebooks.Providers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Librebooks.Areas.Identity.Controllers;

[Route("verifications")]
[ApiController]
[AllowAnonymous]
public class VerificationController (IVerificationManager verificationManager, UserManagerExtension userManager) : ControllerBase
{
    private readonly IVerificationManager verificationManager = verificationManager;
    private readonly UserManagerExtension userManager = userManager;

    [HttpPost]
    [Route("send")]
    public async Task<IActionResult> SendAsync ([FromBody] VerificationModels.SendRequestModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = userManager.FindByNameAsync(model.Reason!);

        if (user == null)
            return Ok(TransactionResult.Failure(TransactionError.Create("Email", "INVALID")));

        var request = await verificationManager.AddAsync(new VerificationRequest(model.Email!, model.Reason!));

        return Ok(request != null ?
            TransactionResult.Success :
            TransactionResult.Failure(TransactionError.Create("Email", "Unable to send verification to your email.")));
    }

    [HttpPost("check")]
    public async Task<IActionResult> VerifyAsync ([FromBody] VerificationModels.VerifyRequestModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await verificationManager.VerifyAsync(model.Email!, model.Reason!, model.Code!);

        if (!result.Succeeded && result.Errors.Any(p => p.Code == nameof(model.Email)))
            return NotFound(result);

        return Ok(TransactionResult.Success);
    }
}
