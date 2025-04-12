using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.Models
{
    public class TagModel
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public virtual IList<ProductModel> Products { get; set; } = new List<ProductModel>();
    }
}