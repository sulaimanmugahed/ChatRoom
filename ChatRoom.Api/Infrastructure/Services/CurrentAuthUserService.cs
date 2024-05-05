using ChatRoom.Api.Contracts.Interfaces;
using System.Security.Claims;

namespace ChatRoom.Api.Infrastructure.Services;

public class CurrentAuthUserService(IHttpContextAccessor httpContextAccessor) : ICurrentAuthUserService
{
    public string UserId => httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier)!;
    public string UserName => httpContextAccessor.HttpContext?.User.Identity?.Name!;

}
