using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bakery.Core.Entities
{
  public class Order: EntityObject
    {
        [Required]
        [Display(Name ="Bestellnr.")]
        public string OrderNr { get; set; }

        [Display(Name = "Datum")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", 
               ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
 

        [Display(Name = "Kunde")]
        public Customer Customer { get; set; }
        [DisplayName("Kunde")]
        public int CustomerId { get; set; }

        public List<OrderItem> OrderItems { get; set; }
    }
}
