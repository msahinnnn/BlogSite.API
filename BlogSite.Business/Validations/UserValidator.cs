using BlogSite.API.ViewModels.UserVMs;
using FluentValidation;

namespace BlogSite.API.Validations
{
    public class UserValidator : AbstractValidator<CreateUserVM>
    {
        public UserValidator()
        {
            RuleFor(u => u.FirstName).NotEmpty().WithMessage("First name cannot be empty!");
            RuleFor(u => u.FirstName).MinimumLength(2).WithMessage("First name must be at least 2 characters!");

            RuleFor(u => u.LastName).NotEmpty().WithMessage("Last name cannot be empty!");
            RuleFor(u => u.LastName).MinimumLength(2).WithMessage("Last name must be at least 2 characters!");

            RuleFor(u => u.Email).NotEmpty().WithMessage("Email cannot be empty!");
            RuleFor(u => u.Email).EmailAddress().WithMessage("Email must be mail address type!");

            RuleFor(u => u.Password).NotEmpty().WithMessage("Password cannot be empty!");
            RuleFor(u => u.Password).MinimumLength(5).WithMessage("Password must be at least 5 characters!");

        }
    }
}
