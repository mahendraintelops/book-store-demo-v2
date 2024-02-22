using AutoMapper;
using MediatR;
using Application.Queries.BookService;
using Application.Responses;
using Core.Repositories;

namespace Application.Handlers.BookService
{
    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, BookResponse>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        public GetBookByIdQueryHandler(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }
        public async Task<BookResponse> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var generatedBook = await _bookRepository.GetByIdAsync(request.id);
            var bookEntity = _mapper.Map<BookResponse>(generatedBook);
            return bookEntity;
        }
    }
}
