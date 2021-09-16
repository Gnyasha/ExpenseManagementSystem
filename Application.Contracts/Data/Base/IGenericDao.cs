using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Contracts.Data.Base
{
  
    public interface IGenericDao<T> where T : class
    {
        T GetById(int id);
        IReadOnlyList<T> GetAll();
        T SaveOrUpdate(T entity);
        int DeleteById(int id);
    }
}
