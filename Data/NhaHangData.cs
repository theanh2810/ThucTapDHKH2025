using Dapper;
using HeThongDatBan.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Globalization;

namespace HeThongDatBan.Data
{
    public class NhaHangData
    {
        private readonly QuanLyHeThongDatBanContext _context;
        private readonly string? _connectionString;
        public NhaHangData(QuanLyHeThongDatBanContext context)
        {
            _context = context;
            _connectionString = _context.Database.GetConnectionString();
        }

        public async Task<IEnumerable<NhaHang>> GetNhaHangs()
        {
            return await _context.NhaHangs.ToListAsync();
        }
      
        public async Task<Test> Add(Test data)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                
                await conn.OpenAsync();
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@TenNhaHang", data.TenNhaHang);
               
                parameters.Add("@GioMoCua", data.GioMoCua);
                Test list = conn.QueryFirstOrDefault<Test>("spu_NhaHang_Add", parameters, commandType: CommandType.StoredProcedure);
                if(list != null)
                    return list;
                return null;
            }
        }

        public async Task<NhaHang?> GetNhaHang(int id)
        {
            var item = await _context.NhaHangs.Where(u => u.NhaHangId == id).FirstOrDefaultAsync();
            return item;
        }

        public async Task<IEnumerable<NhaHang>> GetNhaHangsDapper()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                DynamicParameters parameters = new DynamicParameters();
                IEnumerable<NhaHang> list = conn.Query<NhaHang>("select * from NhaHangs", commandType: CommandType.Text);
                return list;
            }
        }
    }
    

}
