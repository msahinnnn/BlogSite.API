using BlogSite.API.ViewModels.UserVMs;
using FluentValidation;

namespace BlogSite.API.Validations
{
    public class UserValidator : AbstractValidator<CreateUserVM>
    {
        public UserValidator()
        {
            RuleFor(u => u.Name).NotEmpty().WithMessage("First name cannot be empty!");
            RuleFor(u => u.Name).MinimumLength(2).WithMessage("First name must be at least 2 characters!");

            RuleFor(u => u.Surname).NotEmpty().WithMessage("Last name cannot be empty!");
            RuleFor(u => u.Surname).MinimumLength(2).WithMessage("Last name must be at least 2 characters!");

            RuleFor(u => u.Email).NotEmpty().WithMessage("Email cannot be empty!");
            RuleFor(u => u.Email).EmailAddress().WithMessage("Email must be mail address type!");

        }
    }
}
