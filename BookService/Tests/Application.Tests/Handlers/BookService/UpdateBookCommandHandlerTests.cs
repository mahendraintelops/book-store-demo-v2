using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Application.Commands.BookService;
using Application.Exceptions;
using Application.Handlers.BookService;
using Core.Entities;
using Core.Repositories;

namespace Application.Tests.Handlers.BookService
{
    public class UpdateBookCommandHandlerTests
    {
        private readonly Mock<IBookRepository> _bookRepository;
        private readonly Mock<ILogger<UpdateBookCommandHandler>> _logger;
        private readonly Mock<IMapper> _mapper;

        public UpdateBookCommandHandlerTests()
        {
            _bookRepository = new();
            _logger = new();
            _mapper = new();
        }

        [Fact]
        public async Task Handle_ThrowsBookNotFoundExceptionWhenBookNotFound()
        {
            // Arrange
            var Id = 123; // Replace with the ID you want to test
            var request = new UpdateBookCommand { Id = Id }; // Create a request object

            _bookRepository
               .Setup(r => r.GetByIdAsync(Id))
                .ReturnsAsync((Book)null); // Mock the repository to return null

            var handler = new UpdateBookCommandHandler(_bookRepository.Object, _mapper.Object, _logger.Object);

            // Act and Assert
            await Assert.ThrowsAsync<BookNotFoundException>(
                async () => await handler.Handle(request, CancellationToken.None)
            );
        }
    }
}
