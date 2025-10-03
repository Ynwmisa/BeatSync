using BeatSync.Web.Models;
using Microsoft.AspNetCore.Identity;

namespace BeatSync.Web.Data;

public static class IdentitySeed
{
    public const string RoleProducer = "Producer";
    public const string RoleArtist = "Artist";

    public static async Task EnsureSeededAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        foreach (var roleName in new[] { RoleProducer, RoleArtist })
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
    }
}


