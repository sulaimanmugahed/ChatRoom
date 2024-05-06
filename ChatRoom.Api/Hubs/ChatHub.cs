using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using ChatRoom.Api.Contracts.Interfaces;
using ChatRoom.Api.Contracts.Dtos.Messages;

namespace ChatRoom.Api.Hubs;


[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ChatHub(
	IUserConnectionService connectionService,
	IRoomService roomService,
	IMessageService messageService)
	: Hub<IChatClient>
{

	public override async Task OnConnectedAsync()
	{
		var userId = Context.UserIdentifier ?? string.Empty;

		// add the new connectionId of user to his rooms
		var userRooms = await roomService.GetUserRooms(userId);
		if (userRooms is not null)
			foreach (var room in userRooms)
				await Groups.AddToGroupAsync(Context.ConnectionId, room);

		// add the connectionId with user agent to database or change connected to true if user exist
		var userAgent = Context.GetHttpContext()?.Request.Headers.UserAgent.FirstOrDefault() ?? string.Empty;
		await connectionService.StartConnection(userId, Context.ConnectionId, userAgent);

		await base.OnConnectedAsync();
	}


	public override async Task OnDisconnectedAsync(Exception? exception)
	{
		// set connected to false
		await connectionService.StopConnection(Context.ConnectionId);
		await base.OnDisconnectedAsync(exception);
	}


	public async Task<List<RoomMessageOutput>> LoadRoomMessage(string roomId)
	{
		var userId = Context.UserIdentifier ?? string.Empty;
		var isUserInRoom = await roomService.IsUserInRoom(userId, roomId);
		if (!isUserInRoom)
		{
			return [];
		}

		return await messageService.GetRoomMessage(roomId);

	}


	public async Task<List<PrivateMessageOutput>> LoadPrivateMessages(string recever)
	{
		var userId = Context.UserIdentifier ?? string.Empty;
		return await messageService.GetPrivateMessage(userId, recever);
	}



	public async Task SendPrivateChatMessage(PrivateMessageInput input)
	{
		input.Sender ??= Context.UserIdentifier ?? string.Empty;
		var newMessage = await messageService.CreatePrivateMessage(input);

		await Clients.Caller.AddPrivateChatMessage(newMessage);
		var connections = await connectionService.GetUserConnections(input.Recever);

		if (connections is not null)
			foreach (var connection in connections)
				await Clients.Client(connection)
					.AddPrivateChatMessage(newMessage);
	}


	public async Task SendRoomChatMessage(RoomMessageInput input)
	{
		input.Sender = Context.UserIdentifier!;
		var isUserInRoom = await roomService.IsUserInRoom(input.Sender, input.Room);
		if (isUserInRoom)
		{
			var newMessage = await messageService.CreateRoomMessage(input);
			await Clients.Group(input.Room).AddRoomChatMessage(newMessage);
		}

	}



	public async Task<List<RoomMessageOutput>> JoinRoom(string roomId)
	{
		var userId = Context.UserIdentifier ?? string.Empty;
		await roomService.AddUserToRoom(userId, roomId);
		await Groups.AddToGroupAsync(Context.ConnectionId, roomId);

		var userName = Context.User?.Identity?.Name ?? string.Empty;
		await NewMemberJoinRoom(userName, roomId);

		return await LoadRoomMessage(roomId);
	}



	public async Task LeaveRoom(string roomId)
	{
		var userId = Context.UserIdentifier ?? string.Empty;

		await roomService.RemoveUserFromRoom(userId, roomId);
		await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId);

		var userName = Context.User?.Identity?.Name ?? string.Empty;
		await MemberLeaveGroup(userName, roomId);
	}


	public async Task NewMemberJoinRoom(string user, string roomId)
	{
		await Clients.OthersInGroup(roomId).OnMemberJoinRoom(user, roomId);
	}

	public async Task MemberLeaveGroup(string user, string roomId)
	{
		await Clients.OthersInGroup(roomId).OnMemberLeaveRoom(user, roomId);
	}


}