namespace ChatRoom.Api.Contracts.Dtos.Account;

public class AuthenticationRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}
