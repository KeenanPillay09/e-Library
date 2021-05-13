using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Library.Core.Models
{
    public class Basket : BaseEntity
    {
        public virtual ICollection<BasketItem> BasketItems { get; set; } //Virtual because EF whenver load basket it loads items too (Lazy Loading)

        public Basket()
        {
            this.BasketItems = new List<BasketItem>();
        }
    }
}
