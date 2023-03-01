using BlogSite.API.ViewModels.CommentVMs;
using FluentValidation;

namespace BlogSite.API.Validations
{
    public class CommentValidator : AbstractValidator<CreateCommentVM>
    {
        public CommentValidator()
        {
            RuleFor(c => c.Content).NotEmpty().WithMessage("Content  cannot be empty!");
            RuleFor(c => c.Content).MinimumLength(10).WithMessage("Content must be at least 10 characters!");

        }
    }
}
