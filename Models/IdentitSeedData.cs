using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Portfolio_Strona.Models
{
    public class IdentitSeedData
    {
        private const string adminUser = "fake";
        private const string adminPassword = "fake";
        public static async Task EnsurePopulated(UserManager<IdentityUser> userManager)
        {
            IdentityUser user = await userManager.FindByIdAsync(adminUser);
        if (user == null)
            {
                user = new IdentityUser("fake");
                await userManager.CreateAsync(user, adminPassword);
            }
        }
    }
}
