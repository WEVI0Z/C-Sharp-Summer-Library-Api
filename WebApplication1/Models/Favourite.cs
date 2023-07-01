namespace WebApplication1.Models;

public class Favourite: BaseModel
{
    public string UserId { get; set; }
    public int BookId { get; set; }
}