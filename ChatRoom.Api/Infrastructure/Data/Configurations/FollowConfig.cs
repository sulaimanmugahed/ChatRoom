using ChatRoom.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatRoom.Api.Infrastructure.Data.Configurations;

public class FollowConfig : IEntityTypeConfiguration<Follow>
{
	public void Configure(EntityTypeBuilder<Follow> builder)
	{
		builder.ToTable("Follows");

		builder.HasKey(e => new { e.FollowerId, e.FollowingId }).HasName("PK__Follows__79CB03351C5942");

		builder.HasOne(x => x.Follower)
			.WithMany(x => x.Following)
			.HasForeignKey(x => x.FollowerId)
			.OnDelete(DeleteBehavior.ClientSetNull)
			.HasConstraintName("FK__AppUser__Follower__550B1D61");

		builder.HasOne(x => x.Following)
			.WithMany(x => x.Followers)
			.HasForeignKey(x => x.FollowingId)
			.OnDelete(DeleteBehavior.ClientSetNull)
			.HasConstraintName("FK__AppUser__Following__590B1D61");
	}
}
