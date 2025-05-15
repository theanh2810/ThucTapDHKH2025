using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HeThongDatBan.Models;

public partial class QuanLyHeThongDatBanContext : DbContext
{
    public QuanLyHeThongDatBanContext()
    {
    }

    public QuanLyHeThongDatBanContext(DbContextOptions<QuanLyHeThongDatBanContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DanhGium> DanhGia { get; set; }

    public virtual DbSet<DatBan> DatBans { get; set; }

    public virtual DbSet<HinhAnh> HinhAnhs { get; set; }

    public virtual DbSet<KhuyenMai> KhuyenMais { get; set; }

    public virtual DbSet<LichSuTimKiem> LichSuTimKiems { get; set; }

    public virtual DbSet<NguoiDung> NguoiDungs { get; set; }

    public virtual DbSet<NhaHang> NhaHangs { get; set; }

    public virtual DbSet<ThanhToan> ThanhToans { get; set; }

    public virtual DbSet<ThucDon> ThucDons { get; set; }

    public virtual DbSet<TinNhan> TinNhans { get; set; }

    public virtual DbSet<TinTuc> TinTucs { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DanhGium>(entity =>
        {
            entity.HasKey(e => e.DanhGiaId).HasName("PK__DanhGia__DAC05D0B69173888");

            entity.Property(e => e.DanhGiaId).HasColumnName("DanhGia_id");
            entity.Property(e => e.NgayDanhGia)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NguoiDungId).HasColumnName("NguoiDung_id");
            entity.Property(e => e.NhaHangId).HasColumnName("NhaHang_id");
            entity.Property(e => e.NhanXet).HasMaxLength(1000);

            entity.HasOne(d => d.NguoiDung).WithMany(p => p.DanhGia)
                .HasForeignKey(d => d.NguoiDungId)
                .HasConstraintName("FK__DanhGia__NguoiDu__6383C8BA");

            entity.HasOne(d => d.NhaHang).WithMany(p => p.DanhGia)
                .HasForeignKey(d => d.NhaHangId)
                .HasConstraintName("FK__DanhGia__NhaHang__6477ECF3");
        });

        modelBuilder.Entity<DatBan>(entity =>
        {
            entity.HasKey(e => e.DatBanId).HasName("PK__DatBan__74F2F8DB4D480B96");

            entity.ToTable("DatBan");

            entity.Property(e => e.DatBanId).HasColumnName("DatBan_id");
            entity.Property(e => e.GhiChu).HasMaxLength(500);
            entity.Property(e => e.KhachHangId).HasColumnName("KhachHang_id");
            entity.Property(e => e.NgayDat).HasColumnType("datetime");
            entity.Property(e => e.NhaHangId).HasColumnName("NhaHang_id");

            entity.HasOne(d => d.KhachHang).WithMany(p => p.DatBans)
                .HasForeignKey(d => d.KhachHangId)
                .HasConstraintName("FK__DatBan__KhachHan__5BE2A6F2");

            entity.HasOne(d => d.NhaHang).WithMany(p => p.DatBans)
                .HasForeignKey(d => d.NhaHangId)
                .HasConstraintName("FK__DatBan__NhaHang___5CD6CB2B");
        });

        modelBuilder.Entity<HinhAnh>(entity =>
        {
            entity.HasKey(e => e.HinhAnhId).HasName("PK__HinhAnh__66C823F73ED705C7");

            entity.ToTable("HinhAnh");

            entity.Property(e => e.HinhAnhId).HasColumnName("HinhAnh_id");
            entity.Property(e => e.MoTa).HasMaxLength(1000);
            entity.Property(e => e.NgayTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UrlHinhAnh).HasMaxLength(500);
        });

        modelBuilder.Entity<KhuyenMai>(entity =>
        {
            entity.HasKey(e => e.KhuyenMaiId).HasName("PK__KhuyenMa__4B5EE7B5FCA2313A");

            entity.ToTable("KhuyenMai");

            entity.Property(e => e.KhuyenMaiId).HasColumnName("KhuyenMai_id");
            entity.Property(e => e.MoTa).HasMaxLength(1000);
            entity.Property(e => e.NgayBatDau).HasColumnType("datetime");
            entity.Property(e => e.NgayCapNhat).HasColumnType("datetime");
            entity.Property(e => e.NgayDang)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NgayKetThuc).HasColumnType("datetime");
            entity.Property(e => e.NhaHangId).HasColumnName("NhaHang_id");
            entity.Property(e => e.TenKhuyenMai).HasMaxLength(255);

            entity.HasOne(d => d.NhaHang).WithMany(p => p.KhuyenMais)
                .HasForeignKey(d => d.NhaHangId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__KhuyenMai__NhaHa__75A278F5");
        });

        modelBuilder.Entity<LichSuTimKiem>(entity =>
        {
            entity.HasKey(e => e.LichSuTimKiemId).HasName("PK__LichSuTi__2792AC1D6FEA08EB");

            entity.ToTable("LichSuTimKiem");

            entity.Property(e => e.LichSuTimKiemId).HasColumnName("LichSuTimKiem_id");
            entity.Property(e => e.NguoiDungId).HasColumnName("NguoiDung_id");
            entity.Property(e => e.ThoiGianTimKiem)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TuKhoaTimKiem).HasMaxLength(255);

            entity.HasOne(d => d.NguoiDung).WithMany(p => p.LichSuTimKiems)
                .HasForeignKey(d => d.NguoiDungId)
                .HasConstraintName("FK__LichSuTim__Nguoi__5812160E");
        });

