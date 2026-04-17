namespace Web.BlazorServer.ViewModels.Configuration.Setup.Path;

public class PathDataGridVM
{
    public Guid Id { get; set; }
    public string PathCode { get; set; }
    public string LocalPath { get; set; }
    public string BackupPath { get; set; }
    public string ErrorPath { get; set; }
    public bool Active { get; set; }
}
