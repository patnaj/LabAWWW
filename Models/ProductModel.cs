using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab2.Models
{
    public class ProductModel
    {
        [Key]
        public int Id { get; set; }
        [RegularExpression("^[A-Z][a-z \\-0-9\"]{3,29}$")]
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public float Price { get; set; } = 0;
        public int CatlogId { get; set; }

        [ForeignKey("CatlogId")]
        public virtual CatalogModel? Catalog { get; set; } = null;
        public virtual IList<TagModel> Tags { get; set; } = new List<TagModel>();
    }
}