using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers;

[ApiController]
[Route("user")]
public class UsersController
{
    private IBaseRepository<User> Repository { get; set; }
    private IBaseRepository<Book> BookRepository { get; set; }

    public UsersController(IBaseRepository<User> repository, IBaseRepository<Book> bookRepository)
    {
        Repository = repository;
        BookRepository = bookRepository;
    }
    
    [HttpPost("book")]
    public JsonResult Post(Book book)
    {
        BookRepository.Create(book);
        return new JsonResult(book);
    }
    
    [HttpGet("book")]
    public JsonResult getBook()
    {
        return new JsonResult(BookRepository.GetAll());
    }
    
    private ClaimsIdentity GetIdentity(User person)
    {
        if (person != null)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, person.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Role)
            };
            ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }
 
        // если пользователя не найдено
        return null;
    }

    [HttpGet]
    public JsonResult Get()
    {
        return new JsonResult(Repository.GetAll());
    }

    [HttpGet("token/{id}")]
    public JsonResult GetToken(Guid id)
    {
        User identity = Repository.Get(id);

        var pesron = GetIdentity(identity);
        
        var claims = new List<Claim> {new Claim(ClaimTypes.Name, identity.Login) };
        // создаем JWT-токен
        var jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            
        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
 
        var response = new
        {
            access_token = encodedJwt,
            username = pesron.Name
        };
 
        return new JsonResult(response);
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