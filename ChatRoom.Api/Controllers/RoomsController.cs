using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ChatRoom.Api.Contracts.Interfaces;
using ChatRoom.Api.Domain.Constants;
using ChatRoom.Api.Domain.Models;
using ChatRoom.Api.Hubs;
using System.Security.Claims;
using ChatRoom.Api.Contracts.Wrappers;
using ChatRoom.Api.Contracts.Dtos.Rooms;

namespace ChatRoom.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class RoomsController(
	IRoomService roomService,
	ICurrentAuthUserService currentAuthUser
	) : ControllerBase
{

	[HttpPost(nameof(Create))]
	public async Task<BaseResult<string>> Create(string roomName)
	{
		var newRoom = new Room
		{
			Id = Guid.NewGuid().ToString(),
			Name = roomName
		};

		newRoom.UserRooms ??= new List<UserRoom>();

		newRoom.UserRooms.Add(new UserRoom
		{
			UserId = currentAuthUser.UserId,
			MemberRole = RoomMemberRoles.Admin

		});

		await roomService.CreateRoom(newRoom);

		return new BaseResult<string>(newRoom.Id);
	}


	[HttpGet(nameof(GetRoomList))]
	public async Task<BaseResult<List<RoomDto>>> GetRoomList()
	{
		var rooms = await roomService.GetAllRoom();

		return new BaseResult<List<RoomDto>>(rooms.Select(room => new RoomDto
		{
			Id = room.Id,
			Name = room.Name,
		})
		.ToList());
	}


	[HttpGet(nameof(GetRoom))]
	public async Task<BaseResult<RoomDto>> GetRoom(string roomId)
	{
		var room = await roomService.GetRoom(roomId);
		if (room is null)
			return new BaseResult<RoomDto>(new Error(ErrorCode.NotFound, "The Room with this id not found !"));

		return new BaseResult<RoomDto>(new RoomDto
		{
			Id = room.Id,
			Name = room.Name,
		});
	}


	[HttpGet(nameof(GetRoomDetail))]
	public async Task<BaseResult<RoomDetailDto>> GetRoomDetail(string roomId)
	{
		var room = await roomService.GetRoom(roomId);
		if (room is null)
			return new BaseResult<RoomDetailDto>(new Error(ErrorCode.NotFound, "The Room with this id not found !"));

		return new BaseResult<RoomDetailDto>(new RoomDetailDto
		{
			Id = room.Id,
			Name = room.Name,
			Owner = await roomService.GetRoomOwner(room.Id)
		});
	}

}
