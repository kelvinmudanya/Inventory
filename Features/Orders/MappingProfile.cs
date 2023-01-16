using AutoMapper;
using Inventory.Domain;

namespace Inventory.Features.Orders
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, List.OrderViewModel>()
                .ForMember(x => x.SupplierName, y => y.MapFrom(z => z.Supplier.Name))
                 .ForMember(x => x.SupplierLocation, y => y.MapFrom(z => z.Supplier.Location));
            CreateMap<Item, Details.ItemsViewModel>();

        }
    }
}
