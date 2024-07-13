using E_commercial_Web_RESTAPI.Helpers;
using E_commercial_Web_RESTAPI.Interfaces;
using E_commercial_Web_RESTAPI.Models;
using E_commercial_Web_RESTAPI.Repositories;

namespace E_commercial_Web_RESTAPI.Services
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CartService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResponse> AddItemToCart(string UserId, long productId, int quantity)
        {
            var cart = _unitOfWork._cart_repository.GetCart(UserId) ?? _unitOfWork._cart_repository.CreateCart(UserId);

            var product = _unitOfWork._product_repository.GetProductById(productId);

            if (product is null)
            {
                return new ApiResponse
                {
                    Success = true,
                    Message = $"Product is not found ",
                    StatusCode = 404
                };


            }
            if (quantity > product.Quantity)
            {
                return new ApiResponse
                {
                    Success = true,
                    Message = $"Quantity is exceeded",
                    StatusCode = 400
                };

            }
            else
            {
                

                CartItem? item = _unitOfWork._cartItem_repository.GetAllItems(UserId).FirstOrDefault(k => k.ProductId == productId);


                if (item is null)
                {
                    item = new CartItem()
                    {
                        CartId = cart.Id,
                        UserId = UserId,
                        ProductId = productId,
                        Quantity = quantity,
                        ProductName = product.Name,
                        Price = product.price,
                        imageurl = product.imageurl
                    };

                    _unitOfWork._cartItem_repository.Add(item);


                }



                else
                {
                    return new ApiResponse
                    {
                        Success = false,
                        Message = $"Product with {productId} has been already added to the cart",
                        StatusCode = 400
                    };

                }
                product.Quantity -= quantity;




            }
            await _unitOfWork.CommitChanges();
            return new ApiResponse
            {
                Success = true,
                Message = $"Product has been added to the cart",
                StatusCode = 200
            };


        }

        public async Task<ApiResponse> UpdateCartItem(CartItem Item)
        {
            //var check_item = _unitOfWork._cartItem_repository.GetByIdAsync(Item.Id);
            var cart = _unitOfWork._cart_repository.GetByIdAsync(Item.CartId);
            if (cart is null)
            {

                return new ApiResponse
                {
                    Success = false,
                    Message = $"This cart is not available ",
                    StatusCode = 404
                };
            }
            CartItem updated_item = new CartItem()
            {
                ProductId = Item.ProductId,
                UserId = Item.UserId,
                Quantity = Item.Quantity,
                ProductName = Item.ProductName,
                Price = Item.Price,
                CartId = cart.Id,

            };
            _unitOfWork._cartItem_repository.Update(updated_item);
            await _unitOfWork.CommitChanges();
            return new ApiResponse
            {
                Success = true,
                Message = $"cartItem has been updated and added to the cart",
                StatusCode = 200
            };






        }

        public async Task<ApiResponse> UpdateCartItemsQuantityAsync(string? UserId, long productId, int quantity)
        {
            var product = _unitOfWork._product_repository.GetProductById(productId);
            CartItem? cartItem = _unitOfWork._cartItem_repository.GetAllItems(UserId).FirstOrDefault(k => k.ProductId == productId);
          
            if (product is not null || cartItem is not null && product.Quantity >= quantity)
            {
                cartItem.Quantity += quantity;
                product.Quantity -= quantity;
                _unitOfWork._cartItem_repository.Update(cartItem);
                await _unitOfWork.CommitChanges();
                return new ApiResponse
                {
                    Success = true,
                    Message = $"{quantity} with item {product.Id} has been added to the cart",
                    StatusCode = 200
                };

            }
            else
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = $"There was an error in trying to add more quantities of the product",
                    StatusCode = 404
                };



            }
        }



    }
      
}

