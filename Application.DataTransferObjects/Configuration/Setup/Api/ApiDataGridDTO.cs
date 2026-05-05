namespace Application.DataTransferObjects.Configuration.Setup.Api;

public class ApiDataGridDTO
{
    public Guid Id { get; set; }
    public string ApiCode { get; set; }
    public string Method { get; set; }
    public string Url { get; set; }
    public bool Active { get; set; }
}
