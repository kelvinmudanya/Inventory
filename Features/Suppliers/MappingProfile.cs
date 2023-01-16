using AutoMapper;
using Inventory.Domain;
using Inventory.Domain.Dto;

namespace Inventory.Features.Suppliers
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Create.Command, Supplier>();
            CreateMap<ContactPersonDto, ContactPerson>();
            CreateMap<Supplier, List.SupplierViewModel>();
            CreateMap<Update.Command, Supplier>();
            CreateMap<Supplier, SupplierOptions.SupplierViewModel>()
                .ForMember(x => x.Label, y => y.MapFrom(z => z.Name))
                .ForMember(x => x.Value, y => y.MapFrom(z => z.EntityGuid));
            CreateMap<Supplier, Details.SupplierViewModel>();
            CreateMap<ContactPerson, ContactPersonDto>();


        }
    }
}
