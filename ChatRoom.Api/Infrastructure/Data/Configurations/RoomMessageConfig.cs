using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ChatRoom.Api.Domain.Models;

namespace ChatRoom.Api.Infrastructure.Data.Configurations;

public class RoomMessageConfig : IEntityTypeConfiguration<RoomMessage>
{
    public void Configure(EntityTypeBuilder<RoomMessage> builder)
    {
        builder.ToTable("RoomMessage");
        builder.HasKey(e => e.Id);

        builder.HasOne(d => d.Sender).WithMany(p => p.RoomMessages)
            .HasForeignKey(d => d.SenderId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK__RoomMessa__Sender__440B1D61");

        builder.HasOne(d => d.Room).WithMany(p => p.RoomMessages)
            .HasForeignKey(d => d.RoomId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK__Room__Id__4316F928");
    }
}
