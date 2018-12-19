using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System;

namespace prodandcat.Models
{
    public class Product
    {
        [Key]
        public int ProductId {get; set;}

        public List<Associations> Associations {get; set;}

        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        [Display(Name = "Name:")]
        public string Name {get; set;}

        [Required]
        [MinLength(3)]
        [MaxLength(255)]
        [Display(Name = "Description:")]
        public string Description {get; set;}

        [Required]
        [Display(Name = "Price:")]
        public decimal Price {get; set;}
        public DateTime CreatedAt {get; set;}
        public DateTime UpdatedAt {get; set;}

    }
}