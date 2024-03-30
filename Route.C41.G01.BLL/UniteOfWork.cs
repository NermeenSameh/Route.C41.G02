using Route.C41.G01.BLL.Interfaces;
using Route.C41.G01.BLL.Repositories;
using Route.C41.G02.DAL.Data;
using Route.C41.G02.DAL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.G01.BLL
{
    public class UniteOfWork : IUniteOfWork
    {
        /// public IEmployeeRepository EmployeeRepository { get; set; }
        /// public IDepartmentRepository DepartmentRepository { get; set; }
        public ApplicationDbContext _dbContext { get; }

        // private Dictionary<string, IGenericRepository<ModelBase>> _repositoties;
        private Hashtable _repositoties;
        public UniteOfWork(ApplicationDbContext dbContext) // Ask CLR For Creating Object from 'DbContext'
        {
            _dbContext = dbContext;
            _repositoties = new Hashtable();
            ///  EmployeeRepository = new EmployeeRepository(_dbContext);
            ///  DepartmentRepository = new DepartmentRepository(_dbContext);



        }
        public int Complete()
        {
            return _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public IGenericRepository<T> Repository<T>() where T : ModelBase
        {

            var key = typeof(T).Name;

            if (!_repositoties.ContainsKey(key))
            {

                if (key == nameof(Employee))
                {
                    var repository = new EmployeeRepository(_dbContext);
                    _repositoties.Add(key, repository);

                }
                else
                {
                   var repository = new GenericRepository<T>(_dbContext);
                    _repositoties.Add(key, repository);

                }

            }
            return _repositoties[key] as IGenericRepository<T>;
        }
    }
}
