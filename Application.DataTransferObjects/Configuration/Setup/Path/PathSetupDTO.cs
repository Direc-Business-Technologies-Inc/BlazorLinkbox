using Application.DataTransferObjects.Commons;

namespace Application.DataTransferObjects.Configuration.Setup.Path;

public class PathSetupDTO : AuditableDTO
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
    public bool Active { get; set; }
}
