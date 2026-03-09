using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantManagement.Core.Models;
using RestaurantManagement.Core.ViewModels.Bill;

namespace RestaurantManagement.BLL.Interfaces
{
    public interface IBillServices
    {
        public Task<IEnumerable<BillVM>> GetBillsAsync();
        public Task<BillVM?> GetBillAsync(int tablespaceId);
        public Task<BillVM> CreateBillAsync(int tablespaceId);
        public Task<int> AddBillAsync(int TablespaceId, CreateBillVM createBillVM);
    }
}
