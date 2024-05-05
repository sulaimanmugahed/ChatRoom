using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ChatRoom.Api.Domain.Models;
using System.Reflection;
namespace ChatRoom.Api.Infrastructure.Data;

public class RoomDbContext(DbContextOptions<RoomDbContext> options)
    : IdentityDbContext<AppUser, AppRole, string>
    (options)
{

    public DbSet<UserConnection> UserConnections { get; set; }
    public DbSet<Domain.Models.Room> Rooms { get; set; }
    public DbSet<UserRoom> UserRooms { get; set; }
    public DbSet<UserMessage> UserMessages { get; set; }
    public DbSet<RoomMessage> RoomMessages { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);


        builder.Entity<IdentityUserRole<string>>(entity =>
        {
            entity.ToTable("UserRoles");
        });

        builder.Entity<IdentityUserClaim<string>>(entity =>
        {
            entity.ToTable("UserClaims");
        });

        builder.Entity<IdentityUserLogin<string>>(entity =>
        {
            entity.ToTable("UserLogins");
        });

        builder.Entity<IdentityRoleClaim<string>>(entity =>
        {
            entity.ToTable("RoleClaims");

        });

        builder.Entity<IdentityUserToken<string>>(entity =>
        {
            entity.ToTable("UserTokens");
        });


        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
