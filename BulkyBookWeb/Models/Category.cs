using System.ComponentModel.DataAnnotations;
namespace BulkyBookWeb.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter category name")]
        [StringLength(maximumLength: 20, MinimumLength = 3, ErrorMessage = "Please enter category name of length 3 to 20 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter display ordeer")]
        [Range(minimum: 1, maximum: 100, ErrorMessage = "Please enter display order from 1 to 100 only")]
        public int? DisplayOrder { get; set; }

        public DateTime MyProperty { get; set; } = DateTime.Now;
    }
}
