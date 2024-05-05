using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ChatRoom.Api.Domain.Models;

namespace ChatRoom.Api.Infrastructure.Data.Configurations;

public class UserMessageConfig : IEntityTypeConfiguration<UserMessage>
{
    public void Configure(EntityTypeBuilder<UserMessage> builder)
    {
        builder.ToTable("UserMessages");
        builder.HasKey(e => e.Id);

        builder.HasOne(d => d.Recipient).WithMany(p => p.UserMessageRecipients)
            .HasForeignKey(d => d.RecipientId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK__UserMessa__Recip__440B1D61");

        builder.HasOne(d => d.Sender).WithMany(p => p.UserMessageSenders)
            .HasForeignKey(d => d.SenderId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK__UserMessa__Sende__4316F928");
    }
}
