using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bakery.Core.Entities
{
  public partial class Customer : EntityObject
  {
    [Required]
    [DisplayName("Kundennr.")]
    public string CustomerNr { get; set; }
    [MaxLength(20)]
    [DisplayName("Vorname")]
    public string FirstName { get; set; }
    [MaxLength(20)]
    [DisplayName("Nachname")]
    public string LastName { get; set; }

    public string FullName => FirstName + " " + LastName;

    public override string ToString()
    {
      return FirstName + " " + LastName;
    }
  }
}
