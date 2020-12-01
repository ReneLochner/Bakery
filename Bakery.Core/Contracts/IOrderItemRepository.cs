using System.Threading.Tasks;

namespace Bakery.Core.Contracts
{
  public interface IOrderItemRepository
  {
    Task<int> GetCountAsync();
  }
}