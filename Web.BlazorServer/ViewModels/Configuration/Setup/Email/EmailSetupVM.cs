using Web.BlazorServer.ViewModels.Commons;

namespace Web.BlazorServer.ViewModels.Configuration.Setup.Email;

public class EmailSetupVM : AuditableVM
{
    public string EmailCode { get; set; }
    public string Description { get; set; }
    public string EmailAddress { get; set; }
    public string DisplayName { get; set; }
    public string EmailPassword { get; set; }
    public string ApiSecretKey { get; set; }
    public string SMTPClient { get; set; }
    public string Port { get; set; }
    public bool Active { get; set; } = true;
}
