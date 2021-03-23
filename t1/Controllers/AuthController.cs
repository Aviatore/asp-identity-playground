using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using ASP;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using t1.Models;
using t1.Resources;

namespace t1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IMapper _mapper;
        private UserManager<User> _userManager;
        private RoleManager<Role> _roleManager;
        
        public AuthController(IMapper mapper, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Test()
        {
            return Ok("response");
        }
        
        [AllowAnonymous]
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody]UserSignUpResource userSignUpResource)
        {
            //var user = _mapper.Map<UserSignUpResource, User>(userSignUpResource);
            var user = _mapper.Map<User>(userSignUpResource);
            Console.WriteLine($"userName: {user.UserName}");
            var userCreateResult = await _userManager.CreateAsync(user, userSignUpResource.Password);

            if (userCreateResult.Succeeded)
            {
                return Created(string.Empty, string.Empty);
            }

            return Problem(userCreateResult.Errors.ToString(), null, 500);
        }

        public async Task<IActionResult> SignIn(UserLogInResource userLogInResource)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Email == userLogInResource.Email);
            if (user is null)
            {
                return NotFound("User not found");
            }

            var userSignInResult = await _userManager.CheckPasswordAsync(user, userLogInResource.Password);

            if (userSignInResult)
            {
                return Ok();
            }

            return BadRequest("Email or password incorrect");
        }

        public async Task<IActionResult> CreateRole(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                return BadRequest("Role name should be provided.");
            }

            var newRole = new Role() {Name = roleName};

            var roleResult = await _roleManager.CreateAsync(newRole);

            if (roleResult.Succeeded)
            {
                return Ok();
            }

            return Problem(roleResult.Errors.First().Description, null, 500);
        }

        public async Task<IActionResult> AddUserToRole(Guid id, [FromBody] string roleName)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == id);

            var result = await _userManager.AddToRoleAsync(user, roleName);

            if (result.Succeeded)
            {
                return Ok();
            }

            return Problem(result.Errors.First().Description, null, 500);
        }
    }
}