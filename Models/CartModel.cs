using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Lab2.Models
{



    public class CartAdressModel{
        [RegularExpression("[A-Za-z0-9\\-\\.]{1,50}")]
        [DisplayName("Ulica")]
        public string Streat { get; set; } = "";

        [RegularExpression("[0-9]{1,4}[a-zA-Z]{0,4}")]
        [DisplayName("Nr. Domu")]
        public string HomeNo { get; set; } = "";
        [RegularExpression("[0-9]{2}[0-9]{3}")]
        [DisplayName("Kod Pocztowy")]
        public string ZipCode { get; set; } = "";
        [Phone]
        [DisplayName("Telefon")]
        public string PhoneNo { get; set; } = "";
        [EmailAddress]
        [DisplayName("Email")]
        public string Email { get; set; } = "";

    }


    public class CartModel: CartAdressModel
    {
        [Key]
        public int Id { get; set; }

        public virtual IList<CartItemModel> Products { get; set; } = new List<CartItemModel>();

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual IdentityUser User {get;set;}
    
        public DateTime? OrderDate { get; set; } = null;
    }

    public class CartItemModel
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual ProductModel Product { get; set; }


        [DisplayName("Ilość")]
        public float Amount { get; set; }
        [DisplayName("Nazwa")]
        public string Title { get => Product != null ? Product.Title : ""; }
        [DisplayName("Cena")]
        public float Price { get => Product != null ? Product.Price : 0; }
        [DisplayName("Wartość")]
        public float Value { get => Product != null ? Product.Price * Amount : 0; }
    }
}
