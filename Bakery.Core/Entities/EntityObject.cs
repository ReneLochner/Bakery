using Bakery.Core.Contracts;
using System.ComponentModel.DataAnnotations;

namespace Bakery.Core.Entities
{

  public class EntityObject : IEntityObject
  {
    #region IEnityObject Members

    [Key]
    public int Id { get; set; }

    [Timestamp]
    public byte[] Timestamp
    {
      get;
      set;
    }

    #endregion
  }
}
