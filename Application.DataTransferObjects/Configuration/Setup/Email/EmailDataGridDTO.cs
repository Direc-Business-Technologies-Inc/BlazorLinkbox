namespace Application.DataTransferObjects.Configuration.Setup.Email;

public class EmailDataGridDTO
{
    public Guid Id { get; set; }
    public string EmailCode { get; set; }
    public string Description { get; set; }
    public bool Active { get; set; }
}
