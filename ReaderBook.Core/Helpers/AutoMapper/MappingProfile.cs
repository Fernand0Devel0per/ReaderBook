using AutoMapper;
using MongoDB.Bson;
using ReaderBook.Core.Domain.Book;
using ReaderBook.Core.Dtos.Book;
using ReaderBook.Core.Helpers.Extensions;
using ReaderBook.Core.Models.ValueObject.Book;

namespace ReaderBook.Core.Helpers.AutoMapper
{
    public class MappingProfile : Profile
    {


        public MappingProfile()
        {
          
            CreateMap<Book, BookSchema>()
               .ForMember(dest => dest.Id, opt => opt.Ignore())
               .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => (int)src.Gender))
               .ForMember(dest => dest.Pages, opt => opt.MapFrom(src => src.Pages));

            CreateMap<Book, BookSchema>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => (int)src.Gender))
                .ForMember(dest => dest.Pages, opt => opt.MapFrom(src => src.Pages))
                .ConstructUsing(dest => new BookSchema
                {
                    Id = ObjectId.GenerateNewId(),
                    Title = dest.Title,
                    Gender = (int)dest.Gender,
                    Pages = dest.Pages.Select(page => new BookSchema.Page
                    {
                        Number = page.Number,
                        Content = page.Content
                    }).ToList()
                })
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<BookSchema, BookResponse>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
               .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.ToBookGenre().ToString()));
            //.ForMember(dest => dest.Pages, opt => opt.MapFrom(src => src.Pages));

            CreateMap<BookSchema.Page, Page>()
                .ConvertUsing(src => Page.Create(src.Number, src.Content));

            CreateMap<Page, BookSchema.Page>();

            CreateMap<BookSchema.Page, PageDto>();

            CreateMap<PageDto, Page>();


        }
    }
}
