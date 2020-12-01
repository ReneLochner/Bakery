using Bakery.Core.Contracts;
using Bakery.Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Bakery.Web.Pages
{
  public class IndexModel : PageModel
  {
    private readonly IUnitOfWork _uow;

    [Display(Name = "Nachname")]
    [BindProperty]
    public string FilterLastName { get; set; }
    
    public IndexModel(IUnitOfWork uow)
    {
      _uow = uow;
    }

    public async Task<IActionResult> OnGet()
    {
      return Page();
    }

    public async Task<IActionResult> OnPostSearch()
    {
      return Page();
    }
  }
}
