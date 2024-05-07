using Microsoft.EntityFrameworkCore;
using ChatRoom.Api.Contracts.Interfaces;
using ChatRoom.Api.Domain.Models;
using ChatRoom.Api.Infrastructure.Data;



namespace ChatRoom.Api.Infrastructure.Services;


public class UserConnectionService(ChatRoomDbContext context) : IUserConnectionService
{
    public async Task<List<string>> GetUserConnections(string userId)
    {
        return await context.UserConnections
            .Where(x => x.UserId == userId && x.Connected)
            .Select(x => x.ConnectionId)
            .ToListAsync();
    }


    public async Task StartConnection(string userId, string connectionId, string userAgent)
    {
        var connection = await context.UserConnections
              .SingleOrDefaultAsync(x => x.UserId == userId && x.UserAgent == userAgent);

        if (connection is not null)
        {
            connection.ConnectionId = connectionId;
            connection.Connected = true;
            connection.LastSeen = null;
            context.UserConnections.Update(connection);
        }
        else
        {
            await context.UserConnections.AddAsync(new UserConnection
            {
                UserId = userId,
                ConnectionId = connectionId,
                UserAgent = userAgent,
                Connected = true

            });
        }

        await context.SaveChangesAsync();

    }

    public async Task StopConnection(string connectionId)
    {
        var connection = await context.UserConnections.SingleOrDefaultAsync(a => a.ConnectionId == connectionId);
        if (connection is not null)
        {
            connection.Connected = false;
            connection.LastSeen = DateTime.UtcNow;
            await context.SaveChangesAsync();
        }
    }


}
