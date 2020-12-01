using Bakery.Core.Entities;
using System.Linq;

namespace Bakery.Core.DTOs
{
  public class ProductDto
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string ProductNr { get; set; }

    public double Price { get; set; }

    public int AmountSold { get; set; }

    public double Sales => AmountSold * Price;

    public ProductDto(Product p)
    {
      if (p != null)
      {
        Id = p.Id;
        Name = p.Name;
        ProductNr = p.ProductNr;
        Price = p.Price;
        AmountSold = p.OrderItems.Count();
      }
    }
  }
}
