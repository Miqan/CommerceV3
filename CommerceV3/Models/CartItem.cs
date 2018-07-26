using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CommerceV3.Models
{
    public class CartItem
    {
		public string Id { get; set; }
        [Display(Name = "Sepet")]
		public string CartId { get; set; }
        [Display(Name = "Sepet")]
        [ForeignKey("CartId")]
		public Cart Cart { get; set; }

        [Display(Name = "Ürün")]
		public string ProductId { get; set; }
		[ForeignKey("ProductId")]
        [Display(Name = "Ürün")]
        public Product Product { get; set; }

        [Display(Name = "Adet")]
		public int Quantity { get; set; }

		[NotMapped]
		public decimal TotalPrice
		{
			get
			{
				return Product.Price * Quantity;
			}
		}
    }
}
