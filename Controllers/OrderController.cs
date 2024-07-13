using AutoMapper;
using E_commercial_Web_RESTAPI.DTOS.Order;
using E_commercial_Web_RESTAPI.Helpers;
using E_commercial_Web_RESTAPI.Interfaces;
using E_commercial_Web_RESTAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace E_commercial_Web_RESTAPI.Controllers
{
    public class OrderController : APIBaseController
    {
        private readonly IOrder _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrder orderService, IMapper mapper)
        {
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost]
        public async Task<ActionResult<OrderDTO>> CreateOrder([FromBody] OrderRequestDTO orderDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var order = await _orderService.PlaceOrderAsync(orderDto.UserId,
                 orderDto.Address);

            if (order == null) return BadRequest(new ApiResponse()
            { Success = false, Message = "Problem Creating Order", StatusCode = 400 });

            return Ok(order);
        }

        [HttpGet("/ordersForUser/")]
        public async Task<ActionResult<IReadOnlyList<OrderDTO>>> GetOrdersForUser(string UserId)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var orders = await _orderService.GetOrderForUserAsync(UserId);
            return Ok(_mapper.Map<IReadOnlyList<OrderDTO>>(orders));
        }

        [HttpGet("{OrderId:long}")]
        public async Task<ActionResult<OrderDTO>> GetOrderByIdForUser(long OrderId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var order = await _orderService.GetOrderByIdAsync(OrderId);

            if (order == null) return NotFound(new ApiResponse() { Message = "No Order is found With this OrderId ", StatusCode = 404 });

            return Ok(_mapper.Map<OrderDTO>(order));
        }

        //[HttpGet]

        //public async Task<ActionResult<IReadOnlyList<OrderDTO>>> GetAllOrders()
        //{
        //    var orders = await _orderService.GetAllOrders();
        //    if (orders is null) return NotFound(new ApiResponse() { StatusCode = 404 });
        //    return Ok(_mapper.Map<IReadOnlyList<OrderDTO>>(orders));
        //}



    }
}
