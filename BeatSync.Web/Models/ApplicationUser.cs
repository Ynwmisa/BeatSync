using Microsoft.AspNetCore.Identity;

namespace BeatSync.Web.Models;

public class ApplicationUser : IdentityUser
{
    public string? DisplayName { get; set; }
}


