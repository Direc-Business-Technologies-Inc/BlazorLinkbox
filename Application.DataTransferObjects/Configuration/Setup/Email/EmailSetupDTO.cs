using Application.DataTransferObjects.Commons;

namespace Application.DataTransferObjects.Configuration.Setup.Email;

public class EmailSetupDTO : AuditableDTO
{
    public string EmailCode { get; set; }
    public string Description { get; set; }
    public string EmailAddress { get; set; }
    public string DisplayName { get; set; }
    public string EmailPassword { get; set; }
    public string SMTPClient { get; set; }
    public string Port { get; set; }
    public bool Active { get; set; }
}
