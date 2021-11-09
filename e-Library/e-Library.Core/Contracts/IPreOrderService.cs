using e_Library.Core.Models;
using e_Library.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Library.Core.Contracts
{
    public interface IPreOrderService
    {
        void CreatePreOrder(PreOrder baseOrder);
        List<PreOrder> GetPreOrderList();
        PreOrder GetPreOrder(string Id);
        void UpdatePreOrder(PreOrder updatedOrder);

    }
}
