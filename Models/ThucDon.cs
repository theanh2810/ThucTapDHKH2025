using System;
using System.Collections.Generic;

namespace HeThongDatBan.Models;

public partial class ThucDon
{
    public int ThucDonId { get; set; }

    public int? NhaHangId { get; set; }

    public string? HinhAnh { get; set; }

    public DateTime? NgayTao { get; set; }

    public virtual NhaHang? NhaHang { get; set; }
}
