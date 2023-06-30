namespace JobManager.Models
{
    public class TaiLieuCV
    {
        public string? MaTaiLieuCV { get; set; }

        public string? TenTaiLieuCV { get; set; }

        public virtual ICollection<CongViec_TaiLieuCV> CongViecTaiLieuCV { get; set; }
    }
}
