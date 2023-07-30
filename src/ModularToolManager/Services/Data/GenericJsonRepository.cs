using System;
using System.Collections.Generic;

namespace ModularToolManager.Services.Data;
internal class GenericJsonRepository<T, I> : IRepository<T, I>
{
    public bool Delete(T entity)
    {
        throw new NotImplementedException();
    }

    public bool DeleteById(I id)
    {
        throw new NotImplementedException();
    }

    public T? FindById(I id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<T> GetAll(Func<T, bool> filter)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<T> GetAll()
    {
        throw new NotImplementedException();
    }

    public T? Insert(T entity)
    {
        throw new NotImplementedException();
    }

    public T? Update(T entity)
    {
        throw new NotImplementedException();
    }
}
