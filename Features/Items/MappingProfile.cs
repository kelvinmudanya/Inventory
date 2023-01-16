using AutoMapper;
using Inventory.Domain;

namespace Inventory.Features.Items
{
 public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Create.Command, Item>();
            CreateMap<Item, List.ItemsViewModel>();
            CreateMap<Item, Details.ItemsViewModel>()
                .ForMember(x => x.CategoryName, y => y.MapFrom(z => z.Category.Name));
            CreateMap<Update.Command, Item>();

               
        }
    }
}
