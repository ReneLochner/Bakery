namespace Bakery.Core.DTOs
{
  public class OrderItemDto
  {
    public int Id { get; set; }
    public string ProductName { get; set; }
    public string ProductPrice { get; set; }

    public int Amount { get; set; }
  }
}
