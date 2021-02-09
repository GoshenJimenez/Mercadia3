using GoshenJimenez.Mercadia3.Web.Infrastructure.Domain;
using GoshenJimenez.Mercadia3.Web.Infrastructure.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoshenJimenez.Mercadia3.Web.ViewModels.Products
{
    public class ProductSearchViewModel
    {
        public Paged<ProductViewModel> Products { get; set; }
    }

    public class ProductViewModel : Product
    {
    }
}
