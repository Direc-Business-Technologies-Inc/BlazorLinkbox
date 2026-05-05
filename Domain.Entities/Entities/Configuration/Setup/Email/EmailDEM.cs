using Ardalis.GuardClauses;
using Domain.Commons;
using Domain.Markers;

namespace Domain.Entities.Entities.Configuration.Setup.Email;

public class EmailDEM : AuditableDEM, IAggregateRoot, IActivateable
{
    public string EmailCode { get; private set; }
    public string Description { get; private set; }
    public string EmailAddress { get; private set; }
    public string DisplayName { get; private set; }
    public string EmailPassword { get; private set; }
    public string SMTPClient { get; private set; }
    public string Port { get; private set; }
    public bool Active { get; private set; }

    EmailDEM() { }

    EmailDEM(
        string emailCode, 
        string description, 
        string emailAddress, 
        string displayName, 
        string emailPassword, 
        string sMTPClient, 
        string port, 
        bool active)
    {
        EmailCode = Guard.Against.NullOrEmpty(emailCode, nameof(emailCode), "Email Code cannot be null or empty");
        Description = Guard.Against.NullOrEmpty(description, nameof(description), "Description cannot be null or empty"); 
        EmailAddress = Guard.Against.NullOrEmpty(emailAddress, nameof(emailAddress), "Email Address cannot be null or empty"); 
        DisplayName = Guard.Against.NullOrEmpty(displayName, nameof(displayName), "Display name cannot be null or empty");
        EmailPassword = Guard.Against.NullOrEmpty(emailPassword, nameof(emailPassword), "Email Password cannot be null or empty");
        SMTPClient = Guard.Against.NullOrEmpty(sMTPClient, nameof(sMTPClient), "SMTP cannot be null or empty");
        Port = Guard.Against.NullOrEmpty(port, nameof(port), "Remote port cannot be null or empty");
        Active = active;
    }

    public static EmailDEM Create(
        string emailCode,
        string description,
        string emailAddress,
        string displayName,
        string emailPassword,
        string sMTPClient,
        string port,
        bool active = true)
    {
        return new(
            emailCode,
            description,
            emailAddress,
            displayName,
            emailPassword,
            sMTPClient,
            port,
            active);
    }

    public EmailDEM Update(
        string emailCode,
        string description,
        string emailAddress,
        string displayName,
        string emailPassword,
        string sMTPClient,
        string port,
        bool active)
    {
        EmailCode = Guard.Against.NullOrEmpty(emailCode, nameof(emailCode), "Email Code cannot be null or empty");
        Description = Guard.Against.NullOrEmpty(description, nameof(description), "Description cannot be null or empty");
        EmailAddress = Guard.Against.NullOrEmpty(emailAddress, nameof(emailAddress), "Email Address cannot be null or empty");
        DisplayName = Guard.Against.NullOrEmpty(displayName, nameof(displayName), "Display name cannot be null or empty");
        EmailPassword = Guard.Against.NullOrEmpty(emailPassword, nameof(emailPassword), "Email Password cannot be null or empty");
        SMTPClient = Guard.Against.NullOrEmpty(sMTPClient, nameof(sMTPClient), "SMTP cannot be null or empty");
        Port = Guard.Against.NullOrEmpty(port, nameof(port), "Remote port cannot be null or empty");
        Active = active;

        return this;
    }

    public EmailDEM Activate()
    {
        Active = true;
        return this;
    }

    public EmailDEM Deactivate()
    {
        Active = false;
        return this;
    }
}
