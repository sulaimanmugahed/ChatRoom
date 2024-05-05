namespace ChatRoom.Api.Domain.Models;

public class UserRoom
{
    public string RoomId { get; set; }
    public string UserId { get; set; }
    public string MemberRole { get; set; }
    public Room Room { get; set; }
    public AppUser User { get; set; }

}
