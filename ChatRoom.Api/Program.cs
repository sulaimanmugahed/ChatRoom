using Microsoft.AspNetCore.Identity;
using ChatRoom.Api.Domain.Models;
using ChatRoom.Api.Hubs;
using ChatRoom.Api.Infrastructure.Data.Seeds;
using ChatRoom.Api.Infrastructure.Extensions;
using ChatRoom.Api.Infrastructure.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices(builder.Configuration);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseCors("Any");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<CustomGlobalErrorHandlerMiddleware>();

app.MapControllers();

app.MapHub<ChatHub>($"/chatHub");

// seeding data
using var scope = app.Services.CreateScope();
var scopedService = scope.ServiceProvider;
await DefaultAdminUserData.SeedAsync(scopedService.GetRequiredService<UserManager<AppUser>>());

app.Run();
