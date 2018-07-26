using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CommerceV3.Models
{
    public enum RegionType
    {
        [Display(Name = "Ülke")]
        Country = 1,
        [Display(Name = "Şehir")]
        City = 2,
        [Display(Name = "İlçe")]
        Town = 3,
        [Display(Name = "Mahalle")]
        HomeTown = 4
    }
}
