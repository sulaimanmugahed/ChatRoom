namespace ChatRoom.Api.Domain.Models;

public class UserConnection
{
    public int Id { set; get; }
    public string ConnectionId { get; set; }
    public string UserId { get; set; }
    public string UserAgent { get; set; }
    public bool Connected { get; set; }
    public AppUser User { get; set; }
    public DateTime? LastSeen { get; set; }
}
