using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace e_Library.Core.Models
{
    public class PreOrderBooks : BaseEntity
    {
        [Display(Name = "Title")]
        public string BookName { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        [Display(Name = "Genre")]
        public string Category { get; set; }
        public string Author { get; set; }
    }
}
