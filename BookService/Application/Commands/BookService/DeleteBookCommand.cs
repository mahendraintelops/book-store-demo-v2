using MediatR;

namespace Application.Commands.BookService
{
    public class DeleteBookCommand : IRequest
    {
        public int Id { get; set; }
    }
}
