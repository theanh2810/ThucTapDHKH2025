using System;
using System.Collections.Generic;

namespace HeThongDatBan.Models;

public partial class KhuyenMai
{
    public int KhuyenMaiId { get; set; }

    public int NhaHangId { get; set; }

    public string TenKhuyenMai { get; set; } = null!;

    public string? MoTa { get; set; }

    public DateTime NgayBatDau { get; set; }

    public DateTime NgayKetThuc { get; set; }

    public DateTime? NgayDang { get; set; }

    public DateTime? NgayCapNhat { get; set; }

    public int? TrangThai { get; set; }

    public virtual NhaHang NhaHang { get; set; } = null!;
}
