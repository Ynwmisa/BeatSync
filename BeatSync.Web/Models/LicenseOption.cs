namespace BeatSync.Web.Models;

public class LicenseOption
{
    public int Id { get; set; }
    public int BeatId { get; set; }
    public LicenseType LicenseType { get; set; }
    public decimal Price { get; set; }
    public string? Terms { get; set; }

    public Beat? Beat { get; set; }
}


