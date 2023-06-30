namespace JobManager.Models
{
    public class NguoiDung_CongViec
    {
        public string? MaNguoiDungCV { get; set; }

        public string? NguoiDungId { get; set; }

        public string? MaCongViec { get; set; }

        public int TrangThai { get; set; }

        public virtual CongViec? CongViec { get; set; }

        public virtual NguoiDung? NguoiDung { get; set; }
    }
}
