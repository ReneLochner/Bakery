using System.Threading.Tasks;

namespace Bakery.Core.Contracts
{
  public interface IOrderRepository
  {
    Task<int> GetCountAsync();
  }
}