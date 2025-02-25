namespace CourseApp.Data
{
    public class Ogrenci
    {
        public int OgrenciId { get; set; }
        public string? OgrenciAd { get; set; }
        public string? OgrenciSoyad { get; set; }
        public string? AdSoyad => OgrenciAd + " " + OgrenciSoyad;

        public string? Eposta { get; set; }
        public string? Telefon { get; set; }
    }
}