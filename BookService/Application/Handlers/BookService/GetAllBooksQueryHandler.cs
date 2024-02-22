using AutoMapper;
using MediatR;
using Application.Queries.BookService;
using Application.Responses;
using Core.Repositories;

namespace Application.Handlers.BookService
{
    public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, List<BookResponse>>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        public GetAllBooksQueryHandler(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }
        public async Task<List<BookResponse>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            var generatedBook = await _bookRepository.GetAllAsync();
            var bookEntity = _mapper.Map<List<BookResponse>>(generatedBook);
            return bookEntity;
        }
    }
}
