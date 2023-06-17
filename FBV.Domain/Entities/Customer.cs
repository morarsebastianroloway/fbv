using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBV.Domain.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string EmailAddress { get; set; } = string.Empty;
    }
}
