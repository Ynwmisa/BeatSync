namespace BeatSync.Web.Models;

public class Beat
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Genre { get; set; }
    public int? Bpm { get; set; }
    public string? Key { get; set; }
    public string? Mood { get; set; }
    public string? Instruments { get; set; }
    public string AudioFilePath { get; set; } = string.Empty;
    public string? CoverImagePath { get; set; }
    public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

    public ICollection<LicenseOption> LicenseOptions { get; set; } = new List<LicenseOption>();
}


