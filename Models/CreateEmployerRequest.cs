namespace EasyEmployerBackend.Models;

public class CreateEmployerRequest
{
    public string AccessToken { get; set; } = string.Empty;
    public string Werkgevernaam { get; set; } = string.Empty;
}
