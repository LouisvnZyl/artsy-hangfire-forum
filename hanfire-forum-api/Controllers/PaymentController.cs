using HangfireForum.Domain.Common.Requests;
using Microsoft.AspNetCore.Mvc;

namespace hanfire_forum_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : Controller
    {
        [HttpPost]
        [Route('payments')]
        public Task<IActionResult> SubmitPayment([FromBody] PaymentRequest paymentRequest)
        {

        }
    }
}
