using BlogSite.API.Services.Abstract;
using BlogSite.API.ViewModels.PostVMs;
using BlogSite.API.ViewModels.UserVMs;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogSite.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        //private readonly IValidator<CreateUserVM> _validator;


        public UsersController(IUserService userService/*, IValidator<CreateUserVM> validator*/)
        {
            _userService = userService;
            //_validator = validator;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var users = _userService.GetUsers();
            return Ok(users);
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateUserVM createUserVM)
        {
            //var validation = _validator.Validate(createUserVM);
            //if (validation.IsValid)
            //{
                _userService.CreateUser(createUserVM);
                return Ok();
            //}
            //return BadRequest(validation.Errors);
        }
    }
}
