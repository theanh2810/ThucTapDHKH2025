using System;
using System.Collections.Generic;

namespace HeThongDatBan.Models;

public partial class TinNhan
{
    public int TinNhanId { get; set; }

    public int? NguoiGuiId { get; set; }

    public int? NguoiNhanId { get; set; }

    public string? NoiDungTinNhan { get; set; }

    public DateTime? ThoiGianGui { get; set; }

    public int? TrangThai { get; set; }

    public virtual NguoiDung? NguoiGui { get; set; }

    public virtual NguoiDung? NguoiNhan { get; set; }
}
