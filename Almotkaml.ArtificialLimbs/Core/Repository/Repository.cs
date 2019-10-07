using Almotkaml.ArtificialLimbs.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Core.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ArtificialLimbsDbContext _context;
        protected readonly IUnitOfWork _unitOfWork;

        public Repository(ArtificialLimbsDbContext context)
        {
            _context = context;
           // _unitOfWork = new UnitOfWork(_context);
        }



        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void AddRange(IEnumerable<T> entitye)
        {
            _context.Set<T>().AddRange(entitye);
        }

        public int Count(Func<T, bool> predicate)
        {
            return _context.Set<T>().Where(predicate).Count();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }
        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entitye)
        {
            _context.Set<T>().RemoveRange(entitye);

        }

        public void Update(T entity)
        {
            //var entry = _context.Entry(entity);
            //if (entry.State == EntityState.Detached || entry.State == EntityState.Modified)
            //{
            //    entry.State = EntityState.Modified;
            //    _context.Set<T>().Attach(entity); //attach
            //}
            _context.Entry(entity).State = EntityState.Modified;

        }


    }
}