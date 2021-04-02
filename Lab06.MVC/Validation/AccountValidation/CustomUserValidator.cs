using Lab06.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lab06.MVC.Validation.AccountValidation
{
    public class CustomUserValidator : IUserValidator<ApplicationUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<ApplicationUser> manager, ApplicationUser user)
        {
            List<IdentityError> errorList = new List<IdentityError>();

            if (user.UserName.Length < 4)
            {
                errorList.Add(new IdentityError
                {
                    Description = "Login must have at least 4 characters"
                });
            }

            return Task.FromResult(errorList.Count == 0 ? IdentityResult.Success : IdentityResult.Failed(errorList.ToArray()));
        }
    }
}
