using MediatR;

namespace Application.Commands.BookService
{
    public class CreateBookCommand : IRequest<int>
    {
        public int Id  { get; set; }
    
        
        public string Author { get; set; }
        
    
        
        public string Name { get; set; }
        
    
    }
}
