using Domain.Entities.Entities.Configuration.Setup.Email;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.MsSql.Configurations.Entities.Configuration.Setup.Email;

internal class EmailCfg : IEntityTypeConfiguration<EmailDEM>
{
    public void Configure(EntityTypeBuilder<EmailDEM> builder)
    {
        builder.ToTable("OEMS");
        builder.Property(p => p.Id)
            .IsRequired();
        builder.HasKey(p => p.Id)
            .HasName("PK_OEMS");

        builder.Property(p => p.EmailCode)
            .HasColumnName("EmailCode")
            .HasMaxLength(50)
            .IsRequired();
        builder.Property(p => p.Description)
            .HasColumnName("Description")
            .HasMaxLength(500)
            .IsRequired();
        builder.Property(p => p.EmailAddress)
            .HasColumnName("EmailAddress")
            .HasMaxLength(255)
            .IsRequired();
        builder.Property(p => p.DisplayName)
            .HasColumnName("DisplayName")
            .HasMaxLength(255)
            .IsRequired();
        builder.Property(p => p.EmailPassword)
            .HasColumnName("EmailPassword")
            .HasMaxLength(500)
            .IsRequired();
        builder.Property(p => p.SMTPClient)
            .HasColumnName("SMTPClient")
            .HasMaxLength(255)
            .IsRequired();
        builder.Property(p => p.Port)
            .HasColumnName("Port")
            .HasMaxLength(10)
            .IsRequired();
        builder.Property(p => p.Active)
            .HasColumnName("Active")
            .IsRequired()
            .HasDefaultValue(true);

        builder.HasIndex(p => p.EmailCode)
            .IsUnique()
            .HasName("IX_OEMS_EmailCode");
        builder.HasIndex(p => p.Active)
            .HasName("IX_OEMS_Active");
    }
}
