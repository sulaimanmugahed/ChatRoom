using ChatRoom.Api.Contracts.Interfaces;
using ChatRoom.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ChatRoom.Api.Infrastructure.Services;

public class UserService(ChatRoomDbContext context) : IUserService
{
	public async Task<bool> IsUserExist(string userId)
	{
		return await context.Users.AnyAsync(u => u.Id == userId);
	}
}
