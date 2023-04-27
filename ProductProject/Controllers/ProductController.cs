using Microsoft.AspNetCore.Mvc;

namespace ProductProject.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductDbContext _dbContext;

        public ProductController(ProductDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var products = _dbContext.Products.ToList();
            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Products.Add(product);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }
    }
}
