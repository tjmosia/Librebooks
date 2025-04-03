using System.ComponentModel.DataAnnotations;

namespace Librebooks.Areas.Identity.Models
{
    public class VerificationModels
    {

        public class SendRequestModel
        {
            [Required(ErrorMessage = "Email is required")]
            public string? Email { get; set; }

            [Required(ErrorMessage = "Type is required")]
            public string? RequestUri { get; set; }
        }

        public class VerifyRequestModel : SendRequestModel
        {
            [Required(ErrorMessage = "Code is required")]
            public string? Code { get; set; }
        }
    }
}
