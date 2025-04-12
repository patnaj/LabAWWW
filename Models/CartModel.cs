using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.Models
{
    public class CartModel
    {
        [Key]
        public int Id { get; set; }
        [RegularExpression("[A-Za-z0-9\\-\\.]{1,50}")]
        public string Streat { get; set; } = "";

        [RegularExpression("[0-9]{1,4}[a-zA-Z]{0,4}")]
        public string HomeNo { get; set; } = "";
        [RegularExpression("[0-9]{2}[0-9]{3}")]
        public string ZipCode { get; set; }= "";
        [Phone]
        public string PhoneNo {get;set;} = "";
        [EmailAddress]
        public string Email {get;set;} = "";

        public virtual IList<CartItemModel> Products {get;set;} = new List<CartItemModel>();
    }

    public class CartItemModel
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public float Amount { get; set; }

        [ForeignKey("ProductId")]
        public virtual ProductModel Product { get; set; }
    }
}
