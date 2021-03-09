using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoshenJimenez.Mercadia3.Web.Areas.Manage.ViewModels.Products
{
    public class UpdateViewModel
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string TagLine { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
