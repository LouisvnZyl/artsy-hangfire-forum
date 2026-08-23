using HangfireForum.Domain.Common.Requests;
using HangfireForum.Services.PaymentService;
using Microsoft.AspNetCore.Mvc;

namespace hanfire_forum_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            this._paymentService = paymentService;
        }

        [HttpPost]
        [Route("payments")]
        public async Task<IActionResult> SubmitPayment(
        [FromBody] PaymentRequest paymentRequest)
        {
            var paymentResult = await _paymentService.ProcessPayment(paymentRequest);

            if (paymentResult.IsError)
            {
                return Problem(
                    statusCode: StatusCodes.Status400BadRequest,
                    title: "Payment processing failed",
                    detail: paymentResult.FirstError.Description);
            }

            return Ok();
        }
    }
}
