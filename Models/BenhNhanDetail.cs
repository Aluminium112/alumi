namespace Hospital.Models
{
	public class ToaThuoc1
	{
		public int Id { get; set; }

		public DateTime? Ngay { get; set; }

		public string ChanDoan { get; set; }

		public int? IdBacSi { get; set; }

		public int? IdBenhNhan { get; set; }

		public string TinhTrang { get; set; }

	}
	public class BenhNhanTT 
	{
        public int Id { get; set; }

        public int? IdThuoc { get; set; }

        public int? IdToaThuoc { get; set; }

        public byte? SoLuong { get; set; }

    }

}
