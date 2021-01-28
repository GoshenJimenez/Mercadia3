using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoshenJimenez.Mercadia3.Web.Infrastructure.Domain.Models
{
    public class Product : BaseModel
    {
        public string Name { get; set; }
        public string TagLine { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Guid? CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
