using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ChatRoom.Api.Contracts.Interfaces;
using ChatRoom.Api.Domain.Constants;
using ChatRoom.Api.Domain.Models;
using ChatRoom.Api.Hubs;
using System.Security.Claims;

namespace ChatRoom.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class RoomsController(
	IRoomService roomService,
	ICurrentAuthUserService currentAuthUser
	) : ControllerBase
{

	[HttpPost(nameof(Create))]
	public async Task<IActionResult> Create(string roomName)
	{
		var newRoom = new Domain.Models.Room { Id = Guid.NewGuid().ToString(),Name = roomName };
		newRoom.UserRooms ??= new List<UserRoom>();

		newRoom.UserRooms.Add(new UserRoom
		{
			UserId = currentAuthUser.UserId,
			MemberRole = RoomMemberRoles.Admin

		});

		await roomService.CreateRoom(newRoom);
		return Ok(newRoom);
	}

}
