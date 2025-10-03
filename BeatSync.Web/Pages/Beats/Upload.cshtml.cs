using System.ComponentModel.DataAnnotations;
using BeatSync.Web.Data;
using BeatSync.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BeatSync.Web.Pages.Beats;

[Authorize(Roles = IdentitySeed.RoleProducer)]
public class UploadModel : PageModel
{
    private readonly ApplicationDbContext _db;
    private readonly IWebHostEnvironment _env;

    public UploadModel(ApplicationDbContext db, IWebHostEnvironment env)
    {
        _db = db;
        _env = env;
    }

    [BindProperty]
    public InputModel Input { get; set; } = new();

    [BindProperty]
    [Display(Name = "Audio File")]
    [Required]
    public IFormFile? AudioFile { get; set; }

    [BindProperty]
    [Display(Name = "Cover Image")]
    public IFormFile? CoverImage { get; set; }

    public class InputModel
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        public string? Genre { get; set; }
        public int? Bpm { get; set; }
        public string? Key { get; set; }
        public string? Mood { get; set; }
        public string? Instruments { get; set; }
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid || AudioFile == null)
        {
            return Page();
        }

        var uploadsRoot = Path.Combine(_env.WebRootPath, "uploads");
        Directory.CreateDirectory(uploadsRoot);

        string audioFileName = $"{Guid.NewGuid()}{Path.GetExtension(AudioFile.FileName)}";
        string audioSavePath = Path.Combine(uploadsRoot, audioFileName);
        using (var stream = System.IO.File.Create(audioSavePath))
        {
            await AudioFile.CopyToAsync(stream);
        }

        string? coverPath = null;
        if (CoverImage != null)
        {
            string coverFileName = $"{Guid.NewGuid()}{Path.GetExtension(CoverImage.FileName)}";
            string coverSavePath = Path.Combine(uploadsRoot, coverFileName);
            using (var stream = System.IO.File.Create(coverSavePath))
            {
                await CoverImage.CopyToAsync(stream);
            }
            coverPath = $"/uploads/{coverFileName}";
        }

        var beat = new Beat
        {
            Title = Input.Title,
            Genre = Input.Genre,
            Bpm = Input.Bpm,
            Key = Input.Key,
            Mood = Input.Mood,
            Instruments = Input.Instruments,
            AudioFilePath = $"/uploads/{audioFileName}",
            CoverImagePath = coverPath
        };

        _db.Beats.Add(beat);
        await _db.SaveChangesAsync();

        return RedirectToPage("/Beats/Index");
    }
}
