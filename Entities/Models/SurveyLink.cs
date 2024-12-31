using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class SurveyLink : Base
    {
        public int SurveyId { get; set; }
        public Survey Survey { get; set; } // Survey ile ilişki
        public int UserId { get; set; } // Kullanıcıya özel
        public Products User { get; set; } // Kullanıcı modeline referans
        public string UniqueLink { get; set; } // Her kullanıcı için benzersiz link
        public bool IsUsed { get; set; } = false; // Linkin kullanılıp kullanılmadığını belirler
    }
}
