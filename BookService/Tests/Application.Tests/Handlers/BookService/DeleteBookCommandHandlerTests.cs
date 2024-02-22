using Microsoft.Extensions.Logging;
using Moq;
using Application.Commands.BookService;
using Application.Exceptions;
using Application.Handlers.BookService;
using Core.Entities;
using Core.Repositories;

namespace Application.Tests.Handlers.BookService
{
    public class DeleteBookCommandHandlerTests
    {
        private readonly Mock<IBookRepository> _bookRepository;
        private readonly Mock<ILogger<DeleteBookCommandHandler>> _logger;

        public DeleteBookCommandHandlerTests()
        {
            _bookRepository = new();
            _logger = new();
        }

        [Fact]
        public async Task Handle_ThrowsBookNotFoundExceptionWhenBookNotFound()
        {
            // Arrange
            var Id = 123; // Replace with the ID you want to test
            var request = new DeleteBookCommand { Id = Id }; // Create a request object

            _bookRepository
                .Setup(r => r.GetByIdAsync(Id))
                .ReturnsAsync((Book)null); // Mock the repository to return null

            var handler = new DeleteBookCommandHandler(_bookRepository.Object, _logger.Object);

            // Act and Assert
            await Assert.ThrowsAsync<BookNotFoundException>(
                async () => await handler.Handle(request, CancellationToken.None)
            );
        }
    }
}
