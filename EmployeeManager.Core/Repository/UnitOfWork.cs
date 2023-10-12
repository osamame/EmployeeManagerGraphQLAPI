using EmployeeManager.Core.IRepository;
using EmployeeManager.Data.Data;

namespace EmployeeManager.Core.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataBaseContext _context;
        private IGenericRepository<Department> _departments;
        private IGenericRepository<Salary> _salaries;

        public UnitOfWork(DataBaseContext context)
        {
            _context = context;
        }

        public IGenericRepository<Department> Departments => _departments ??= new GenericRepository<Department>(_context);

        public IGenericRepository<Salary> Salaries => _salaries ??= new GenericRepository<Salary>(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
