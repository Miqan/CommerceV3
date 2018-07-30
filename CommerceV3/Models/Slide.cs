using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CommerceV3.Models
{
    public class Slide
    {
		public string Id { get; set; }
        [Display(Name = "Slayt Adı")]
        [StringLength(200)]
        public string Name { get; set; }
        [Display(Name = "Resim")]
        [StringLength(200)]
        public string Image { get; set; }
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
    }
}
