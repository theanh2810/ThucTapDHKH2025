using System;
using System.Configuration;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using HeThongDatBan.Data;
using HeThongDatBan.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class CancelExpiredReservationsService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public CancelExpiredReservationsService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;

    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                // Gọi logic kiểm tra và hủy đơn đặt bàn quá hạn
                await CheckAndCancelExpiredReservations();

                // Chờ 10 phút trước khi chạy lại
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
            catch (Exception ex)
            {
                // Log lỗi nếu có
                Console.WriteLine($"Lỗi trong Background Service: {ex.Message}");
            }
        }
    }

    private async Task CheckAndCancelExpiredReservations()
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            // Lấy chuỗi kết nối từ cấu hình
            var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // Tạo kết nối cơ sở dữ liệu
            using (var conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();
                DynamicParameters parameters = new DynamicParameters();
                IEnumerable<TestDatBan> list = conn.Query<TestDatBan>("select * from TestDatBan", commandType: CommandType.Text);
                
                foreach (var reservation in list)
                {
                    // Kiểm tra thời gian đặt bàn
                    if (reservation.ThoiGianDat < DateTime.Now)
                    {
                        // Hủy đơn đặt bàn
                        parameters = new DynamicParameters();
                        parameters.Add("@ID", reservation.ID);
                        await conn.ExecuteAsync("sp_TestDatBan_Update", parameters, commandType: CommandType.StoredProcedure);
                    }
                }
            }
        }
    }
}