using System.ComponentModel.DataAnnotations;


namespace ErrorHandlingProblemDetails.Data.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Product name is required")]
        public string Name { get; set; }
        [StringLength(20, ErrorMessage = "Category name cannot be longer than 20 characters")]
        public string Category { get; set; }
    }
}
