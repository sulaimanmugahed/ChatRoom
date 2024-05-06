namespace ChatRoom.Api.Contracts.Dtos.Messages;


public class RoomMessageInput
{
    public string Sender { get; set; }
    public string Room { get; set; }
    public string Content { get; set; }

}
