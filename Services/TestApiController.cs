using HeThongDatBan.Data;
using HeThongDatBan.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HeThongDatBan.Services
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestApiController : ControllerBase
    {
        private readonly NhaHangData _user;

        public TestApiController(NhaHangData userController)
        {
            _user = userController;
        }

        [HttpPost]
        public IActionResult Login([FromBody] Test data)
        {
            try
            {
                var items = _user.Add(data);
                if (items != null)
                {
                    return Ok(items);

                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet]
        
        public async Task<IEnumerable<User>> GetUsersDapper()
        {
            try
            {
                var items = new User();
                
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
