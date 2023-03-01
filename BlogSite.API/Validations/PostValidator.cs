using BlogSite.API.ViewModels.CommentVMs;
using BlogSite.API.ViewModels.PostVMs;
using FluentValidation;

namespace BlogSite.API.Validations
{
    public class PostValidator : AbstractValidator<CreatePostVM>
    {
        public PostValidator()
        {
            RuleFor(p => p.Title).NotEmpty().WithMessage("Title cannot be empty!");
            RuleFor(p => p.Title).MinimumLength(5).WithMessage("Title must be at least 5 characters!");

            RuleFor(p => p.Content).NotEmpty().WithMessage("Content cannot be empty!");
            RuleFor(p => p.Content).MinimumLength(10).WithMessage("Content must be at least 10 characters!");


        }
    }
}
