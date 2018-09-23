using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagementSystemApi.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public int UnitId { get; set; }
        public decimal Price { get; set; }
    }
}