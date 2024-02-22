using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Application.Commands.BookService;
using Application.Handlers.BookService;
using Core.Entities;
using Core.Repositories;

namespace Application.Tests.Handlers.BookService
{
    public class CreateBookCommandHandlerTests
    {
        private readonly Mock<IBookRepository> _bookRepository;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<ILogger<CreateBookCommandHandler>> _logger;

        public CreateBookCommandHandlerTests()
        {
            _bookRepository = new();
            _mapper = new();
            _logger = new();
        }

        [Fact]
        public async Task Handle_ReturnsId()
        {
            // Arrange
            var request = new CreateBookCommand(); // Create a request object as needed

            _mapper
                .Setup(m => m.Map<Book>(request))
                .Returns(new Book()); 

            _bookRepository
                .Setup(r => r.AddAsync(It.IsAny<Book>()))
                .ReturnsAsync(new Book { Id = 123 }); 

            var loggerMock = new Mock<ILogger<CreateBookCommandHandler>>();
            var handler = new CreateBookCommandHandler(_bookRepository.Object, _mapper.Object, loggerMock.Object);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Equal(123, result); 
        }
    }
}
