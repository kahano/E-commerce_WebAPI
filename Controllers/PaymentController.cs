using E_commercial_Web_RESTAPI.Data;
using E_commercial_Web_RESTAPI.DTOS.Payments;
using E_commercial_Web_RESTAPI.Helpers;
using E_commercial_Web_RESTAPI.Mapper;
using E_commercial_Web_RESTAPI.Models;
using E_commercial_Web_RESTAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace E_commercial_Web_RESTAPI.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class PaymentController : ControllerBase
    {
        private readonly ApplicationDBcontext _context;
        private readonly IPaymentRepository _paymentRepository;



        public PaymentController(ApplicationDBcontext context, IPaymentRepository paymentRepository)
        {
            _context = context;
            _paymentRepository = paymentRepository;


        }
        [HttpPost]
        [Route("{customerId:long}")]


        public async Task<IActionResult> ChargeCard([FromRoute] long customerId, PaymentRequestDTO paymentdto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var paymentModel = paymentdto.ToPaymentFromRequestDTO();
            var response = await _paymentRepository.InsertPayment(customerId, paymentModel);

            if (response.Success)
            {
                return Ok(response);
            }

            return StatusCode(response.StatusCode, response);

        }




        [HttpGet("{Id:long}")]
        public async Task<IActionResult> GetPaymentByIdAsync(long Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var payment = await _paymentRepository.GetPaymentById(Id);

            if (payment == null)
            {
                return NotFound();
            }
            var paymentModel = payment.ToPaymentDTO();
            return Ok(paymentModel);

        }

        [HttpGet]

        public async Task<IActionResult> GetAllPayments([FromQuery]PaymentQueryObject query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var getpayments = await _paymentRepository.GetAllPaymentsByCustomer(query);
            if (getpayments == null || !getpayments.Any())
            {
                return NotFound("No payments found");
            }
            var AllPayments = getpayments.Select(p => p.ToPaymentDTO()).ToList();
            return Ok(AllPayments);

        }


    }
}
