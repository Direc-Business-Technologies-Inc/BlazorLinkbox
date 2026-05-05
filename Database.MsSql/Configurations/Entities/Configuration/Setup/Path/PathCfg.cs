using Domain.Entities.Entities.Configuration.Setup.Path;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.MsSql.Configurations.Entities.Configuration.Setup.Path;

internal class PathCfg : IEntityTypeConfiguration<PathDEM>
{
    public void Configure(EntityTypeBuilder<PathDEM> builder)
    {
        builder.ToTable("OPTS");
        builder.Property(p => p.Id)
            .IsRequired();
        builder.HasKey(p => p.Id)
            .HasName("PK_OPTS");

        builder.Property(p => p.PathCode)
            .HasColumnName("PathCode")
            .HasMaxLength(50)
            .IsRequired();
        builder.Property(p => p.LocalPath)
            .HasColumnName("LocalPath")
            .HasMaxLength(500)
            .IsRequired();
        builder.Property(p => p.FileSearchOption)
            .HasColumnName("FileSearchOption")
            .HasMaxLength(100)
            .IsRequired();
        builder.Property(p => p.BackupPath)
            .HasColumnName("BackupPath")
            .HasMaxLength(500);
        builder.Property(p => p.ErrorPath)
            .HasColumnName("ErrorPath")
            .HasMaxLength(500);
        builder.Property(p => p.RemotePath)
            .HasColumnName("RemotePath")
            .HasMaxLength(500);
        builder.Property(p => p.RemoteServer)
            .HasColumnName("RemoteServer")
            .HasMaxLength(255);
        builder.Property(p => p.RemoteIpAddress)
            .HasColumnName("RemoteIpAddress")
            .HasMaxLength(15);
        builder.Property(p => p.RemotePort)
            .HasColumnName("RemotePort")
            .HasMaxLength(5);
        builder.Property(p => p.RemoteUserId)
            .HasColumnName("RemoteUserId")
            .HasMaxLength(100);
        builder.Property(p => p.RemotePassword)
            .HasColumnName("RemotePassword")
            .HasMaxLength(500);
        builder.Property(p => p.Active)
            .HasColumnName("Active")
            .IsRequired()
            .HasDefaultValue(true);
        builder.HasIndex(p => p.PathCode)
            .IsUnique()
            .HasName("IX_OPTS_PathCode");
        builder.HasIndex(p => p.Active)
            .HasName("IX_OPTS_Active");
    }
}
