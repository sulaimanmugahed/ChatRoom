namespace ChatRoom.Api.Domain.Models;


public class UserMessage
{
    public int Id { get; set; }

    public string SenderId { get; set; }

    public string RecipientId { get; set; }

    public string Content { get; set; }

    public DateTime Sent { get; set; }

    public AppUser Recipient { get; set; }

    public AppUser Sender { get; set; }


}
