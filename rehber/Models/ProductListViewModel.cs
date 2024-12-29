using Entities.Models;

namespace rehber.Models
{
    public class ProductListViewModel
    {
        public IEnumerable<Products> Products { get; set; } = Enumerable.Empty<Products>();
        public Pagination Pagination { get; set; } = new();

        public IEnumerable<Categories> Categories { get; set; } = Enumerable.Empty<Categories>();

		public IEnumerable<EducationStatus> EducationStatus { get; set; } = Enumerable.Empty<EducationStatus>();

        public IEnumerable<Referance> Referance { get; set; } = Enumerable.Empty<Referance>();
        public int TotalCount => Products.Count();
        public Dictionary<int, bool> UserLastSurveyStatus { get; set; }
    }
}
