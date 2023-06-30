namespace JobManager.Models
{
    public class DuAn
    {
        public string? MaDuAn { get; set; }

        public string? TenDuAn { get; set; }

        public string? MoTaDuAn { get; set; }

        public DateTime? NgayBatDau { get; set; }

        public DateTime? NgayKetThuc { get; set; }

        public int? TrangThai { get; set; }

        public DateTime? NgayTaoDuAn { get; set; }

        public bool? Deleted { get; set; } = false;

        public virtual ICollection<TaiLieu> TaiLieu { get; set; }
        public virtual ICollection<CongViec> CongViec { get; set; }
    }
}
