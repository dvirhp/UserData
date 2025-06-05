namespace UserData.Models;

public class User
{
    public string FirstName { get; set; } = ""; // Default value to avoid null
    public string LastName { get; set; } = "";
    public string Email { get; set; } = "";
    public string SourceId { get; set; } = "";
}
