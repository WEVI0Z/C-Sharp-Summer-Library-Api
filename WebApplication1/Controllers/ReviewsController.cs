using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers;

[ApiController]
[Route("review")]
public class ReviewsController
{
    private IBaseRepository<Review> Repository { get; set; }

    public ReviewsController(IBaseRepository<Review> repository)
    {
        Repository = repository;
    }
    
    
    [HttpPost]
    public JsonResult Post(Review book)
    {
        Repository.Create(book);
        return new JsonResult(book);
    }
    
    [HttpGet]
    public JsonResult Get()
    {
        return new JsonResult(Repository.GetAll());
    }

    [HttpDelete("{id}")]
    public JsonResult Delete(Guid id)
    {
        bool success = true;
        var document = Repository.Get(id);

        try
        {
            if (document != null)
            {
                Repository.Delete(document.Id);
            }
            else
            {
                success = false;
            }
        }
        catch (Exception)
        {
            success = false;
        }

        return success ? new JsonResult("Delete successful") : new JsonResult("Delete was not successful");
    }
}