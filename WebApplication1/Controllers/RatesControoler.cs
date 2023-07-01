using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers;

[ApiController]
[Route("rate")]
public class RatesControoler
{
    private IBaseRepository<Rate> Repository { get; set; }

    public RatesControoler(IBaseRepository<Rate> repository)
    {
        Repository = repository;
    }
    
    
    [HttpPost]
    public JsonResult Post(Rate book)
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