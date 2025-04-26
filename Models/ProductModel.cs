using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace Lab2.Models
{
    public class ProductModel
    {
        [Key]
        [HiddenInput]
        public int Id { get; set; }
        // [RegularExpression("^[A-Z][a-z \\-0-9\"]{3,29}$")]
        [RegularExpression("^[A-Z][a-z0-9 '\"-]{4,20}$")]
        [DisplayName("Tytuł")]
        public string Title { get; set; } = "";
        [DisplayName("Opis")]
        [RegularExpression("^[^<>\\//]{0,100}$")]
        public string Description { get; set; } = "";
        [DisplayName("Cena")]
        public float Price { get; set; } = 0;
        [HiddenInput]
        public int CatlogId { get; set; }

        [ForeignKey("CatlogId")]
        public virtual CatalogModel? Catalog { get; set; } = null;

        [DisplayName("Tagi")]
        [RegularExpression("^([a-zA-Z0-9]{2,10},?)*$")]
        public string? Tags { get; set; } = "";

    }
}