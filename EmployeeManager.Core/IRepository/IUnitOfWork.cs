using EmployeeManager.Data.Data;

namespace EmployeeManager.Core.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Salary> Salaries { get; }

        IGenericRepository<Department> Departments { get; }

        Task<int> SaveAsync();
    }
}
