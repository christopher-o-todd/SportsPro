using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsPro.DataLayer
{
    public interface IRepository<T> where T : class
    {
        //create interface for repositories and add required methods

        List<T> List(QueryOptions<T> options);

        T Get(int id);
        T Get(QueryOptions<T> options);

        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Save();
    }
}
