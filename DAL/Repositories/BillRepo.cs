using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Core.Models;
using RestaurantManagement.DAL.Data;
using RestaurantManagement.DAL.Interfaces;

namespace RestaurantManagement.DAL.Repositories
{
    public class BillRepo(RestaurantDBContext context) : IBillRepo
    {
        private readonly RestaurantDBContext _context = context;

        public async Task GenerateBillAsync(Bill bill)
        {
            await _context.Bill.AddAsync(bill);
            var table = await _context.Tablespace.FirstOrDefaultAsync(t => t.Id == bill.TablespaceId);
            table!.IsOccupied = false;
            var orders = await _context.Order.Where(o => o.TablespaceId == bill.TablespaceId).ToListAsync();
            foreach(var order in orders)
            {
                order.Status = "Paid";
            }
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Bill>> GetAllBillsAsync()
        {
            return await _context.Bill.AsNoTracking().OrderByDescending(b => b.Timestamp).ToListAsync();
        }

        public async Task<Bill?> GetBillAsync(int id)
        {
            return await _context.Bill
                .Include(t => t.Tablespace)
                .Include(t => t.Orders)
                .ThenInclude(o => o.OrderItems)
                .ThenInclude(oi => oi.MenuItem)
                .FirstOrDefaultAsync(b => b.Id == id);
        }
    }
}
