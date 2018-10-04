using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GraniteHouse.Models
{
    public class Products
    {
        public int Id { get; set; }

        public String Name { get; set; }

        public double Price { get; set; }

        public bool Avaliable { get; set; }

        public string Image { get; set; }

        public string ShadeColor { get; set; }

        [Display(Name="Product Type")]
        public int ProductTypeId { get; set; }

        [ForeignKey("ProductTypeId")]
        public int SpecialTagsID { get; set; }

        [ForeignKey("SpecialTagsID")]
        public virtual SpecialTags SpecialTags { get; set; }
    }
}
