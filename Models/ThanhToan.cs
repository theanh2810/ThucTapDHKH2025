using System;
using System.Collections.Generic;

namespace HeThongDatBan.Models;

public partial class ThanhToan
{
    public int ThanhToanId { get; set; }

    public int? NguoiDungId { get; set; }

    public int? DatBanId { get; set; }

    public double SoTien { get; set; }

    public string? PhuongThucThanhToan { get; set; }

    public int? TrangThai { get; set; }

    public virtual DatBan? DatBan { get; set; }

    public virtual NguoiDung? NguoiDung { get; set; }
}
