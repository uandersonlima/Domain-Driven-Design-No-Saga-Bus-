using AscoreStore.Catalog.Application.ViewModels;
using AscoreStore.Catalog.Domain.ProductAggregate;
using AutoMapper;

namespace AscoreStore.Catalog.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ProductViewModel, Product>()
                .ConstructUsing(p =>
                    new Product(p.Name, p.Description, p.isActivated,
                        p.Value, p.CreateDate,
                        new Dimensions(p.Height, p.Width, p.Depth), p.CategoryId, new Image(p.Image.Name, p.Image.ContentType, p.Image.Size, p.Image.Data)));

            CreateMap<CategoryViewModel, Category>()
                .ConstructUsing(c => new Category(c.Name, c.Code));


            CreateMap<ImageViewModel, Image>()
                .ConstructUsing(c => new Image(c.Name, c.ContentType, c.Size, c.Data));
        }
    }
}