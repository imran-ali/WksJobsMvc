using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WksJobsMvc.Data;

namespace WksJobsMvc.Controllers.API
{
    public class ItemListController : Controller
    {
        private readonly WksDbContext _context;
        public ItemListController(WksDbContext context)
        {
            _context = context;
        }

        

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        //[Route("search")]
        public async Task<IActionResult> Search(string term)
        {
            if (!string.IsNullOrEmpty(term))
            {
                var items = await _context.Items.ToListAsync();
                var data = items.Where(a => a.ItemCode.Contains(term, StringComparison.OrdinalIgnoreCase)
                || a.MaterialDescription.Contains(term, StringComparison.OrdinalIgnoreCase)).ToList().AsReadOnly();
                return Ok(data);
            }
            else
            {
                return Ok();
            }
        }
    }
}
