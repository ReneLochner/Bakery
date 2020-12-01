using System;
using System.Threading.Tasks;

namespace Bakery.Core.Contracts
{
  public interface IUnitOfWork : IAsyncDisposable
  {

    /// <summary>
    /// Standard Repositories 
    /// </summary>
    ICustomerRepository Customers { get; }

    IOrderRepository Orders { get; }
    IProductRepository Products { get; }
    IOrderItemRepository OrderItems { get; }



    Task<int> SaveChangesAsync();
    Task DeleteDatabaseAsync();
    Task MigrateDatabaseAsync();
    Task CreateDatabaseAsync();
  }
}
