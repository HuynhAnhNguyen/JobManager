using Microsoft.AspNetCore.Identity;

namespace JobManager.Models
{
    public class NguoiDung : IdentityUser
    {
        public string? HoNguoiDung { get; set; }
        public string? TenNguoiDung { get; set; }

        public DateTime NgaySinh { get; set; } = DateTime.Now;

        public string? GioiTinh { get; set; }

        public string? DiaChi { get; set; }

        public int DisableAccount { get; set; }

        public virtual ICollection<NguoiDung_CongViec> NguoiDungCongViec { get; set; }
    }
}
