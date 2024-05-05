using ChatRoom.Api.Domain.Models;

namespace ChatRoom.Api.Contracts.Dtos;

public class PrivateMessageOutput
{
    public string Sender { get; set; }
    public string Content { get; set; }
    public DateTime Sent { get; set; }
}
