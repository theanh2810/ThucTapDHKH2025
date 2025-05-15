using System;
using System.Collections.Generic;

namespace HeThongDatBan.Models;

public partial class DatBan
{
    public int DatBanId { get; set; }

    public int? KhachHangId { get; set; }

    public int? NhaHangId { get; set; }

    public DateTime NgayDat { get; set; }

    public int SoKhach { get; set; }

    public int? TrangThai { get; set; }

    public string? GhiChu { get; set; }

    public virtual NguoiDung? KhachHang { get; set; }

    public virtual NhaHang? NhaHang { get; set; }

    public virtual ICollection<ThanhToan> ThanhToans { get; set; } = new List<ThanhToan>();
}
