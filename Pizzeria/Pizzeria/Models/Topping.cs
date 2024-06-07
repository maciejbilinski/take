using System.ComponentModel.DataAnnotations;

namespace Pizzeria.Models
{
    public class Topping
    {
        public int Id { get; set; } // Primary key

        [StringLength(100, MinimumLength = 2 )]
        [Required]
        public string Name { get; set; }
        public List<Pizza>? Pizzas { get; set; } // Many-to-many relationship
    }
    
}
