using AutoMapper;
using E_commercial_Web_RESTAPI.Data;
using E_commercial_Web_RESTAPI.DTOS.Payments;
using E_commercial_Web_RESTAPI.Helpers;
using E_commercial_Web_RESTAPI.Interfaces;
using E_commercial_Web_RESTAPI.Mapper;
using E_commercial_Web_RESTAPI.Models;
using E_commercial_Web_RESTAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Stripe;

namespace E_commercial_Web_RESTAPI.Controllers
{

    public class PaymentController : APIBaseController
    {
    
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;






        public PaymentController(IPaymentRepository paymentRepository, IMapper mapper)
        {
           
            _paymentRepository = paymentRepository;
            _mapper = mapper;
         



        }

        
        [HttpPost]
        [Route("{customerId:long},{CartId:long}")]

        //[Authorize(Roles = "Customer")]
        public async Task<IActionResult> ChargeCard([FromRoute] long customerId, long CartId, [FromBody] PaymentRequestDTO paymentdto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var paymentModel = paymentdto.ToPaymentFromRequestDTO();
            var response = await _paymentRepository.CheckOutPayment(customerId,CartId, paymentModel);

            if (response.Success)
            {
                return Ok(response);
            }

            return StatusCode(response.StatusCode, response);

        }



        //codes to review 


        [HttpGet("/payments/{Id:long}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetPaymentByIdAsync(long Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var payment = await _paymentRepository.GetPaymentById(Id);

            if (payment == null)
            {
                return NotFound("No transaction is found");
            }

            try
            {
              
                return Ok(_mapper.Map<PaymentDTO>(payment));
            }
            catch (Exception ex)
            {
                // Log the exception and return a server error response
                // e.g., _logger.LogError(ex, "An error occurred while converting payment to DTO.");
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet("/payments")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllPaymentsForCustomer([FromQuery] PaymentQueryObject query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var getpayments = await _paymentRepository.GetAllPaymentsByCustomer(query);
            if (getpayments == null || !getpayments.Any())
            {
                return NotFound("No transactions found");
            }
            try
            {

                return Ok(_mapper.Map<IReadOnlyList<PaymentDTO>>(getpayments));




            }
            catch (Exception ex)
            {
                // Log the exception and return a server error response
                // e.g., _logger.LogError(ex, "An error occurred while converting payment to DTO.");
                return StatusCode(500, ex.Message);
            }

        }



    }
}
