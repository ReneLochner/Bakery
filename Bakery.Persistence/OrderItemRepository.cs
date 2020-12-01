using Bakery.Core.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Bakery.Persistence
{
  public class OrderItemRepository : IOrderItemRepository
  {
    private readonly ApplicationDbContext _dbContext;

    public OrderItemRepository(ApplicationDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public async Task<int> GetCountAsync()
    {
      return await _dbContext.OrderItems.CountAsync();
    }
  }
}
