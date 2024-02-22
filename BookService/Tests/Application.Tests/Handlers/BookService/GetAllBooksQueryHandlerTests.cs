using AutoMapper;
using Moq;
using Application.Handlers.BookService;
using Application.Queries.BookService;
using Application.Responses;
using Core.Entities;
using Core.Repositories;

namespace Application.Tests.Handlers.BookService
{
    public class GetAllBooksQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ReturnsListOfBookResponses()
        {
            // Arrange
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Book, BookResponse>();
            });

            var mapper = new Mapper(mapperConfig);

            var obj = new List<Book> 
        {
            new Book { Id = 1 },
            new Book { Id = 2 }

        };

            var RepositoryMock = new Mock<IBookRepository>();
            RepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(obj);

            var query = new GetAllBooksQuery();
            var handler = new GetAllBooksQueryHandler(RepositoryMock.Object, mapper);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<BookResponse>>(result);
            Assert.Equal(obj.Count, result.Count);
           
        }
    }
}
