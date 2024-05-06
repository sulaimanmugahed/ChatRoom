
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ChatRoom.Api.Contracts.Dtos.Account;

public class RegistrationRequest
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }

}
