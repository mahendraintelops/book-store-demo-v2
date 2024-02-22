using MediatR;
using Application.Responses;

namespace Application.Queries.BookService
{
    public class GetAllBooksQuery : IRequest<List<BookResponse>>
    {

    }
}
