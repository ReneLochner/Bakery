using Bakery.Core.Contracts;
using Bakery.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Bakery.Web.Pages
{
  public class CreateModel : PageModel
  {
    private readonly IUnitOfWork _uow;
    
    public CreateModel(IUnitOfWork uow)
    {
      _uow = uow;
    }

    public async Task<IActionResult> OnGet()
    {
      return Page();
    }
  }
}
