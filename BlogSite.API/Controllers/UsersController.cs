using BlogSite.API.Models;
using BlogSite.API.Services.Abstract;
using BlogSite.API.ViewModels.CommentVMs;
using BlogSite.API.ViewModels.PostVMs;
using BlogSite.API.ViewModels.UserVMs;
using BlogSite.DataAccsess.Abstract;
using BlogSite.DataAccsess.Concrete.AdoNet;
using BlogSite.Entities.ViewModels.CommentVMs;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogSite.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        //private IUserService _userService;
        ////private readonly IValidator<CreateUserVM> _validator;
        //private IUserRepository _userRepository;


        //public UsersController(IUserService userService/*, IValidator<CreateUserVM> validator*/, IUserRepository userRepository)
        //{
        //    _userService = userService;
        //    _userRepository = userRepository;
        //    //_validator = validator;
        //}

        //[HttpGet("[action]")]
        //public IActionResult Get()
        //{
        //    var users = _userRepository.GetAllUsers();
        //    return Ok(users);
        //}


        //[HttpGet("[action]/{userId}")]
        //public IActionResult GetUserById(Guid userId)
        //{
        //    var user = _userRepository.GetUserById(userId);
        //    return Ok(user);
        //}

        //[HttpPost("[action]")]
        //public IActionResult Post([FromBody] CreateUserVM createUserVM, [FromQuery] Guid postId)
        //{
        //    //_userRepository.CreateUser()
        //    return Ok();
        //}

        //[HttpDelete("[action]")]
        //public IActionResult Delete([FromQuery] Guid commentId)
        //{
        //    var res = _userRepository.DeleteComment(commentId);
        //    return Ok(res);
        //}

        //[HttpPut("[action]")]
        //public IActionResult Update([FromBody] UpdateCommentVM updateCommentVM, [FromQuery] Guid commentId)
        //{
        //    var res = _commentRepository.UpdateComment(updateCommentVM, commentId);
        //    return Ok(res);
        //}
    }
}
