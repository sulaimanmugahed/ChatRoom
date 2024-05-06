using Microsoft.EntityFrameworkCore;
using ChatRoom.Api.Contracts.Dtos;
using ChatRoom.Api.Contracts.Interfaces;
using ChatRoom.Api.Domain.Constants;
using ChatRoom.Api.Infrastructure.Data;
using ChatRoom.Api.Domain.Models;


namespace ChatRoom.Api.Infrastructure.Services;

public class RoomService(RoomDbContext context) : IRoomService
{

    public async Task<List<string>> GetUserRooms(string userId)
    {
        return await context.UserRooms.Where(x => x.UserId == userId)
            .Select(x => x.RoomId.ToString()).ToListAsync();
    }

    public async Task<string?> GetUserRoom(string userId, string roomId)
    {
        return await context.UserRooms.Where(x => x.UserId == userId && x.RoomId.ToString() == roomId)
            .Select(x => x.RoomId.ToString()).FirstOrDefaultAsync();
    }


    public async Task<bool> IsUserInRoom(string userId, string roomId)
    {
        return await context.UserRooms.AnyAsync(x => x.RoomId.ToString() == roomId && x.UserId == userId);
    }

    public async Task<Room?> GetRoom(string id)
    {
      return  await context.Rooms.SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Room>> GetAllRoom()
    {
        return await context.Rooms.ToListAsync();
    } 

    public async Task CreateRoom(Room room)
    {
        await context.Rooms.AddAsync(room);
        await context.SaveChangesAsync();
    }

    public async Task<string> GetRoomOwner(string roomId)
    {
        var owner = await context.UserRooms
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.RoomId == roomId && x.MemberRole == RoomMemberRoles.Admin);

        return owner!.User.Name;
    }

    public async Task AddUserToRoom(string userId, string roomId, string memberRole = RoomMemberRoles.Member)
    {
        var user = context.Users.Include(x=> x.UserRooms).FirstOrDefault(x => x.Id == userId);
        if (user is not null)
        {
			await context.UserRooms.AddAsync(new UserRoom
			{
				RoomId = roomId,
				UserId = user.Id,
				MemberRole = memberRole
			});

			await context.SaveChangesAsync();
		}

	}

    public async Task RemoveUserFromRoom(string userId, string roomId)
    {
        var userRoom = await context.UserRooms.FirstOrDefaultAsync(x => x.UserId == userId && x.RoomId.ToString() == roomId);
        context.UserRooms.Remove(userRoom);

        await context.SaveChangesAsync();
    }


}
