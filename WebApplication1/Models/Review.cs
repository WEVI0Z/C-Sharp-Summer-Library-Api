namespace WebApplication1.Models;

public class Review: BaseModel
{
    public string UserId { get; set; }
    public int BookId { get; set; }
    public string Text { get; set; }
}