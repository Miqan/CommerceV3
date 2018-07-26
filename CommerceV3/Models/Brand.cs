using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CommerceV3.Models
{
    public class Brand
    {
		public string Id { get; set; }
        [Display(Name = "Marka Adı")]
		public string Name { get; set; }
        [Display(Name = "Logo")]
		public string Logo { get; set; }

		public virtual ICollection<Product> Products { get; set; }
    }
}
