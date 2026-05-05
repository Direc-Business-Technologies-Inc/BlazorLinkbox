using Domain.Entities.Entities.Configuration.Setup.Api;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.MsSql.Configurations.Entities.Configuration.Setup.Api;

internal class ApiCfg : IEntityTypeConfiguration<ApiDEM>
{
    public void Configure(EntityTypeBuilder<ApiDEM> builder)
    {
        builder.ToTable("OAPS");
        builder.Property(p => p.Id)
            .IsRequired();
        builder.HasKey(p => p.Id)
            .HasName("PK_OAPS");

        builder.Property(p => p.ApiCode)
            .HasColumnName("ApiCode")
            .HasMaxLength(50)
            .IsRequired();
        builder.Property(p => p.Method)
            .HasColumnName("Method")
            .HasMaxLength(10)
            .IsRequired();
        builder.Property(p => p.Module)
            .HasColumnName("Module")
            .HasMaxLength(100)
            .IsRequired();
        builder.Property(p => p.Url)
            .HasColumnName("Url")
            .HasMaxLength(500)
            .IsRequired();
        builder.Property(p => p.ApiKey)
            .HasColumnName("ApiKey")
            .HasMaxLength(500);
        builder.Property(p => p.ApiSecretKey)
            .HasColumnName("ApiSecretKey")
            .HasMaxLength(500);
        builder.Property(p => p.ApiToken)
            .HasColumnName("ApiToken")
            .HasMaxLength(1000);
        builder.Property(p => p.ApiLoginUrl)
            .HasColumnName("ApiLoginUrl")
            .HasMaxLength(500);
        builder.Property(p => p.ApiLoginBody)
            .HasColumnName("ApiLoginBody")
            .HasColumnType("nvarchar(max)");
        builder.Property(p => p.Active)
            .HasColumnName("Active")
            .IsRequired()
            .HasDefaultValue(true);
        builder.HasIndex(p => p.ApiCode)
            .IsUnique()
            .HasName("IX_OAPS_ApiCode");
        builder.HasIndex(p => p.Active)
            .HasName("IX_OAPS_Active");
    }
}
