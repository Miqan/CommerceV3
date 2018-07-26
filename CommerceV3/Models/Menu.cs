using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CommerceV3.Models
{
    public class Menu
    {
		public string Id { get; set; }
        [Display(Name = "Menü Adı")]
        public string Name { get; set; }
		public virtual ICollection<MenuItem> MenuItems { get; set; }
    }
}
