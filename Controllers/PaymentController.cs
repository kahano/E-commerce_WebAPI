using E_commercial_Web_RESTAPI.Data;
using E_commercial_Web_RESTAPI.DTOS;
using E_commercial_Web_RESTAPI.Mapper;
using E_commercial_Web_RESTAPI.Models.Payment.Payment;
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
     

        public async Task<IActionResult> ChargeCard([FromRoute]long customerId, PaymentRequestDTO paymentdto)
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

    


        [HttpGet("{CustomerId}")]
        public async Task<IActionResult> GetPaymentByIdAsync(long CustomerId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var payment = await _paymentRepository.GetPaymentById(CustomerId);

            if (payment == null)
            {
                return NotFound();
            }
            var paymentModel = payment.ToPaymentDTO();
            return Ok(paymentModel);

        }

        [HttpGet]

        public async Task<IActionResult> GetAllPayments()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var getpayments = await _paymentRepository.GetAllPaymentsByCustomers();
            var AllPayments = getpayments.Select(p => p.ToPaymentDTO()).ToList();
            return Ok(AllPayments);

        }


    }
}
