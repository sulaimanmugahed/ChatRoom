using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ChatRoom.Api.Domain.Models;

namespace ChatRoom.Api.Infrastructure.Data.Configurations;

public class UserConnectionConfig : IEntityTypeConfiguration<UserConnection>
{
    public void Configure(EntityTypeBuilder<UserConnection> builder)
    {
        builder.ToTable("UserConnections");
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.User)
        .WithMany(u => u.UserConnections)
        .HasForeignKey(a => a.UserId);
    }
}
