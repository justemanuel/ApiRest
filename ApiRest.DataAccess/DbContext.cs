using ApiRest.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiRest.DataAccess
{
    public class DbContext<T> : IDbContext<T> where T : class, IEntity
    {
        DbSet<T> _items;

        ApiDbContext _context;

        public DbContext(ApiDbContext context)
        {
            _context = context;
            _items = _context.Set<T>();
        }

        public void Delete(int id)
        {
            var item = GetById(id);

            if (item != null)
                _items.Remove(item);

            _context.SaveChanges();
        }

        public IList<T> GetAll()
        {
            return _items.ToList();
        }

        public T GetById(int id)
        {
            return _items.FirstOrDefault(x => x.Id == id);
        }

        public T Save(T entity)
        {
            _items.Add(entity);
            _context.SaveChanges();
            return entity;
        }
    }
}
