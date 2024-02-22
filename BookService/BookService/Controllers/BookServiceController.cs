using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Commands.BookService;
using Application.Queries.BookService;
using Application.Responses;
using System.Net;


namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookServiceController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<BookServiceController> _logger;
        public BookServiceController(IMediator mediator, ILogger<BookServiceController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        
        [HttpPost(Name = "CreateBook")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CreateBook([FromBody] CreateBookCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        

        
        [HttpGet(Name = "GetAllBooks")]
        [ProducesResponseType(typeof(IEnumerable<List<BookResponse>>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<BookResponse>>> GetAllBooks(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetAllBooksQuery(), cancellationToken);
            return Ok(response);
        }
        

        
        [HttpGet("{id}", Name = "GetBookById")]
        [ProducesResponseType(typeof(BookResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<BookResponse>> GetBookById(int id, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Book GET request received for ID {id}", id);
            var response = await _mediator.Send(new GetBookByIdQuery(id), cancellationToken);
            return Ok(response);
        }
        

        
        [HttpPut(Name = "UpdateBook")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateBook([FromBody] UpdateBookCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
        

        
        [HttpDelete("{id}", Name = "DeleteBook")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteBook(int id)
        {
            _logger.LogInformation("Book DELETE request received for ID {id}", id);
            var cmd = new DeleteBookCommand() { Id = id };
            await _mediator.Send(cmd);
            return NoContent();
        }
        
    }
}
