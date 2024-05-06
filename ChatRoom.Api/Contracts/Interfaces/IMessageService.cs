using ChatRoom.Api.Contracts.Dtos.Messages;

namespace ChatRoom.Api.Contracts.Interfaces;
public interface IMessageService
{
    Task<PrivateMessageOutput> CreatePrivateMessage(PrivateMessageInput input);
    Task<RoomMessageOutput> CreateRoomMessage(RoomMessageInput input);
    Task<List<PrivateMessageOutput>> GetPrivateMessage(string sender, string recever);
    Task<List<RoomMessageOutput>> GetRoomMessage(string roomId);
}