using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Contracts.Data.Base
{
  
    public interface IGenericDao<T> where T : class
    {
        T GetById(int id);
        IQueryable<T> GetAll();
        T SaveOrUpdate(T entity);
        void Delete(T entity);
    }
}
