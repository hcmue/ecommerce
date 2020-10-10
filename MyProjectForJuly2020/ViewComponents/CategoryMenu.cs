using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProjectForJuly2020.Data;
using System.Threading.Tasks;

namespace MyProjectForJuly2020.ViewComponents
{
    public class CategoryMenu : ViewComponent
    {
        private readonly MyDbContext _context;

        public CategoryMenu(MyDbContext ctx)
        {
            _context = ctx;
        }

        public async Task<IViewComponentResult> InvokeAsync() {
            return View(await _context.Loais.ToListAsync());
        }
    }
}
