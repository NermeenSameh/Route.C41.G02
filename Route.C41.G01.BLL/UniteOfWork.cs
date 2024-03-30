using Route.C41.G01.BLL.Interfaces;
using Route.C41.G01.BLL.Repositories;
using Route.C41.G02.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.G01.BLL
{
    public class UniteOfWork : IUniteOfWork , IDisposable
    {
        public IEmployeeRepository EmployeeRepository { get; set; }
        public IDepartmentRepository DepartmentRepository { get; set; }
        public ApplicationDbContext _dbContext { get; }

        public UniteOfWork(ApplicationDbContext dbContext) // Ask CLR For Creating Object from 'DbContext'
        {
            _dbContext = dbContext;
            EmployeeRepository = new EmployeeRepository(_dbContext);
            DepartmentRepository = new DepartmentRepository(_dbContext);
        }
        public int Complete()
        {
           return _dbContext.SaveChanges();
        }

        public void Dispose() 
        { 
            _dbContext.Dispose();
        }
    }
}
