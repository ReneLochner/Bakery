using System.Threading.Tasks;

namespace Bakery.Core.Contracts
{
  public interface ICustomerRepository
  {
    Task<int> GetCountAsync();
  }
}