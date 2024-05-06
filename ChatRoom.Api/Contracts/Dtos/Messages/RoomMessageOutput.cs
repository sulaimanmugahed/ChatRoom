using ChatRoom.Api.Domain.Models;

namespace ChatRoom.Api.Contracts.Dtos.Messages;

public class RoomMessageOutput
{
    public string Sender { get; set; }
    public DateTime Sent { get; set; }
    public string Content { get; set; }
    public string Room { get; set; }
}
