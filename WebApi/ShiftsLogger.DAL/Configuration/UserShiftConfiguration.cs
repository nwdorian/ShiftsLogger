using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShiftsLogger.DAL.Entities;

namespace ShiftsLogger.DAL.Configuration;
public class UserShiftConfiguration : IEntityTypeConfiguration<UserShiftEntity>
{
    public void Configure(EntityTypeBuilder<UserShiftEntity> builder)
    {
        builder.ToTable("UserShift");

        builder.HasKey(us => us.Id);

        builder.Property(us => us.Id)
            .ValueGeneratedNever();

        builder.HasOne(us => us.User)
            .WithMany(u => u.Shifts)
            .HasForeignKey(us => us.UserId);

        builder.HasOne(us => us.Shift)
            .WithMany(s => s.Users)
            .HasForeignKey(us => us.ShiftId);
    }
}
