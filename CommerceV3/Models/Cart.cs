using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CommerceV3.Models
{
    public class Cart
    {
		public Cart()
		{
			IsActive = true;
		}
		public string Id { get; set; }
        [Display(Name = "Sahip")]
        [StringLength(100)]
        public string Owner { get; set; }
		public virtual ICollection<CartItem> CartItems { get; set; }
        [Display(Name = "Oluşturan Kullanıcı")]
		public string CreatedBy { get; set; }
        [Display(Name = "Oluşturulma Tarihi")]
		public DateTime CreateDate { get; set; }
        [Display(Name = "Güncelleyen Kullanıcı")]
		public string UpdatedBy { get; set; }
        [Display(Name = "Güncellenme Tarihi")]
		public DateTime UpdateDate { get; set; }

		public bool IsActive { get; set; }
    }
}
