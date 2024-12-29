using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.RequestParameters
{
    public class ProductRequestParameters : RequestParameters
    {
        public String Name { get; set; }
        public String SurName { get; set; }
        public String? Address { get; set; } = String.Empty;
        public int? PhoneNumber { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public ProductRequestParameters() : this(1,6)
        {
            
        }
        public ProductRequestParameters(int pageNumber=1, int pageSize=6)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
