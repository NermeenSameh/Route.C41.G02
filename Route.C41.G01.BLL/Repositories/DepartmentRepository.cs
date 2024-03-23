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
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {


        ///  private readonly ApplicationDbContext _dbContext; // NULL
        ///
        ///  public DepartmentRepository(ApplicationDbContext dbContext) // Ask ClR For Creating Object From
        ///  {
        ///      //_dbContext = new ApplicationDbContext(new Microsoft.EntityFrameworkCore.DbContextOptions<ApplicationDbContext>());
        ///      _dbContext = dbContext;
        ///  }
        /// 
        ///  public int Add(Department entity)
        ///  {
        ///      _dbContext.Departments.Add(entity);
        ///      return _dbContext.SaveChanges();
        ///  }
        /// 
        ///  public int Update(Department entity)
        ///  {
        ///      _dbContext.Departments.Update(entity);
        ///      return _dbContext.SaveChanges();
        ///
        ///  }
        /// 
        ///  public int Delete(Department entity)
        ///  {
        ///      _dbContext.Departments.Remove(entity);
        ///      return _dbContext.SaveChanges();
        ///
        ///  }
        ///
        ///  public IEnumerable<Department> GetAll()
        ///    => _dbContext.Departments.AsNoTracking().ToList();
        ///
        ///  public Department Get(int id)
        ///  {
        ///      /// var department = _dbContext.Departments.Local.Where(D => D.Id == id).FirstOrDefault();
        ///      /// if (department == null)
        ///      ///     department = _dbContext.Departments.Where(D => D.Id == id).FirstOrDefault();
        ///      ///
        ///      /// return department;
        ///
        ///      //  return _dbContext.Departments.Find(id);
        ///
        ///      return _dbContext.Find<Department>(id); // EF Core 3.1 New Feature 
        ///  
        ///  }
        public DepartmentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
