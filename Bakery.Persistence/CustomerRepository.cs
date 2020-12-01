using Bakery.Core.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Bakery.Persistence
{
  public class CustomerRepository : ICustomerRepository
  {
    private readonly ApplicationDbContext _dbContext;

    public CustomerRepository(ApplicationDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public async Task<int> GetCountAsync()
    {
      return await _dbContext.Customers.CountAsync();
    }
  }
}
