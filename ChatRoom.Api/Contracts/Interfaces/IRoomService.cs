using ChatRoom.Api.Domain.Constants;
using ChatRoom.Api.Domain.Models;

namespace ChatRoom.Api.Contracts.Interfaces;

public interface IRoomService
{
    Task AddUserToRoom(string userId, string roomId, string memberRole = RoomMemberRoles.Member);
    Task<string?> GetUserRoom(string userId, string roomId);
    Task<List<string>> GetUserRooms(string userId);
    Task<bool> IsUserInRoom(string userId, string roomId);
    Task RemoveUserFromRoom(string userId, string roomId);
    Task CreateRoom(Room room);
    Task<Room?> GetRoom(string id);
    Task<string> GetRoomOwner(string roomId);
    Task<List<Room>> GetAllRoom();
}