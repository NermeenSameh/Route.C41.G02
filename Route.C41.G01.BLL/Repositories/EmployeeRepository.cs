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
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
       // private readonly ApplicationDbContext _dbContext;
        public EmployeeRepository(ApplicationDbContext dbContext) : base(dbContext) // Ask CLR for Creating Object from ApplicationDbContext
        {
        }

        ///  private readonly ApplicationDbContext _dbContext; // NULL
        ///
        ///  public EmployeeRepository(ApplicationDbContext dbContext) // Ask ClR For Creating Object From
        ///  {
        ///      //_dbContext = new ApplicationDbContext(new Microsoft.EntityFrameworkCore.DbContextOptions<ApplicationDbContext>());
        ///      _dbContext = dbContext;
        ///  }
        ///
        ///  public int Add(Employee entity)
        ///  {
        ///      _dbContext.Employees.Add(entity);
        ///      return _dbContext.SaveChanges();
        ///  }
        ///
        ///  public int Update(Employee entity)
        ///  {
        ///      _dbContext.Employees.Update(entity);
        ///      return _dbContext.SaveChanges();
        ///
        ///  }
        ///
        ///  public int Delete(Employee entity)
        ///  {
        ///      _dbContext.Employees.Remove(entity);
        ///      return _dbContext.SaveChanges();
        ///
        ///  }
        ///
        ///  public IEnumerable<Employee> GetAll()
        ///    => _dbContext.Employees.AsNoTracking().ToList();
        ///
        ///  public Employee Get(int id)
        ///  {
        ///    
        ///      return _dbContext.Find<Employee>(id); // EF Core 3.1 New Feature 
        ///
        ///  }

         public IQueryable<Employee> GetEmployeeByAddress(string address)
         {
             //return _dbContext.Employees.Where(E => E.Address.ToLower()== address.ToLower());
             return _dbContext.Employees.Where(E => E.Address.Equals(address));
         
         
         }

        public IQueryable<Employee> SearchByName(string name)
         => _dbContext.Employees.Where(E => E.Name.ToLower().Contains(name));


        public override async Task<IEnumerable<Employee>> GetAll()
            => await _dbContext.Set<Employee>().Include(E => E.Departments).AsNoTracking().ToListAsync();  



    }
}
