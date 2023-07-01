using WebApplication1.Models;

namespace WebApplication1.Repositories;

public class BaseRepository<TDbModel> : IBaseRepository<TDbModel> where TDbModel: BaseModel
{
    private ApplicationContext Context { get; set; }
    public BaseRepository(ApplicationContext context)
    {
        Context = context;
    }

    public TDbModel Create(TDbModel model)
    {
        Context.Set<TDbModel>().Add(model);
        Context.SaveChanges();
        return model;
    }

    public void Delete(Guid id)
    {
        var toDelete = Context.Set<TDbModel>().FirstOrDefault(m => m.Id == id);
        Context.Set<TDbModel>().Remove(toDelete);
        Context.SaveChanges();
    }

    public List<TDbModel> GetAll()
    {
        return Context.Set<TDbModel>().ToList();
    }

    public TDbModel Update(TDbModel model)
    {
        // var toUpdate = Context.Set<TDbModel>().FirstOrDefault(m => m.Id == model.Id);
        // if (toUpdate != null)
        // {
        //     toUpdate = model;
        // }
        // Context.Update(model);
        // Context.SaveChanges();
        // return toUpdate;

        var trackedBlog = Context.Users.Find(model.Id);

        Context.Entry(trackedBlog).CurrentValues.SetValues(model);

        Context.SaveChanges();

        return model;
    }

    public TDbModel Get(Guid id)
    {
        return Context.Set<TDbModel>().FirstOrDefault(m => m.Id == id);
    }
}