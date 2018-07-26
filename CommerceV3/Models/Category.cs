using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CommerceV3.Models
{
    public class Category
    {
		public string Id { get; set; }
		[Display(Name = "Kategori Adı")]
		public string Name { get; set; }
		public virtual ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
