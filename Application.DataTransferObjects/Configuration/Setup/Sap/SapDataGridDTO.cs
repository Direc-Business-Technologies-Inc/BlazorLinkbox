namespace Application.DataTransferObjects.Configuration.Setup.Sap;

public class SapDataGridDTO
{
    public Guid Id { get; set; }
    public string SapCode { get; set; }
    public string DbVersion { get; set; }
    public int LicensePort { get; set; }
    public string IpAddress { get; set; }
    public string Version { get; set; }
    public string DbPort { get; set; }
    public string DbName { get; set; }
    public string DbUser { get; set; }
    public string SapUser { get; set; }
    public bool Active { get; set; }
}
