namespace InallarEticaretWebService.Models
{
    public class EklenecekSiparis
    {
        public int? LogicalRef { get; set; }
        public string? TeklifNo { get; set; } = string.Empty;
        public string? FicheNo { get; set; } = string.Empty;
        public string? MusteriAdi { get; set; } = string.Empty;
        public string? TelefonNo { get; set; } = string.Empty;
        public string? UlkeKodu { get; set; } = string.Empty;
        public string? CYPHCODE { get; set; } = string.Empty;
        public string? SPECODE { get; set; } = string.Empty;
        public int ClientRef { get; set; }
        public int PaymentRef { get; set; }
        public double SepetToplamTutar { get; set; }
        public DateTime? KayitZamani { get; set; } = null;
        public int? OnayDurumu { get; set; }
        public DateTime? OnayZamani { get; set; } = null;
        public int? OrFicheRef { get; set; }
        public List<SepettekiUrun>? Sepet { get; set; }
    }

    public class SepettekiUrun
    {
        public int LogicalRef { get; set; }
        public string? UrunAdi { get; set; } = string.Empty;
        public string? UrunKodu { get; set; } = string.Empty;
        public string? Keyword1 { get; set; } = string.Empty;
        public int UserRef { get; set; }
        public int Adet { get; set; }
        public double BirimFiyat { get; set; }
        public double ToplamTutar { get; set; }
        public List<string> ImagePaths { get; set; }
        public int Currency { get; set; }

    }
}
