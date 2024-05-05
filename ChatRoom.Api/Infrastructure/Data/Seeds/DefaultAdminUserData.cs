using Microsoft.AspNetCore.Identity;
using ChatRoom.Api.Domain.Models;

namespace ChatRoom.Api.Infrastructure.Data.Seeds;

public class DefaultAdminUserData
{
	public static async Task SeedAsync(UserManager<AppUser> userManager)
	{

		var defaultUser = new AppUser
		{
			UserName = "Sulaiman",
			Email = "Sulaiman@gmail.com",
			Name = "Sulaiman",
			PhoneNumber = "00967773050577",
			EmailConfirmed = true,
			PhoneNumberConfirmed = true
		};

		if (userManager.Users.All(u => u.Id != defaultUser.Id))
		{
			var user = await userManager.FindByEmailAsync(defaultUser.Email);
			if (user == null)
			{
				await userManager.CreateAsync(defaultUser, "qweasd");
				await userManager.AddToRoleAsync(defaultUser, "Admin");
			}

		}
	}
}
