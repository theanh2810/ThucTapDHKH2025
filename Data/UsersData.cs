using Dapper;
using HeThongDatBan.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace HeThongDatBan.Data
{
    public class UsersData
    {
        private readonly QuanLyHeThongDatBanContext _context;
        private readonly string? _connectionString;
        public UsersData(QuanLyHeThongDatBanContext context)
        {
            _context = context;
            _connectionString = _context.Database.GetConnectionString();
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> Add(User data)
        {
            _context.Users.Add(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<User?> GetUser(string userName)
        {
            var item = await _context.Users.Where(u => u.TaiKhoan == userName).FirstOrDefaultAsync();
            return item;
        }

        public async Task<IEnumerable<User>> GetUsersDapper()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                DynamicParameters parameters = new DynamicParameters();
                IEnumerable<User> list = conn.Query<User>("select * from Users", commandType: CommandType.Text);
                return list;
            }
        }

        public async Task<User> Login(string taiKhoan, string matKhau)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@TaiKhoan", taiKhoan);
                parameters.Add("@MatKhau", matKhau);
                User list = conn.QueryFirstOrDefault<User>("sp_Users_Login", parameters, commandType: CommandType.StoredProcedure);
                return list;
            }
        }
    }
}
