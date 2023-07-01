using System.Security.Claims;

namespace WebApplication1.Models;

public class Book: BaseModel
{
    public string UserId { get; set; }
    public int BookId { get; set; }
}