using Web.BlazorServer.ViewModels.Commons;

namespace Web.BlazorServer.ViewModels.Configuration.Setup.Api;

public class ApiSetupVM : AuditableVM
{
    public string ApiCode { get; set; }
    public string Method { get; set; }
    public string Module { get; set; }
    public string Url { get; set; }
    public string ApiKey { get; set; }
    public string ApiSecretKey { get; set; }
    public string ApiToken { get; set; }
    public string ApiLoginUrl { get; set; }
    public string ApiLoginBody { get; set; }
    public bool Active { get; set; }
}
