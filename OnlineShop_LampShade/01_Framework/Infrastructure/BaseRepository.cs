using _01_Framework.Domain;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace _01_Framework.Infrastructure;

public class BaseRepository<TKey, T> : IRepository<TKey, T> where T : class
{
    private readonly DbContext context;

    public BaseRepository(DbContext context)
    {
        this.context = context;
    }

    public void Create(T entity)
    {
        context.Add(entity);
    }

    public List<T> GetAll()
    {
        return context.Set<T>().ToList();
    }

    public bool Exists(Expression<Func<T, bool>> expression)
    {
        return context.Set<T>().Any(expression);
    }

    public T GetDetails(TKey id)
    {
        return context.Set<T>().Find(id);
    }

    public List<T> Search(Expression<Func<T, bool>> filter)
    {
        return context.Set<T>().Where(filter).ToList();
    }

    public T Get(TKey id)
    {
        return context.Find<T>(id);
    }

    public List<T> Get()
    {
        return context.Set<T>().ToList();
    }

    public void SaveChanges()
    {
        context.SaveChanges();
    }
}