using AutoMapper;
using E_commercial_Web_RESTAPI.DTOS.BasketITem;
using E_commercial_Web_RESTAPI.Helpers;
using E_commercial_Web_RESTAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace E_commercial_Web_RESTAPI.Controllers
{
    public class CartController : APIBaseController
    {
        private readonly IMapper _mapper;
        private readonly ICartService _cartService;
        public CartController(IMapper mapper, ICartService cartService)
        {
            _mapper = mapper;
            _cartService = cartService;
        }

        [HttpPost]

        public async Task<ActionResult> AddToCart(CartItemRequestDTO Cartdto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cart =  await _cartService.AddItemToCart(Cartdto.UserId,Cartdto.ProductId,Cartdto.Quantity);

            if (cart == null) return BadRequest(new ApiResponse()
            { Success = false, Message = "Problem Adding to Cart", StatusCode = 400 });

            return Ok(cart);


        }

        [HttpPatch]

        public async Task<ActionResult> UpdateCartItem(CartItemRequestDTO Cartdto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cart = await _cartService.UpdateCartItemsQuantityAsync(Cartdto.UserId, Cartdto.ProductId,Cartdto.Quantity);
            if (cart == null) return BadRequest(new ApiResponse()
            { Success = false, Message = "Problem updating the Cart", StatusCode = 400 });

            return Ok(cart);
        }

    }
}
