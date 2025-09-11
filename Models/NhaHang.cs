using System;
using System.Collections.Generic;

namespace HeThongDatBan.Models;

public partial class NhaHang
{
    public int NhaHangId { get; set; }

    public int? NguoiDungId { get; set; }

    public string TenNhaHang { get; set; } = null!;

    public string DiaChi { get; set; } = null!;

    public string? SoDienThoai { get; set; }

    public string? Email { get; set; }

    public string? LoaiAmThuc { get; set; }

    public string? HinhAnh { get; set; }

    public string? MoTa { get; set; }

    public double? TienCoc { get; set; }

    public TimeOnly? GioMoCua { get; set; }

    public TimeOnly? GioDongCua { get; set; }

    public int? TrangThai { get; set; }

    public DateTime? NgayTao { get; set; }

    public DateTime? NgayCapNhat { get; set; }

    public virtual ICollection<DanhGium> DanhGia { get; set; } = new List<DanhGium>();

    public virtual ICollection<DatBan> DatBans { get; set; } = new List<DatBan>();

    public virtual ICollection<KhuyenMai> KhuyenMais { get; set; } = new List<KhuyenMai>();

    public virtual NguoiDung? NguoiDung { get; set; }

    public virtual ICollection<ThucDon> ThucDons { get; set; } = new List<ThucDon>();
}

public partial class DangKyNhaHangRequest
{
    public string TenNhaHang { get; set; } = null!;

    public string DiaChi { get; set; } = null!;

    public string? SoDienThoai { get; set; }

    public string? Email { get; set; }

    public string? LoaiAmThuc { get; set; }

    //public string? HinhAnh { get; set; }

    public string? MoTa { get; set; }

    public double? TienCoc { get; set; }

    public TimeOnly? GioMoCua { get; set; }

    public TimeOnly? GioDongCua { get; set; }

}

public partial class TestDatBan
{
    public int ID { get; set; }
    public bool TrangThai { get; set; }
    public DateTime ThoiGianDat { get; set; }
}
