using System.Security.Claims;

namespace WebApplication1.Models;

public class Book: BaseModel
{
    public int UserId { get; set; }
    public int BookId { get; set; }
}