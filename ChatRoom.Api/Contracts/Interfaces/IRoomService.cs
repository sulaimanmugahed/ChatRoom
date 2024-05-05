using ChatRoom.Api.Domain.Constants;

namespace ChatRoom.Api.Contracts.Interfaces;

public interface IRoomService
{
    Task AddUserToRoom(string userId, string roomId, string memberRole = RoomMemberRoles.Member);
    Task<string?> GetUserRoom(string userId, string roomId);
    Task<List<string>> GetUserRooms(string userId);
    Task<bool> IsUserInRoom(string userId, string roomId);
    Task RemoveUserFromRoom(string userId, string roomId);
    Task CreateRoom(Domain.Models.Room room);
}