using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCrud.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [ForeignKey("ClientId")]
        public int ClientId { get; set; }
        [Required]
        [DisplayName("Product Name")]
        public string NameProduct { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

    }
}
