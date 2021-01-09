using System.Collections.Generic;
using System.Threading.Tasks;
using CqrsServices;
using CqrsServices.Models;
using CqrsServices.Orders.Command;
using CqrsServices.Orders.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CqrsSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        private const string Json = "application/json";

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet, Route("get-all")]
        [Produces(Json)]
        public async Task<ActionResult<IList<OrderModel>>> GetAllOrders()
        {
            var result = await _mediator.Send(new GetAllOrdersQuery());
            return Ok(result);
        }

        [HttpGet, Route("get-byId")]
        [Produces(Json)]
        public async Task<ActionResult<OrderModel>> GetOrderById(int id)
        {
            var result = await _mediator.Send(new GetOrderByIdQuery(id));
            return Ok(result);
        }

        [HttpPost, Route("create")]
        [Produces(Json)]
        public async Task<ActionResult<Response<int>>> CreateOrder([FromBody] CreateOrderCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}