using Librebooks.Areas.Identity.Models;
using Librebooks.Areas.Identity.Services;
using Librebooks.CoreLib.Operations;
using Librebooks.Models.Entity.GeneralSpace;
using Librebooks.Providers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Librebooks.Areas.Identity.Controllers
{
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

            var user = userManager.FindByNameAsync(model.RequestUri!);

            if (user == null)
                return Ok(TransactionResult.Failure(TransactionError.Create("Email", "INVALID")));

            var request = await verificationManager.AddAsync(new VerificationRequest(model.Email!, model.RequestUri!));

            return Ok(request != null ?
                TransactionResult.Success :
                TransactionResult.Failure(TransactionError.Create("Email", "Unable to send verification to your email.")));
        }

        [HttpPost]
        [Route("check")]
        public async Task<IActionResult> VerifyAsync ([FromBody] VerificationModels.VerifyRequestModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var request = await verificationManager.VerifyAsync(model.Email!, model.RequestUri!, model.Code!);

            if (request == null)
                return Ok(TransactionResult.Failure());

            if (!request.Verified)
                return Ok(TransactionResult.Failure(TransactionError.Create("Code", "Invalid code provided.")));

            var user = await userManager.FindByNameAsync(request.Subject!);

            if (user == null)
            {
                return Ok(TransactionResult.Failure(TransactionError.Create("Email", "INVALID")));
            }
            else if (model.RequestUri!.Contains("register") || model.RequestUri.Contains("login"))
            {
                user.EmailConfirmed = true;
                await userManager.UpdateAsync(user);
                await verificationManager.DeleteAsync(request);
            }

            return Ok(TransactionResult.Success);
        }
    }
}
