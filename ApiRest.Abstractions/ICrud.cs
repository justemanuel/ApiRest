using System;
using System.Collections.Generic;

namespace ApiRest.Abstractions
{
    public interface ICrud<T>
    {
        T Save(T entity);
        IList<T> GetAll();
        T GetById(int id);
        void Delete(int id);
    }
}
