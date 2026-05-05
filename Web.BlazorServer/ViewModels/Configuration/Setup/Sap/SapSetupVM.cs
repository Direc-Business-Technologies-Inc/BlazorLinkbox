using Web.BlazorServer.ViewModels.Commons;

namespace Web.BlazorServer.ViewModels.Configuration.Setup.Sap;

public class SapSetupVM : AuditableVM
{
    public string SapCode { get; set; }
    public string DbVersion { get; set; }
    public string DbPort { get; set; }
    public string SldServer { get; set; }
    public string ServerName { get; set; }
    public string LicensePort { get; set; }
    public string IpAddress { get; set; }
    public string Version { get; set; }
    public string DbName { get; set; }
    public string DbUser { get; set; }
    public string DbPassword { get; set; }
    public string SapUser { get; set; }
    public string SapPassword { get; set; }
    public bool Active { get; set; } = true;
}
