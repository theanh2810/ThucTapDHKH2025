using System;
using System.Collections.Generic;

namespace HeThongDatBan.Models;

public partial class User
{
    public int Id { get; set; }

    public string TaiKhoan { get; set; } = null!;

    public string MatKhau { get; set; } = null!;

    public int? VaiTro { get; set; }

    public bool? TrangThai { get; set; }
}
