using AutoMapper;
using MongoDB.Bson;
using ReaderBook.Core.Domain.Book;
using ReaderBook.Core.Dtos.Book;
using ReaderBook.Core.Helpers.Enums;
using ReaderBook.Core.Helpers.Extensions;
using ReaderBook.Core.Models.ValueObject.Book;

namespace ReaderBook.Core.Helpers.AutoMapper
{
    public class MappingProfile : Profile
    {


        public MappingProfile(IMapper mapper)
        {

            CreateMap<Book, BookSchema>()
               .ForMember(dest => dest.Id, opt => opt.Ignore())
               .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => (int)src.Gender))
               .ForMember(dest => dest.Pages, opt => opt.MapFrom(src => src.Pages));

            CreateMap<BookSchema, Book>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => (BookGenre)src.Gender))
                .ForMember(dest => dest.Pages, opt => opt.MapFrom(src => src.Pages))
                .ConstructUsing(src => Book.Create(src.Title, (BookGenre)src.Gender,
                    mapper.Map<ICollection<BookSchema.Page>, ICollection<Page>>(src.Pages)))
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => false));

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
