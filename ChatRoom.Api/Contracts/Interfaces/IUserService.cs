namespace ChatRoom.Api.Contracts.Interfaces;

public interface IUserService
{
    Task<bool> IsUserExist(string userId);
}