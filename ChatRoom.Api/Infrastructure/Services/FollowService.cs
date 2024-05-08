using ChatRoom.Api.Contracts.Dtos.Follows;
using ChatRoom.Api.Contracts.Interfaces;
using ChatRoom.Api.Domain.Constants;
using ChatRoom.Api.Domain.Models;
using ChatRoom.Api.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChatRoom.Api.Infrastructure.Services;

public class FollowService(ChatRoomDbContext context) : IFollowService
{

	public async Task<List<FollowerDto>> GetAllFollowers(string currentUser)
	{
		return await context.Follows.Where(f => f.FollowingId == currentUser)
			.Select(f => new FollowerDto
			{
				Id = f.Follower.Id,
				Name = f.Follower.Name
			})
			.ToListAsync();
	}

	public async Task<List<FollowerDto>> GetAllFollowing(string currentUser)
	{
		return await context.Follows.Where(f => f.FollowerId == currentUser)
			.Select(f => new FollowerDto
			{
				Id = f.Following.Id,
				Name = f.Following.Name
			})
			.ToListAsync();
	}

	public async Task Follow(string currentUser, string userToFollow)
	{
		var follow = new Follow
		{
			FollowerId = currentUser,
			FollowingId = userToFollow,
		};

		if (!context.Follows.Contains(follow))
		{
			await context.AddAsync(follow);
			await context.SaveChangesAsync();
		}
	}


	public async Task<bool> IsFollowingMe(string currentUser, string userToCheck)
	{
		return await context.Follows.AnyAsync(x => x.FollowerId == userToCheck && x.FollowingId == currentUser);
	}

}
