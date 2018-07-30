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
        [StringLength(200)]
        public string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        
    }
}
