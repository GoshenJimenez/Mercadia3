using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoshenJimenez.Mercadia3.Web.Infrastructure.Domain.Models
{
    public class UserLogin : BaseModel
    {
        public Guid? UserId { get; set; }

        public User User { get; set; }

        public LoginType Type { get; set; }

        public string Key { get; set; }

        public string Value { get; set; }
    }
}
