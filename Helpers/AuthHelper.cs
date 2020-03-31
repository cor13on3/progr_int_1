using Lost.Models;
using System.Linq;
using System.Security.Claims;

namespace Lost.Helpers
{
    public static class AuthHelper
    {
        public static bool IsLogged(ClaimsPrincipal user)
        {
            return user.Identity.IsAuthenticated;
        }

        public static string GetEmail(ClaimsPrincipal user)
        {
            return user.Claims.FirstOrDefault(x => x.Type == "Email")?.Value;
        }

        public static bool CzyModerator(ClaimsPrincipal user)
        {
            return user.Claims.FirstOrDefault(x => x.Type == "Role")?.Value == RolaUzytkownika.Moderator.ToString();
        }
    }
}
