namespace ChatRoom.Api.Contracts.Interfaces;

public interface IUserConnectionService
{
    Task<List<string>> GetUserConnections(string userId);
    Task StartConnection(string userId, string connectionId, string userAgent);
    Task StopConnection(string connectionId);
}
