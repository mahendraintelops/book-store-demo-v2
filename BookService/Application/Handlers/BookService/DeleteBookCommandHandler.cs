using MediatR;
using Microsoft.Extensions.Logging;
using Application.Commands.BookService;
using Application.Exceptions;
using Core.Entities;
using Core.Repositories;

namespace Application.Handlers.BookService
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand>
    {
        private readonly IBookRepository _bookRepository;
        private readonly ILogger<DeleteBookCommandHandler> _logger;

        public DeleteBookCommandHandler(IBookRepository bookRepository, ILogger<DeleteBookCommandHandler> logger)
        {
            _bookRepository = bookRepository;
            _logger = logger;
        }
        public async Task Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var bookToDelete = await _bookRepository.GetByIdAsync(request.Id);
            if (bookToDelete == null)
            {
                throw new BookNotFoundException(nameof(Book), request.Id);
            }

            await _bookRepository.DeleteAsync(bookToDelete);
            _logger.LogInformation($" Id {request.Id} is deleted successfully.");
        }
    }
}
