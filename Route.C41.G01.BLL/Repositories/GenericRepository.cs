using Microsoft.EntityFrameworkCore;
using Route.C41.G01.BLL.Interfaces;
using Route.C41.G02.DAL.Data;
using Route.C41.G02.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.G01.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : ModelBase
    {
        private protected readonly ApplicationDbContext _dbContext; // NULL

        public GenericRepository(ApplicationDbContext dbContext) // Ask ClR For Creating Object From
        {
            //_dbContext = new ApplicationDbContext(new Microsoft.EntityFrameworkCore.DbContextOptions<ApplicationDbContext>());
            _dbContext = dbContext;
        }



        // public int Add(T entity)
        // {
        //     _dbContext.Set<T>().Add(entity);
        //     return _dbContext.SaveChanges();
        // }

        public void Add(T entity)
          => _dbContext.Set<T>().Add(entity);


        //  public int Update(T entity)
        //  {
        //      _dbContext.Set<T>().Update(entity);
        //      return _dbContext.SaveChanges();
        //
        //  }

        public void Update(T entity)
           => _dbContext.Set<T>().Update(entity);



        //  public int Delete(T entity)
        //  {
        //      _dbContext.Set<T>().Remove(entity);
        //      return _dbContext.SaveChanges();
        //
        //  }

        public void Delete(T entity)
           => _dbContext.Set<T>().Remove(entity);



        public IEnumerable<T> GetAll()
        {
            if (typeof(T) == typeof(Employee))
                return (IEnumerable<T>)_dbContext.Employees.Include(E => E.Departments).AsNoTracking().ToList();
            else
                return _dbContext.Set<T>().AsNoTracking().ToList();

        }

        public T Get(int id)
        {
            /// var department = _dbContext.Departments.Local.Where(D => D.Id == id).FirstOrDefault();
            /// if (department == null)
            ///     department = _dbContext.Departments.Where(D => D.Id == id).FirstOrDefault();
            ///
            /// return department;

            //  return _dbContext.Departments.Find(id);

            return _dbContext.Find<T>(id); // EF Core 3.1 New Feature 

        }

    }
}
