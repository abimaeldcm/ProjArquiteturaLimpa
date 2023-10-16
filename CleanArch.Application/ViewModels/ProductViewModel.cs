using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Application.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="The name is required")]
        [MinLength(3)]
        [MaxLength(50)]
        [DisplayName("Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "The description is required")]
        [MinLength(5)]
        [MaxLength(200)]
        [DisplayName("Description")]
        public string Description { get; set; }
        [Required(ErrorMessage = "The Price is required")]
        [Range(0.1,9999.99)]
        [DisplayName("Description")]
        public decimal Price { get; set; }
    }
}
