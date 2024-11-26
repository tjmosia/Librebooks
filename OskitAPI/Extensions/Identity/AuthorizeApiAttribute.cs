﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MacbooksAPI.Extensions.Identity
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class AuthorizeApiAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization (AuthorizationFilterContext context)
        {
            throw new NotImplementedException();
        }
    }
}
