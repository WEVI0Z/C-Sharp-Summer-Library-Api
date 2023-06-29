using System.Security.Claims;

namespace WebApplication1.Models;

public class User: BaseModel
{
    public string Login { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
}