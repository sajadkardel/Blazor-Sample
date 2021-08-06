using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor.Server.Sample.Models.Product
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Color { get; set; }

        public decimal Price { get; set; }

        public int CategoryId { get; set; }
    }
}
