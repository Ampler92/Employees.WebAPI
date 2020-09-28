using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManager.WebAPI.Data;
using EmployeeManager.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManager.WebAPI.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly EmployeeManagerDbContext _context;
        private readonly DbSet<T> _entities;

        public Repository(EmployeeManagerDbContext context)
        {
            _context = context;
            _entities = _context.Set<T>();
        }
        public IQueryable<T> GetAll()
        {
            return _entities;
        }
        public T GetById(Guid id)
        {
            return _entities.FirstOrDefault(s => s.Id == id);
        }
        public void Insert(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            _entities.Add(entity);
            _context.SaveChanges();
        }
        public void Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _context.SaveChanges();
        }
        public void Delete(Guid id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));

            var entity = _entities.FirstOrDefault(s => s.Id == id);
            _entities.Remove(entity!);
            _context.SaveChanges();
        }
        public bool Exists(Guid id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));

            return _entities.Any(e => e.Id == id);
        }
    }
}
