using MediatR;
using Application.Responses;

namespace Application.Queries.BookService
{
    public class GetBookByIdQuery : IRequest<BookResponse>
    {
        public int id { get; set; }

        public GetBookByIdQuery(int _id)
        {
            id = _id;
        }
    }
}
