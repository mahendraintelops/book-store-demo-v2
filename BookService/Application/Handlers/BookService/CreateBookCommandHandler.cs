using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Application.Commands.BookService;
using Core.Entities;
using Core.Repositories;

namespace Application.Handlers.BookService
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, int>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateBookCommandHandler> _logger;

        public CreateBookCommandHandler(IBookRepository bookRepository, IMapper mapper, ILogger<CreateBookCommandHandler> logger)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var bookEntity = _mapper.Map<Book>(request);

            /*****************************************************************************/
            var generatedBook = await _bookRepository.AddAsync(bookEntity);
            /*****************************************************************************/
            _logger.LogInformation($" {generatedBook} successfully created.");
            return generatedBook.Id;
        }
    }
}
