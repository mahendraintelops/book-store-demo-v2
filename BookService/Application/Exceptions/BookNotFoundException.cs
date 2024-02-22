namespace Application.Exceptions
{
    public class BookNotFoundException : ApplicationException
    {
        public BookNotFoundException(string name, object key) : base($"Entity {name} - {key} is not found.")
        {

        }
    }
}
