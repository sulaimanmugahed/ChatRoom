using Microsoft.EntityFrameworkCore;
using ChatRoom.Api.Contracts.Interfaces;
using ChatRoom.Api.Infrastructure.Data;
using ChatRoom.Api.Domain.Models;
using ChatRoom.Api.Contracts.Dtos.Messages;


namespace ChatRoom.Api.Infrastructure.Services;

public class MessageService(ChatRoomDbContext context) : IMessageService
{
    public async Task<List<PrivateMessageOutput>> GetPrivateMessage(string sender, string recever)
    {
        return await context.UserMessages
            .Where(x => x.SenderId == sender && x.RecipientId == recever || x.SenderId == recever && x.RecipientId == sender)
            .Select(x => new PrivateMessageOutput { Content = x.Content, Sent = x.Sent, Sender = x.SenderId })
            .ToListAsync();
    }


    public async Task<PrivateMessageOutput> CreatePrivateMessage(PrivateMessageInput input)
    {
        var message = new UserMessage
        {
            SenderId = input.Sender,
            RecipientId = input.Recever,
            Content = input.Content,
            Sent = DateTime.UtcNow

        };

        await context.UserMessages.AddAsync(message);
        await context.SaveChangesAsync();

        return new PrivateMessageOutput
        {
            Content = message.Content,
            Sender = message.SenderId,
            Sent = message.Sent

        };

    }


    public async Task<RoomMessageOutput> CreateRoomMessage(RoomMessageInput input)
    {
        var user = await context.Users.Include(x => x.UserRooms).SingleAsync(x => x.Id == input.Sender);
        var message = new RoomMessage
        {
            Sender = user,
            RoomId = input.Room,
            Content = input.Content,
            Sent = DateTime.UtcNow
        };

        await context.RoomMessages.AddAsync(message);
        await context.SaveChangesAsync();

        return new RoomMessageOutput
        {
            Sender = message.Sender.Name,
            Content = message.Content,
            Room = message.RoomId,
            Sent = message.Sent
        };
    }


    public async Task<List<RoomMessageOutput>> GetRoomMessage(string roomId)
    {
        return await context.RoomMessages
        .Include(x => x.Sender)
        .Where(a => a.RoomId.ToString() == roomId)
        .OrderBy(x => x.Sent)
        .Select(m => new RoomMessageOutput
        {
            Content = m.Content,
            Room = m.RoomId,
            Sent = m.Sent,
            Sender = m.Sender.Name
        })
        .ToListAsync();
    }
}