        modelBuilder.Entity<NguoiDung>(entity =>
        {
            entity.HasKey(e => e.NguoiDungId).HasName("PK__NguoiDun__12629C26CEB5A706");

            entity.ToTable("NguoiDung");

            entity.HasIndex(e => e.SoDienThoai, "UQ__NguoiDun__0389B7BD02B6E7D8").IsUnique();

            entity.HasIndex(e => e.TenDangNhap, "UQ__NguoiDun__55F68FC08C409915").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__NguoiDun__A9D10534E7F6574B").IsUnique();

            entity.Property(e => e.NguoiDungId).HasColumnName("NguoiDung_id");
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.HoTen).HasMaxLength(100);
            entity.Property(e => e.MatKhau).HasMaxLength(255);
            entity.Property(e => e.NgayTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.SoDienThoai).HasMaxLength(15);
            entity.Property(e => e.TenDangNhap).HasMaxLength(100);
        });

        modelBuilder.Entity<NhaHang>(entity =>
        {
            entity.HasKey(e => e.NhaHangId).HasName("PK__NhaHang__F0AFC6E39282748A");

            entity.ToTable("NhaHang");

            entity.Property(e => e.NhaHangId).HasColumnName("NhaHang_id");
            entity.Property(e => e.DiaChi).HasMaxLength(300);
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.LoaiAmThuc).HasMaxLength(100);
            entity.Property(e => e.MoTa).HasMaxLength(1000);
            entity.Property(e => e.NgayCapNhat)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NgayTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NguoiDungId).HasColumnName("NguoiDung_id");
            entity.Property(e => e.SoDienThoai).HasMaxLength(15);
            entity.Property(e => e.TenNhaHang).HasMaxLength(200);

            entity.HasOne(d => d.NguoiDung).WithMany(p => p.NhaHangs)
                .HasForeignKey(d => d.NguoiDungId)
                .HasConstraintName("FK__NhaHang__NguoiDu__4F7CD00D");
        });

        modelBuilder.Entity<ThanhToan>(entity =>
        {
            entity.HasKey(e => e.ThanhToanId).HasName("PK__ThanhToa__781C42BB6B77537B");

            entity.ToTable("ThanhToan");

            entity.Property(e => e.ThanhToanId).HasColumnName("ThanhToan_id");
            entity.Property(e => e.DatBanId).HasColumnName("DatBan_id");
            entity.Property(e => e.NguoiDungId).HasColumnName("NguoiDung_id");
            entity.Property(e => e.PhuongThucThanhToan).HasMaxLength(100);

            entity.HasOne(d => d.DatBan).WithMany(p => p.ThanhToans)
                .HasForeignKey(d => d.DatBanId)
                .HasConstraintName("FK__ThanhToan__DatBa__60A75C0F");

            entity.HasOne(d => d.NguoiDung).WithMany(p => p.ThanhToans)
                .HasForeignKey(d => d.NguoiDungId)
                .HasConstraintName("FK__ThanhToan__Nguoi__5FB337D6");
        });

        modelBuilder.Entity<ThucDon>(entity =>
        {
            entity.HasKey(e => e.ThucDonId).HasName("PK__ThucDon__D80353AD55798D3E");

            entity.ToTable("ThucDon");

            entity.Property(e => e.ThucDonId).HasColumnName("ThucDon_id");
            entity.Property(e => e.NgayTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NhaHangId).HasColumnName("NhaHang_id");

            entity.HasOne(d => d.NhaHang).WithMany(p => p.ThucDons)
                .HasForeignKey(d => d.NhaHangId)
                .HasConstraintName("FK__ThucDon__NhaHang__5441852A");
        });

        modelBuilder.Entity<TinNhan>(entity =>
        {
            entity.HasKey(e => e.TinNhanId).HasName("PK__TinNhan__E0F35048E026D457");

            entity.ToTable("TinNhan");

            entity.Property(e => e.TinNhanId).HasColumnName("TinNhan_id");
            entity.Property(e => e.NguoiGuiId).HasColumnName("NguoiGui_id");
            entity.Property(e => e.NguoiNhanId).HasColumnName("NguoiNhan_id");
            entity.Property(e => e.NoiDungTinNhan).HasMaxLength(2000);
            entity.Property(e => e.ThoiGianGui)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.NguoiGui).WithMany(p => p.TinNhanNguoiGuis)
                .HasForeignKey(d => d.NguoiGuiId)
                .HasConstraintName("FK__TinNhan__NguoiGu__693CA210");

            entity.HasOne(d => d.NguoiNhan).WithMany(p => p.TinNhanNguoiNhans)
                .HasForeignKey(d => d.NguoiNhanId)
                .HasConstraintName("FK__TinNhan__NguoiNh__6A30C649");
        });

        modelBuilder.Entity<TinTuc>(entity =>
        {
            entity.HasKey(e => e.TinTucId).HasName("PK__TinTuc__70EC13222D09FA0F");

            entity.ToTable("TinTuc");

            entity.Property(e => e.TinTucId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("TinTuc_id");
            entity.Property(e => e.LoaiTin).HasMaxLength(50);
            entity.Property(e => e.NgayCapNhat)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NgayDang)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TieuDe).HasMaxLength(255);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC27F5C149ED");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.MatKhau)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TaiKhoan)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TrangThai).HasDefaultValue(true);
            entity.Property(e => e.VaiTro).HasDefaultValue(0);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
