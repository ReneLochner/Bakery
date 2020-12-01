using Bakery.Core.Contracts;
using Bakery.Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace Bakery.Web.Pages
{
  public class DeleteModel : PageModel
  {
    private readonly IUnitOfWork _uow;

    public DeleteModel(IUnitOfWork uow)
    {
      _uow = uow;
    }

    public async Task<IActionResult> OnGet(int id)
    {
      return Page();
    }
  }
}
