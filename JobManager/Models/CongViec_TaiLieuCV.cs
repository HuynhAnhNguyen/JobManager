namespace JobManager.Models
{
    public class CongViec_TaiLieuCV
    {
        public string? MaCVTL { get; set; }

        public string? MaCongViec { get; set; }

        public string? MaTLCV { get; set; }

        public int? TrangThai { get; set; }

        public virtual CongViec? CongViec { get; set; }

        public virtual TaiLieuCV? TaiLieuCV { get; set; }
    }
}
