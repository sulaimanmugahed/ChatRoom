namespace ChatRoom.Api.Contracts.Interfaces;

public interface IChatClient
{
    Task AddPrivateChatMessage(object obj);
    Task AddRoomChatMessage(object obj);
    Task OnMemberLeaveRoom(string user, string room);
    Task OnMemberJoinRoom(string user, string room);
}
