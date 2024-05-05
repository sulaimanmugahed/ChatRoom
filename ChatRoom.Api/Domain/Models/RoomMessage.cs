namespace ChatRoom.Api.Domain.Models;

public class RoomMessage
{
    public int Id { get; set; }

    public string SenderId { get; set; }

    public string RoomId { get; set; }

    public string Content { get; set; }

    public Room? Room { get; set; }

    public AppUser Sender { get; set; }

    public DateTime Sent { get; set; }

}
