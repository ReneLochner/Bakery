using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bakery.Core.Entities
{
  public class OrderItem : EntityObject
  {
    public Order Order { get; set; }
    public int OrderId { get; set; }

    public Product Product { get; set; }
    [Display(Name = "Produkt")]
    public int ProductId { get; set; }

    [DisplayName("Menge")]
    [Range(1, int.MaxValue, ErrorMessage = "Anzahl muss größer 0 sein!")]
    public int Amount { get; set; }
  }
}
