using AutoMapper;
using Moq;
using Application.Handlers.BookService;
using Application.Queries.BookService;
using Application.Responses;
using Core.Entities;
using Core.Repositories;

namespace Application.Tests.Handlers.BookService
{
    public class GetBookByIdQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ReturnsBookResponse()
        {
            // Arrange
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Book, BookResponse>();
            });

            var mapper = new Mapper(mapperConfig);

            var Id = 1; 
            var obj = new Book { Id = Id, /* other properties */ };

            var RepositoryMock = new Mock<IBookRepository>();
            RepositoryMock.Setup(repo => repo.GetByIdAsync(Id)).ReturnsAsync(obj);

            var query = new GetBookByIdQuery(Id);
            var handler = new GetBookByIdQueryHandler(RepositoryMock.Object, mapper);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BookResponse>(result);
            // Add assertions to check the mapping and properties 
            Assert.Equal(Id, result.Id);
            // Add more assertions as needed
        }
    }
}
