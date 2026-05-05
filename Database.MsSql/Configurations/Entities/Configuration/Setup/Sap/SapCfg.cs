using Domain.Entities.Entities.Configuration.Setup.Sap;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.MsSql.Configurations.Entities.Configuration.Setup.Sap;

internal class SapCfg : IEntityTypeConfiguration<SapDEM>
{
    public void Configure(EntityTypeBuilder<SapDEM> builder)
    {
        builder.ToTable("OSPS");
        builder.Property(p => p.Id)
            .IsRequired();
        builder.HasKey(p => p.Id)
            .HasName("PK_OSPS");

        builder.Property(p => p.SapCode)
            .HasColumnName("SapCode")
            .HasMaxLength(50)
            .IsRequired();
        builder.Property(p => p.DbVersion)
            .HasColumnName("DbVersion")
            .HasMaxLength(50)
            .IsRequired();
        builder.Property(p => p.DbPort)
            .HasColumnName("DbPort")
            .HasMaxLength(10)
            .IsRequired();
        builder.Property(p => p.SldServer)
            .HasColumnName("SldServer")
            .HasMaxLength(255)
            .IsRequired();
        builder.Property(p => p.ServerName)
            .HasColumnName("ServerName")
            .HasMaxLength(255)
            .IsRequired();
        builder.Property(p => p.LicensePort)
            .HasColumnName("LicensePort")
            .HasMaxLength(10)
            .IsRequired();
        builder.Property(p => p.IpAddress)
            .HasColumnName("IpAddress")
            .HasMaxLength(15)
            .IsRequired();
        builder.Property(p => p.Version)
            .HasColumnName("Version")
            .HasMaxLength(50);
        builder.Property(p => p.DbName)
            .HasColumnName("DbName")
            .HasMaxLength(128)
            .IsRequired();
        builder.Property(p => p.DbUser)
            .HasColumnName("DbUser")
            .HasMaxLength(128)
            .IsRequired();
        builder.Property(p => p.DbPassword)
            .HasColumnName("DbPassword")
            .HasMaxLength(500)
            .IsRequired();
        builder.Property(p => p.SapUser)
            .HasColumnName("SapUser")
            .HasMaxLength(128)
            .IsRequired();
        builder.Property(p => p.SapPassword)
            .HasColumnName("SapPassword")
            .HasMaxLength(500)
            .IsRequired();
        builder.Property(p => p.Active)
            .HasColumnName("Active")
            .IsRequired()
            .HasDefaultValue(true);
        builder.HasIndex(p => p.SapCode)
            .IsUnique()
            .HasName("IX_OSPS_SapCode");
        builder.HasIndex(p => p.Active)
            .HasName("IX_OSPS_Active");
    }
}
