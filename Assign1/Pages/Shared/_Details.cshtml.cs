using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Assign1.Data;
using Assign1.Models;

namespace Assign1.Pages.Shared
{
    public class _DetailsModel : PageModel
    {
        private readonly Assign1.Data.Assign1Context _context;

        public _DetailsModel(Assign1.Data.Assign1Context context)
        {
            _context = context;
        }

        public IList<Product> Product { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Product = await _context.Product.ToListAsync();
        }
    }
}
