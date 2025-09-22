using HeThongDatBan.Data;
using HeThongDatBan.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HeThongDatBan.Services
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserApiController : ControllerBase
    {
        private readonly UsersData _user;

        public UserApiController(UsersData userController)
        {
            _user = userController;
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<ApiResult<IEnumerable<User>>> GetUsers()
        {
            try
            {
                var items = await _user.GetUsers();
                if (items != null && items.Count() > 0)
                {
                    return new ApiResult<IEnumerable<User>>(true, "Lấy danh sách thành công", items, items.Count());
                }
                return new ApiResult<IEnumerable<User>>(true, "Không có dữ liệu");
            }
            catch (Exception ex)
            {
                return new ApiResult<IEnumerable<User>>(false, ex.Message);
            }
        }

        // API thêm mới user
        [HttpPost]
        public async Task<ApiResult<User>> CreateUser(User data)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(data.TaiKhoan) || string.IsNullOrWhiteSpace(data.MatKhau))
                {
                    return new ApiResult<User>(false, "Tên đăng nhập và email không được để trống");
                }

                var user = await _user.GetUser(data.TaiKhoan);
                if (user != null)
                {
                    return new ApiResult<User>(false, "Tên đăng nhập đã tồn tại");
                }

                // Tạo đối tượng User
                var dataSuccess = await _user.Add(data);

                return new ApiResult<User>(true, "Thêm thành công", dataSuccess, 1);
            }
            catch (Exception ex)
            {
                return new ApiResult<User>(false, ex.Message);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IEnumerable<User>> GetUsersDapper()
        {
            try
            {
                var items = await _user.GetUsersDapper();
                if (items != null && items.Count() > 0)
                {
                    return items;

                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IEnumerable<User>> GetUsersDapper1()
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

        [HttpGet]
        public async Task<IEnumerable<User>> GetUserFromSqlRaw()
        {
            try
            {
                var item = await _user.GetUsersFromSQLRawLinq();
                return item;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //[HttpGet]
        //public async Task<User> Login([FromBody] UserLogin data)
        //{
        //    try
        //    {
        //        var items = await _user.Login(data.TaiKhoan, data.MatKhau);
        //        if (items != null)
        //        {
        //            return items;

        //        }
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}
    }
}
