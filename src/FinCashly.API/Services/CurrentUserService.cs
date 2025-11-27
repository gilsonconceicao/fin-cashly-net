using System.Security.Claims;
using FinCashly.Application.Common.Interfaces;

namespace FinCashly.API.Services;
#nullable disable
public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor accessor)
    {
        _httpContextAccessor = accessor;
    }

    public string UserId =>
        _httpContextAccessor.HttpContext?.User.Claims?.FirstOrDefault(c => c.Type == "uid").Value;

    public string Email =>
        _httpContextAccessor.HttpContext?.User.Claims?.FirstOrDefault(c => c.Type == "email").Value;

    public bool IsAuthenticated =>
        _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
    public IEnumerable<string> Roles =>
        _httpContextAccessor.HttpContext?.User?.Claims
            .Where(c => c.Type == ClaimTypes.Role)
            .Select(c => c.Value);
}
