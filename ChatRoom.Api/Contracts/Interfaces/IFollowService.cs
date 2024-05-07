using ChatRoom.Api.Contracts.Dtos.Follows;

namespace ChatRoom.Api.Contracts.Interfaces;
public interface IFollowService
{
    Task Follow(string currentUser, string userToFollow);
    Task<List<FollowerDto>> GetAllFollowers(string currentUser);
    Task<List<FollowerDto>> GetAllFollowing(string currentUser);
    Task<bool> IsFollowingMe(string currentUser, string userToCheck);
}