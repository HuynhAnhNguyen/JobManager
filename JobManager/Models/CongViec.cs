namespace JobManager.Models
{
    public class CongViec
    {
        public string? MaCongViec { get; set; }

        public string? TenCongViec { get; set; }

        public string? MoTaCongViec { get; set; }

        public DateTime? NgayBatDau { get; set; }

        public DateTime? NgayKetThuc { get; set; }

        public int? TrangThai { get; set; }

        public int? MucDoUuTien { get; set; }

        public string? MaDuAn { get; set; }

        public bool? Deleted { get; set; } = false;

        public DateTime? NgayTaoCongViec { get; set; } = DateTime.Now;

        public virtual ICollection<CongViec_TaiLieuCV> CongViecTaiLieuCV { get; set; }

        public virtual DuAn? DuAn { get; set; }

        public virtual ICollection<NguoiDung_CongViec> NguoiDungCongViec { get; set; }
    }
}
