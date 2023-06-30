namespace JobManager.Models
{
    public class TaiLieu
    {
        public string? MaTaiLieu { get; set; }

        public string? TenTaiLieu { get; set; }

        public string? MaDuAn { get; set; }

        public virtual DuAn? DuAn { get; set; }
    }
}
