using BeatSync.Web.Data;
using BeatSync.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BeatSync.Web.Pages.Checkout;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _db;
    public IndexModel(ApplicationDbContext db) { _db = db; }

    [BindProperty(SupportsGet = true)] public int BeatId { get; set; }
    [BindProperty(SupportsGet = true)] public LicenseType LicenseType { get; set; }

    public Beat? Beat { get; private set; }
    public LicenseOption? License { get; private set; }

    public async Task<IActionResult> OnGetAsync()
    {
        Beat = await _db.Beats.Include(b => b.LicenseOptions)
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.Id == BeatId);
        if (Beat == null) return NotFound();
        License = Beat.LicenseOptions.FirstOrDefault(l => l.LicenseType == LicenseType);
        if (License == null) return NotFound();
        return Page();
    }
}


