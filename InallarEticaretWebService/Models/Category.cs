namespace InallarEticaretWebService.Models
{
    public class Category
    {
        public string? Name { get; set; } = string.Empty;

        public List<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
    }
}
