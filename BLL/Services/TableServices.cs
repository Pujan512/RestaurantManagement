using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RestaurantManagement.BLL.Interfaces;
using RestaurantManagement.Core.Models;
using RestaurantManagement.Core.ViewModels.Tablespace;
using RestaurantManagement.DAL.Interfaces;

namespace RestaurantManagement.BLL.Services
{
    public class TableServices(ITableRepo tableRepo, IGenericRepository<Tablespace> genericTablespaceRepo, IMapper mapper) : ITableServices
    {
        private readonly ITableRepo _tableRepo = tableRepo;
        private readonly IGenericRepository<Tablespace> _genericTablespaceRepo = genericTablespaceRepo;
        private readonly IMapper _mapper = mapper;
        public async Task<bool> AddTableAsync(AddTableVM addTableVM)
        {
            Tablespace table = new()
            {
                Capacity = addTableVM.Capacity,
                TableNumber = await GetTableCount() + 1,
                IsOccupied = addTableVM.IsOccupied,
            };

            try
            {
                await _genericTablespaceRepo.AddAsync(table);
                return true;
            }
            catch
            {
                throw new Exception("Unable to create tablespace");
            }
        }

        public async Task<bool> DeleteTableAsync(int id)
        {
            try
            {
                var table = await GetTableAsync(id);
                if (table == null) throw new Exception("Table not found");
                await _genericTablespaceRepo.DeleteAsync(id);
                return true;
            }
            catch
            {
                throw new Exception("Unable to delete tablespace from DB");
            }

        }

        public async Task<TablespaceVM?> GetTableAsync(int id)
        {
            try
            {
                var tablespace = await _genericTablespaceRepo.GetByIdAsync(id);
                return _mapper.Map<TablespaceVM>(tablespace);
            }
            catch
            {
                throw new Exception("Unable to get tablespace from db");
            }

        }

        public async Task<IEnumerable<TablespaceVM>> GetTablesAsync()
        {
            try
            {
                var tablespaces = await _genericTablespaceRepo.GetAllAsync();
                return _mapper.Map<IEnumerable<TablespaceVM>>(tablespaces);

            }
            catch
            {
                throw new Exception("Unable to get tablespaces from db");
            }
        }

        public async Task<bool> UpdateTableAsync(int id, EditTableVM editTableVM)
        {
            var table = await _genericTablespaceRepo.GetByIdAsync(id);
            if (table == null) throw new Exception("Tablespace not found");
            if (id != editTableVM.Id) throw new Exception("Tablespace id doesn't match");

            table.IsOccupied = editTableVM.IsOccupied == null ? table.IsOccupied : editTableVM.IsOccupied;
            table.Capacity = editTableVM.Capacity;

            try
            {
                await _genericTablespaceRepo.EditAsync();
                return true;
            }
            catch
            {
                throw new Exception("Unable to update table in db");
            }
        }

        public EditTableVM EditVMMapper(TablespaceVM tableVM)
        {
            return new EditTableVM()
            {
                Id = tableVM.Id,
                Capacity = tableVM.Capacity,
                IsOccupied = tableVM.IsOccupied
            };
        }

        public async Task<TablespaceVM?> GetTableWithOrdersAsync(int id)
        {
            try
            {
                var tablespace = await _tableRepo.GetTableWithOrdersAsync(id);
                return _mapper.Map<TablespaceVM>(tablespace);
            }
            catch
            {
                throw new Exception("Unable to get tablespaces with orders from db");
            }
        }

        public async Task<TablespaceVM?> GetTableWithMenuItemsAsync(int id)
        {
            try
            {
                var tablespace = await _tableRepo.GetTableWithMenuItemsAsync(id);
                return _mapper.Map<TablespaceVM>(tablespace);

            }
            catch
            {
                throw new Exception("Unable to get tablespaces with orders from db");
            }
        }

        public async Task<int> GetTableCount() {
            var tables = await _genericTablespaceRepo.GetAllAsync();
            return tables.Count();
        }
    }
}
