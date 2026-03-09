using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantManagement.Core.Models;

namespace RestaurantManagement.DAL.Interfaces
{
    public interface IBillRepo
    {
        public Task<IEnumerable<Bill>> GetAllBillsAsync();
        public Task<Bill?> GetBillAsync(int tablespaceId);
        public Task GenerateBillAsync(Bill bill);
    }
}
