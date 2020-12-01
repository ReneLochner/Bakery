using Bakery.Core.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Bakery.Core.DTOs
{
  public class OrderWithItemsDto : OrderDto
  {
    public IEnumerable<OrderItemDto> OrderedItems { get; set; }

    [DisplayFormat(DataFormatString = "{0:f2}", ApplyFormatInEditMode = true)]
    public double Sales => OrderedItems?.Sum(i => i.Amount * double.Parse(i.ProductPrice)) ?? 0;

    public OrderWithItemsDto() : base(null)
    {
      OrderedItems = new List<OrderItemDto>();
    }
    public OrderWithItemsDto(Order order) : base(order)
    {
      OrderedItems = order.OrderItems
          .Select(o => new OrderItemDto
          {
            Id = o.Id,
            ProductName = o.Product.Name,
            ProductPrice = $"{o.Product.Price:f2}",
            Amount = o.Amount
          })
          .ToList();
    }
  }
}
