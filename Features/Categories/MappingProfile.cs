
using AutoMapper;
using Inventory.Domain;

namespace Inventory.Features.Categories
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Create.Command, Category>();
            CreateMap<Category, List.CategoryViewModel>();
            CreateMap<Update.Command, Category>();
            CreateMap<Category, Details.CategoryViewModel>();
            CreateMap<Item, Details.ItemsViewModel>();
        }
    }
}
