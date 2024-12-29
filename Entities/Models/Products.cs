using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Products : Base
    {
        public String Name { get; set; }
        public String SurName { get; set; }
        public String? Address { get; set; }
		public int? ReferanceId { get; set; }
        public Referance? Referance { get; set; }
        public int? EducationStatusId { get; set; }
		public EducationStatus? EducationStatus { get; set; }

		[RegularExpression(@"^\+?\d{11,11}$", ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
        public string PhoneNumber { get; set; }
        public bool ShowCase { get; set; }

        public int? CategoryId { get; set; }
        public Categories? Categories { get; set; }

    }
}
