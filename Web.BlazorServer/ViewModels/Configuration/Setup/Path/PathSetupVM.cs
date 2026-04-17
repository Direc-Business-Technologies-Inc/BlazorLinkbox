using Web.BlazorServer.ViewModels.Commons;

namespace Web.BlazorServer.ViewModels.Configuration.Setup.Path;

public class PathSetupVM : AuditableVM
{
    public string PathCode { get; set; }
    public string LocalPath { get; set; }
    public string FileSearchOption { get; set; }
    public string BackupPath { get; set; }
    public string ErrorPath { get; set; }
    public string RemotePath { get; set; }
    public string RemoteServer { get; set; }
    public string RemoteIpAddress { get; set; }
    public string RemotePort { get; set; }
    public string RemoteUserId { get; set; }
    public string RemotePassword { get; set; }
    public bool Active { get; set; } = true;
}
