using Microsoft.EntityFrameworkCore;

namespace ChatRoom.Api.Infrastructure.Data.Configurations;

public class RoomConfig : IEntityTypeConfiguration<Domain.Models.Room>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Domain.Models.Room> builder)
    {
        builder.ToTable("Rooms");
    }
}
