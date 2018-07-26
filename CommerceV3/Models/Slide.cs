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
        public string Name { get; set; }
        [Display(Name = "Resim")]
        public string Image { get; set; }
        [Display(Name = "Bağlantı")]
        public string Url { get; set; }
        [Display(Name = "Hedef")]
        public string Target { get; set; }
        [Display(Name = "Yayında Mı?")]
        public bool IsPublished { get; set; }
        [Display(Name = "Pozisyon")]
        public int Position { get; set; }
    }
}
