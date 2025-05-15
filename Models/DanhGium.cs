using System;
using System.Collections.Generic;

namespace HeThongDatBan.Models;

public partial class DanhGium
{
    public int DanhGiaId { get; set; }

    public int? NguoiDungId { get; set; }

    public int? NhaHangId { get; set; }

    public int? SoSao { get; set; }

    public string? NhanXet { get; set; }

    public DateTime? NgayDanhGia { get; set; }

    public virtual NguoiDung? NguoiDung { get; set; }

    public virtual NhaHang? NhaHang { get; set; }
}
