using System;
using System.Collections.Generic;

namespace HeThongDatBan.Models;

public partial class LichSuTimKiem
{
    public int LichSuTimKiemId { get; set; }

    public int? NguoiDungId { get; set; }

    public string? TuKhoaTimKiem { get; set; }

    public DateTime? ThoiGianTimKiem { get; set; }

    public virtual NguoiDung? NguoiDung { get; set; }
}
