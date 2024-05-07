using Microsoft.AspNetCore.Identity;



namespace ChatRoom.Api.Domain.Models;

public class AppUser : IdentityUser
{
    public string Name { get; set; }
    public ICollection<UserConnection> UserConnections { get; set; }
    public ICollection<UserRoom> UserRooms { get; set; }
    public ICollection<UserMessage> UserMessageRecipients { get; set; }
    public ICollection<UserMessage> UserMessageSenders { get; set; }
    public ICollection<RoomMessage> RoomMessages { get; set; }
    public ICollection<Follow> Followers { get; set; }
    public ICollection<Follow> Following { get; set; }
     


}
