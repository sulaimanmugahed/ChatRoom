using Microsoft.AspNetCore.Identity;

namespace ChatRoom.Api.Domain.Models;

public class AppRole(string name) : IdentityRole(name)
{

}
