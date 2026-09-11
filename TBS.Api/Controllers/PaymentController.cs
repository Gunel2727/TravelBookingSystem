using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TBS.Application.Common;
using TBS.Application.DTOs.Payment;
using TBS.Application.Interfaces;

namespace TBS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPayments(CancellationToken cancellationToken)
        {
            var payments = await _paymentService.GetAllAsync(cancellationToken);
            return Ok(ResponseModel<IList<PaymentDto>>.Ok(payments));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetPaymentById(Guid id, CancellationToken cancellationToken)
        {
            var payment = await _paymentService.GetByIdAsync(id,cancellationToken);
            return Ok(ResponseModel<PaymentDto>.Ok(payment));
        }

        [HttpPost]
        public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentDto dto, CancellationToken cancellationToken)
        {
            var payment = await _paymentService.AddAsync(dto, cancellationToken);
            return StatusCode(201, ResponseModel<PaymentDto>.Created(payment));
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdatePayment(Guid id, [FromBody] UpdatePaymentDto dto, CancellationToken cancellationToken)
        {
            var payment = await _paymentService.UpdateAsync(id, dto, cancellationToken);
            return Ok(ResponseModel<PaymentDto>.Ok(payment));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeletePayment(Guid id, CancellationToken cancellationToken)
        {
            await _paymentService.DeleteAsync(id, cancellationToken);
            return Ok(ResponseModel<bool>.Ok(true));
        }



    }
}
