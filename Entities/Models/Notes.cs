using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Notes : Base
    {
        [Column(TypeName = "text")] // Veritabanında text olarak saklar
        public string Suggestion { get; set; }
        public int ProductId { get; set; }
        public Products? Products { get; set; }
        public string RequestCreatorId { get; set; }
        public DateTime RequestCreateTime { get; set; } = DateTime.Now;
    }
}
