using GoshenJimenez.Mercadia3.Web.Infrastructure.Domain;
using GoshenJimenez.Mercadia3.Web.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoshenJimenez.Mercadia3.Web.Controllers
{
    public class ProductsController : Controller
    {

        private readonly DefaultDbContext _context;
        public ProductsController(DefaultDbContext context)
        {
            _context = context;        
        }

        //list
        //pager
        //search-keyword
        //filter
        //sort
        public IActionResult Index()
        {
            return View();
        }

        [Route("products/{id}")]
        public IActionResult Index(Guid? id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
         
            return View(new ProductViewModel()
            {
                Id = product.Id,
                Description = product.Description,
                CategoryId = product.CategoryId,
                CreatedAt = product.CreatedAt,
                Name = product.Name,
                Price = product.Price,
                TagLine = product.TagLine,
                UpdatedAt = product.UpdatedAt
            });
        }
    }
}
