using AutoMapper;

using Application.Commands.BookService;

using Application.Responses;
using Core.Entities;

namespace Application.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
          
            CreateMap<Book, BookResponse>().ReverseMap();
            CreateMap<Book, CreateBookCommand>().ReverseMap();
            CreateMap<Book, UpdateBookCommand>().ReverseMap();
          
        }
    }
}
