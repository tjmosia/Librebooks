﻿using Microsoft.AspNetCore.Identity;

namespace Librebooks.Areas.Identity.Services
{
    public class IdentityErrorDescriberExtension : IdentityErrorDescriber
    {
        public IdentityError UnVerifiedEmail ()
            => new IdentityError()
            {
                Code = nameof(UnVerifiedEmail),
                Description = "Email is not verified."
            };

        public override IdentityError InvalidEmail (string? email = null)
        {
            return new IdentityError
            {
                Code = nameof(InvalidEmail),
                Description = $"Email not registered."
            };
        }

        public IdentityError Duplicate (string? description)
        {
            return new IdentityError
            {
                Code = nameof(Duplicate),
                Description = $"{description}"
            };
        }
    }
}
