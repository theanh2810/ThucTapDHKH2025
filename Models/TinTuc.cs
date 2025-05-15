using System;
using System.Collections.Generic;

namespace HeThongDatBan.Models;

public partial class TinTuc
{
    public Guid TinTucId { get; set; }

    public string TieuDe { get; set; } = null!;

    public string NoiDung { get; set; } = null!;

    public string? HinhAnh { get; set; }

    public string? LoaiTin { get; set; }

    public Guid NguoiDang { get; set; }

    public DateTime? NgayDang { get; set; }

    public DateTime? NgayCapNhat { get; set; }

    public int? TrangThai { get; set; }
}
