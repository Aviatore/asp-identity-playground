using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using t1.Models;
using t1.Resources;

namespace t1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private IMapper _mapper;
        private UserManager<User> _userManager;
        
        public AuthController(IMapper mapper, UserManager<User> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        public IActionResult Test()
        {
            return Ok("response");
        }
        
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(UserSignUpResource userSignUpResource)
        {
            var user = _mapper.Map<UserSignUpResource, User>(userSignUpResource);
            var userCreateResult = _userManager.CreateAsync(user, userSignUpResource.Password);

            if (userCreateResult.IsCompletedSuccessfully)
            {
                return Created(string.Empty, string.Empty);
            }

            return Problem(userCreateResult.Exception.Message, null, 500);
        }
    }
}