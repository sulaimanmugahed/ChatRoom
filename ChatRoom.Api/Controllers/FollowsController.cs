using ChatRoom.Api.Contracts.Dtos.Follows;
using ChatRoom.Api.Contracts.Interfaces;
using ChatRoom.Api.Contracts.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ChatRoom.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class FollowsController(
	IFollowService followService,
	ICurrentAuthUserService currentAuthUser,
	IUserService userService) : ControllerBase
{

	[HttpGet(nameof(GetAllFollowers)), Authorize]
	public async Task<BaseResult<List<FollowerDto>>> GetAllFollowers()
	=> new BaseResult<List<FollowerDto>>(await followService.GetAllFollowers(currentAuthUser.UserId));


	[HttpGet(nameof(GetAllFollowing)), Authorize]
	public async Task<BaseResult<List<FollowerDto>>> GetAllFollowing()
	=> new BaseResult<List<FollowerDto>>(await followService.GetAllFollowing(currentAuthUser.UserId));


	[HttpPost(nameof(Follow)),Authorize]
	public async Task<BaseResult> Follow(string userToFollow)
	{
		var isUserExist = await userService.IsUserExist(userToFollow);
		if (!isUserExist)
			return new BaseResult(new Error(ErrorCode.NotFound, "No User With This Id"));

		await followService.Follow(currentAuthUser.UserId, userToFollow);
		return new BaseResult();
	}

	[HttpDelete(nameof(UnFollow)), Authorize]
	public async Task<BaseResult> UnFollow(string userToUnFollow)
	{
		await followService.UnFollow(currentAuthUser.UserId, userToUnFollow);
		return new BaseResult();
	}

}
