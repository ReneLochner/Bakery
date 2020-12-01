using Bakery.Core.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Bakery.Persistence
{
  public class OrderRepository : IOrderRepository
  {
    private readonly ApplicationDbContext _dbContext;
    public OrderRepository(ApplicationDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public async Task<int> GetCountAsync()
    {
      return await _dbContext.Orders.CountAsync();
    }
  }
}
