using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using t1.Models;

namespace t1.Controllers
{
    public class DepartmentController : ControllerBase
    {
        private readonly TtDbContext _ttDbContext;

        public DepartmentController(TtDbContext ttDbContext)
        {
            _ttDbContext = ttDbContext;
        }
        
        [Authorize("User")]
        [HttpGet("[controller]/all")]
        public IActionResult GetAll()
        {
            return Ok(_ttDbContext.Departments);
        }
    }
}