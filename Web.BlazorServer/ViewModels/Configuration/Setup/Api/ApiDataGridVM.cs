namespace Web.BlazorServer.ViewModels.Configuration.Setup.Api;

public class ApiDataGridVM
{
    public Guid Id { get; set; }
    public string ApiCode { get; set; }
    public string Method { get; set; }
    public string Url { get; set; }
    public bool Active { get; set; }
}
