using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ChatRoom.Api.Domain.Models;

namespace ChatRoom.Api.Infrastructure.Data.Configurations;

public class UserRoomConfig : IEntityTypeConfiguration<UserRoom>
{
    public void Configure(EntityTypeBuilder<UserRoom> builder)
    {
        builder.ToTable("UserRooms");
        builder.HasKey(k => new { k.UserId, k.RoomId });

        builder.HasOne(x => x.User)
        .WithMany(u => u.UserRooms)
        .HasForeignKey(a => a.UserId);

        builder.HasOne(x => x.Room)
        .WithMany(a => a.UserRooms)
        .HasForeignKey(a => a.RoomId);
    }
}
