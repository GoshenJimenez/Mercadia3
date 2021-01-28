using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoshenJimenez.Mercadia3.Web.Infrastructure.Domain.Models
{
    public class ProductTag : BaseModel
    {
        public Guid? ProductId { get; set; }
        public Product Product { get; set; }
        public Guid? TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
