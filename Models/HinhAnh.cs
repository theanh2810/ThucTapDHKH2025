using System;
using System.Collections.Generic;

namespace HeThongDatBan.Models;

public partial class HinhAnh
{
    public int HinhAnhId { get; set; }

    public string UrlHinhAnh { get; set; } = null!;

    public string? MoTa { get; set; }

    public DateTime? NgayTao { get; set; }

    public int? LoaiDoiTuong { get; set; }
}
