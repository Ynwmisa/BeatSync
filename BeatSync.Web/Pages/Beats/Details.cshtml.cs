using BeatSync.Web.Data;
using BeatSync.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BeatSync.Web.Pages.Beats;

public class DetailsModel : PageModel
{
    private readonly ApplicationDbContext _db;
    public DetailsModel(ApplicationDbContext db) { _db = db; }

    public Beat Beat { get; private set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var beat = await _db.Beats.Include(b => b.LicenseOptions)
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.Id == id);
        if (beat == null) return NotFound();
        Beat = beat;
        return Page();
    }
}


