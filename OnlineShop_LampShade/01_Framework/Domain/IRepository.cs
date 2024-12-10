using System.Linq.Expressions;

namespace _01_Framework.Domain;

public interface IRepository<in TKey, T>
{
    void Create(T entity);
    T Get(TKey id);
    List<T> GetAll();
    bool Exists(Expression<Func<T, bool>> expression);
    T GetDetails(TKey id);
    List<T> Search(Expression<Func<T, bool>> filter);
    void SaveChanges();
}