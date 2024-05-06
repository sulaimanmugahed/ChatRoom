namespace ChatRoom.Api.Contracts.Dtos.Messages;

public class PrivateMessageInput
{
    public string Sender { get; set; }
    public string Recever { get; set; }
    public string Content { get; set; }
}

