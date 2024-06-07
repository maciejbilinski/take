using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Pizzeria.Models
{
    public class PositiveDecimalWithTwoDecimalsAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is decimal price)
            {
                if (price <= 0)
                {
                    return new ValidationResult("The price must be a positive number.");
                }

                if (price != Math.Round(price, 2))
                {
                    return new ValidationResult("The price must have at most two decimal places.");
                }
            }
            else
            {
                return new ValidationResult("The field must be a decimal.");
            }

            return ValidationResult.Success;
        }
    }

    public enum CrustType
    {
        Thin,
        Thick
    }

    public class Pizza
    {
        public int Id { get; set; } // Primary key

        [StringLength(100, MinimumLength = 2)]
        [Required]
        public string Name { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        [Required]
        [PositiveDecimalWithTwoDecimalsAttribute]
        [DisplayName("Small Price")]
        public decimal SmallPrice { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        [Required]
        [PositiveDecimalWithTwoDecimalsAttribute]
        [DisplayName("Medium Price")]
        public decimal MediumPrice { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        [Required]
        [PositiveDecimalWithTwoDecimalsAttribute]
        [DisplayName("Large Price")]
        public decimal LargePrice { get; set; }

        [DisplayName("Crust Type")]
        [Required]
        public CrustType CrustType { get; set; }

        public List<Topping>? Toppings { get; set; } // Many-to-many relationship
    }
}
