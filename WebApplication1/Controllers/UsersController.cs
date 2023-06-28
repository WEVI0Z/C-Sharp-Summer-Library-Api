using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers;

[ApiController]
[Route("user")]
public class UsersController
{
    private IBaseRepository<User> Repository { get; set; }

    public UsersController(IBaseRepository<User> repository)
    {
        Repository = repository;
    }

    [HttpGet]
    public JsonResult Get()
    {
        return new JsonResult(Repository.GetAll());
    }

    [HttpPost]
    public JsonResult Post(User user)
    {
        Repository.Create(user);
        return new JsonResult(user);
    }

    [HttpPut]
    public JsonResult Put(User user)
    {
        bool success = true;
        User document = Repository.Get(user.Id);
        try
        {
            if (document != null)
            {
                document = Repository.Update(user);
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

        return success ? new JsonResult($"Update successful {document.Id}") : new JsonResult("Update was not successful");
    }

    [HttpDelete]
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