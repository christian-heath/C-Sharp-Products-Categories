using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System;

namespace prodandcat.Models
{
    public class Category
    {
        [Key]
        public int CategoryId {get; set;}

        public List<Associations> Associations {get; set;}

        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        [Display(Name = "Name:")]
        public string Name {get; set;}
        public DateTime CreatedAt{get; set;}
        public DateTime UpdatedAt{get; set;}
    }
}