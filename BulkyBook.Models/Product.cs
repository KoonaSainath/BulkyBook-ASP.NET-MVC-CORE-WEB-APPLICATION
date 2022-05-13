using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using BulkyBookWeb.Models;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BulkyBook.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        [Range(1,10000)]
        [Display(Name = "List price")]
        public double ListPrice { get; set; }
        [Required]
        [Range(1,10000)]
        public double Price { get; set; }
        [Required]
        [Range(1,10000)]
        [Display(Name = "Price if number of books ordered crosses 50")]
        public double Price50 { get; set; }
        [Required]
        [Range(1,10000)]
        [Display(Name = "Price if number of books ordered crosses 100")]
        public double Price100 { get; set; }
        [ValidateNever]
        [Display(Name = "Upload image")]
        public string ImageUrl { get; set; }
        [Required]
        [Display(Name = "Category")]
        public int CategoryId  { get; set; }
        [ValidateNever]
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        [Required]
        [Display(Name = "Cover type")]
        public int CoverTypeId { get; set; }
        [ValidateNever]
        [ForeignKey("CoverTypeId")]
        public CoverType CoverType { get; set; }

    }
}
