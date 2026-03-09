using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RestaurantManagement.Core.Models;
using RestaurantManagement.Core.ViewModels;
using RestaurantManagement.Core.ViewModels.Account;
using RestaurantManagement.Core.ViewModels.Bill;
using RestaurantManagement.Core.ViewModels.Category;
using RestaurantManagement.Core.ViewModels.MenuItem;
using RestaurantManagement.Core.ViewModels.Order;
using RestaurantManagement.Core.ViewModels.OrderItem;
using RestaurantManagement.Core.ViewModels.Schedule;
using RestaurantManagement.Core.ViewModels.SystemSetting;
using RestaurantManagement.Core.ViewModels.Tablespace;

namespace RestaurantManagement.Core.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserVM>().ReverseMap();
            CreateMap<Tablespace, TablespaceVM>().ReverseMap();
            CreateMap<SystemSetting, SystemSettingVM>().ReverseMap();
            CreateMap<OrderItem, OrderItemVM>().ReverseMap();
            CreateMap<Order, OrderVM>().ReverseMap();
            CreateMap<MenuItem, MenuItemVM>().ReverseMap();
            CreateMap<Category, CategoryVM>().ReverseMap();
            CreateMap<Bill, BillVM>().ReverseMap();
            CreateMap<RegisterVM, AddUserVM>().ReverseMap();
            CreateMap<ScheduleVM, EditScheduleVM>().ReverseMap();

            CreateMap<Bill, BillVM>()
                .ForMember(d => d.OrderVMs, o => o.MapFrom(src => src.Orders))
                .ReverseMap()
                .ForMember(d => d.Orders, o => o.MapFrom(src => src.OrderVMs));

            CreateMap<Category, CategoryVM>()
                .ForMember(d => d.MenuItemVMs, o => o.MapFrom(src => src.MenuItems))
                .ReverseMap()
                .ForMember(d => d.MenuItems, o => o.MapFrom(src => src.MenuItemVMs));

            CreateMap<Schedule, ScheduleVM>()
                .ForMember(d => d.UserVM, o => o.MapFrom(src => src.User))
                .ReverseMap()
                .ForMember(d => d.User, o => o.MapFrom(src => src.UserVM));

            CreateMap<OrderItem, OrderItemVM>()
               .ForMember(d => d.MenuItemVM, opt => opt.MapFrom(src => src.MenuItem))
               .ReverseMap()
               .ForMember(d => d.MenuItem, opt => opt.MapFrom(src => src.MenuItemVM));

            CreateMap<Order, OrderVM>()
                .ForMember(d => d.OrderItemVMs, opt => opt.MapFrom(src => src.OrderItems))
                .ForMember(d => d.UserVM, opt => opt.MapFrom(src => src.User))
                .ReverseMap()
                .ForMember(d => d.OrderItems, opt => opt.MapFrom(src => src.OrderItemVMs))
                .ForMember(d => d.User, opt => opt.MapFrom(src => src.UserVM));

            CreateMap<Tablespace, TablespaceVM>()
                .ForMember(d => d.OrderVMs, opt => opt.MapFrom(src => src.Orders))
                .ReverseMap()
                .ForMember(d => d.Orders, opt => opt.MapFrom(src => src.OrderVMs));
        }
    }
}
