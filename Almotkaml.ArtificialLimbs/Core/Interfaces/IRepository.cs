using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almotkaml.ArtificialLimbs.Core.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Func<T, bool> predicate);
        T GetById(int id);

        void Add(T entity);
        void AddRange(IEnumerable<T> entitye);

        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entitye);

        void Update(T entity);

        int Count(Func<T, bool> predicate);

    }
}
