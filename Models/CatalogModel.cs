using System.ComponentModel.DataAnnotations;

namespace Lab2.Models
{
    public class CatalogModel
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public virtual IList<ProductModel> Products { get; set; } = new List<ProductModel>();
    }
}