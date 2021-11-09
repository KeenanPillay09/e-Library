using e_Library.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Library.Core.ViewModels
{
    public class PreOrderStatusListViewModel
    {
        public IEnumerable<PreOrder> Orders { get; set; }
        public IEnumerable<PreOrderStatusModel> OrderStatusModels { get; set; }
    }
}
