namespace WebApplication1.Models;

public class Rate: BaseModel
{
    public string UserId { get; set; }
    public int BookId { get; set; }
    public int Mark { get; set; }
}