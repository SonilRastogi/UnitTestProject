using System.ComponentModel.DataAnnotations;
using System;

namespace ShoppingCartProject.Models
{
    public class ShoppingItem
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Manufacturer { get; set; }
    }
}
