using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab2.Models
{
    public class ProductModel
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public int CatlogId { get; set; }

        [ForeignKey("CatlogId")]
        public virtual CatalogModel? Catalog { get; set; } = null;
        public virtual IList<TagModel> Tags { get; set; } = new List<TagModel>();
    }
}