using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Application.Commands.BookService;
using Application.Exceptions;
using Core.Entities;
using Core.Repositories;


namespace Application.Handlers.BookService
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateBookCommandHandler> _logger;

        public UpdateBookCommandHandler(IBookRepository bookRepository, IMapper mapper, ILogger<UpdateBookCommandHandler> logger)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var bookToUpdate = await _bookRepository.GetByIdAsync(request.Id);
            if (bookToUpdate == null)
            {
                throw new BookNotFoundException(nameof(Book), request.Id);
            }

            _mapper.Map(request, bookToUpdate, typeof(UpdateBookCommand), typeof(Book));
            await _bookRepository.UpdateAsync(bookToUpdate);
            _logger.LogInformation($"Book is successfully updated");
        }
    }
}
