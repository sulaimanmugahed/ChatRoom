namespace ChatRoom.Api.Domain.Models;

public class Follow
{
	public string FollowerId { get; set; }

	public string FollowingId { get; set; }

	public string? FollowStatus { get; set; }

	public AppUser Follower { get; set; }

	public AppUser Following { get; set; }
}
