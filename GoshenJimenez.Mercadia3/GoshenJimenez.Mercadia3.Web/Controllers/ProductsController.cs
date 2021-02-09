using GoshenJimenez.Mercadia3.Web.Infrastructure.Domain;
using GoshenJimenez.Mercadia3.Web.Infrastructure.Domain.Models;
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
        [HttpGet("products")]
        public IActionResult Index( int pageIndex = 1, 
                                    int pageSize = 10, 
                                    string sortBy = "name", 
                                    string sortOrder = "asc", 
                                    Guid? categoryId = null, 
                                    string keyword = "")
        {
            IQueryable<Product> allProducts = _context.Products.AsQueryable();
            Paged<Product> products = new Paged<Product>();           

            if (categoryId != null)
            {
                allProducts = allProducts.Where(p => p.CategoryId == categoryId);

            }

            if (!string.IsNullOrEmpty(keyword))
            {
                allProducts = allProducts.Where(p =>    p.Name.Contains(keyword)
                                                    ||  p.TagLine.Contains(keyword)
                                                    ||  p.Description.Contains(keyword));
            }

            var queryCount = allProducts.Count();
            var skip = pageSize * (pageIndex - 1);

            long pageCount = (long)Math.Ceiling((decimal)(queryCount / pageSize));

            if (sortBy.ToLower() == "name" && sortOrder.ToLower() == "asc")
            {
                products.Items = allProducts.OrderBy(e => e.Name).Skip(skip).Take(pageSize).ToList();
            }
            else if (sortBy.ToLower() == "name" && sortOrder.ToLower() == "desc")
            {
                products.Items = allProducts.OrderByDescending(e => e.Name).Skip(skip).Take(pageSize).ToList();
            }
            else if (sortBy.ToLower() == "price" && sortOrder.ToLower() == "asc")
            {
                products.Items = allProducts.OrderBy(e => e.Price).Skip(skip).Take(pageSize).ToList();
            }
            else
            {
                products.Items = allProducts.OrderByDescending(e => e.Price).Skip(skip).Take(pageSize).ToList();
            }
          
            var result = new ProductSearchViewModel();
            result.Products = new Paged<ProductViewModel>();
            result.Products.PageCount = pageCount;
            result.Products.QueryCount = queryCount;
            result.Products.PageIndex = pageIndex;
            result.Products.PageSize = pageSize;
            result.Products.Keyword = keyword;

            if (categoryId != null)
            {
                result.Products.FilterString = "categoryId=" + categoryId.ToString();
            }

            result.Products.Items = new List<ProductViewModel>();

            foreach (var product in products.Items)
            {
                result.Products.Items.Add(new ProductViewModel()
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

            return View(result);
        }

        [HttpGet("products/{id}")]
        public IActionResult Index(Guid? id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
         
            return View("~/Views/Products/Product.cshtml", new ProductViewModel()
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
