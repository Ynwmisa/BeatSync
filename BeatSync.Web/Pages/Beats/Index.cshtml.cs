using BeatSync.Web.Data;
using BeatSync.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BeatSync.Web.Pages.Beats;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _db;
    public IndexModel(ApplicationDbContext db) { _db = db; }

    public IReadOnlyList<Beat> Beats { get; set; } = Array.Empty<Beat>();

    [BindProperty(SupportsGet = true)]
    public FilterModel Filter { get; set; } = new();

    [BindProperty(SupportsGet = true)] public int Page { get; set; } = 1;
    [BindProperty(SupportsGet = true)] public int PageSize { get; set; } = 12;

    public class FilterModel
    {
        public string? Genre { get; set; }
        public string? Mood { get; set; }
        public string? Key { get; set; }
        public int? BpmMin { get; set; }
        public int? BpmMax { get; set; }
    }

    public async Task OnGetAsync()
    {
        IQueryable<Beat> query = _db.Beats.OrderByDescending(b => b.CreatedUtc);

        if (!string.IsNullOrWhiteSpace(Filter.Genre))
            query = query.Where(b => b.Genre != null && EF.Functions.Like(b.Genre, $"%{Filter.Genre}%"));
        if (!string.IsNullOrWhiteSpace(Filter.Mood))
            query = query.Where(b => b.Mood != null && EF.Functions.Like(b.Mood, $"%{Filter.Mood}%"));
        if (!string.IsNullOrWhiteSpace(Filter.Key))
            query = query.Where(b => b.Key != null && EF.Functions.Like(b.Key, $"%{Filter.Key}%"));
        if (Filter.BpmMin.HasValue)
            query = query.Where(b => b.Bpm >= Filter.BpmMin);
        if (Filter.BpmMax.HasValue)
            query = query.Where(b => b.Bpm <= Filter.BpmMax);

        query = query.Skip((Page - 1) * PageSize).Take(PageSize);
        Beats = await query.AsNoTracking().ToListAsync();
    }
}


