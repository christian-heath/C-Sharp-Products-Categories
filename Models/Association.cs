using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System;

namespace prodandcat.Models
{
    public class Associations
    {
        [Key]
        public int AssociationId {get; set;}

        [Required]
        public int ProductId {get; set;}
        public Product Products {get; set;}

        [Required]
        public int CategoryId {get; set;}
        public Category Categories {get; set;}
    }
}