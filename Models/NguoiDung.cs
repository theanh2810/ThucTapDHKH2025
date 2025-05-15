using System;
using System.Collections.Generic;

namespace HeThongDatBan.Models;

public partial class NguoiDung
{
    public int NguoiDungId { get; set; }

    public string TenDangNhap { get; set; } = null!;

    public string MatKhau { get; set; } = null!;

    public string? Email { get; set; }

    public string? SoDienThoai { get; set; }

    public string? HoTen { get; set; }

    public int VaiTro { get; set; }

    public DateTime? NgayTao { get; set; }

    public int? TrangThai { get; set; }

    public virtual ICollection<DanhGium> DanhGia { get; set; } = new List<DanhGium>();

    public virtual ICollection<DatBan> DatBans { get; set; } = new List<DatBan>();

    public virtual ICollection<LichSuTimKiem> LichSuTimKiems { get; set; } = new List<LichSuTimKiem>();

    public virtual ICollection<NhaHang> NhaHangs { get; set; } = new List<NhaHang>();

    public virtual ICollection<ThanhToan> ThanhToans { get; set; } = new List<ThanhToan>();

    public virtual ICollection<TinNhan> TinNhanNguoiGuis { get; set; } = new List<TinNhan>();

    public virtual ICollection<TinNhan> TinNhanNguoiNhans { get; set; } = new List<TinNhan>();
}
