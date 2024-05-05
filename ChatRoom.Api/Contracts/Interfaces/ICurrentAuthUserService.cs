namespace ChatRoom.Api.Contracts.Interfaces;

public interface ICurrentAuthUserService
{
    public string UserId { get; }
    public string UserName { get; }
}
