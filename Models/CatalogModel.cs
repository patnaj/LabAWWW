using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Lab2.Models
{
    public class CatalogModel
    {
        [Key]
        [HiddenInput]
        public int Id { get; set; }
        [DisplayName("Nazwa")]
        public string Title { get; set; } = "";
        public virtual IList<ProductModel> Products { get; set; } = new List<ProductModel>();
    }
}