using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public record ProductsDto 
    {
        public int Id { get; init; }

        [Required(ErrorMessage = "Name is required")]
        public String Name { get; set; }

        [Required(ErrorMessage = "SurName is required")]
        public String SurName { get; set; }
        public String? Address { get; set; } = String.Empty;
        public int? ReferanceId { get; set; }
        public int? EducationStatusId { get; set; }

        [RegularExpression(@"^\+?\d{11,11}$", ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
        public string PhoneNumber { get; set; }
        public bool ShowCase { get; set; }
        public int? CategoryId { get; set; }
    }
}
