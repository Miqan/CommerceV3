using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CommerceV3.Models
{
    public class MenuItem
    {
		public string Id { get; set; }
        [Display(Name = "Menü Öğesi Adı")]
        [StringLength(200)]
        public string Name { get; set; }
        [Display(Name = "Bağlantı")]
        [StringLength(200)]
        public string Url { get; set; }
        [Display(Name = "Hedef")]
        [StringLength(200)]
        public string Target { get; set; }
        [Display(Name = "Yayında Mı?")]
        public bool IsPublished { get; set; }
        [Display(Name = "Pozisyon")]
        public int Position { get; set; }
        [Display(Name = "Üst Menü Öğesi")]
        public string ParentMenuItemId { get; set; }
		[ForeignKey("ParentMenuItemId")]
        [Display(Name = "Üst Menü Öğesi")]
        public MenuItem ParentMenuItem { get; set; }
		public virtual ICollection<MenuItem> ChildMenuItems { get; set; }
    }
}
