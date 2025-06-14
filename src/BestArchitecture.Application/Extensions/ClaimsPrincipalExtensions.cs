using System.Security.Claims;

namespace BestArchitecture.Application.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        /// <summary>
        /// Dohvaća korisnički ID iz claimsa (NameIdentifier) i vraća null ako nije validan int.
        /// </summary>
        public static int? GetUserId(this ClaimsPrincipal user)
        {
            var id = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.TryParse(id, out var parsedId) ? parsedId : null;
        }
    }
}
