namespace ChatRoom.Api.Domain.Models;

public class Room
{
    public string Id { get; set; }
    public string Name { get; set; }

    public ICollection<RoomMessage> RoomMessages { get; set; }
    public ICollection<UserRoom> UserRooms { get; set; }
}
