
using Microsoft.AspNetCore.Mvc;
using NotificationService.API.Models;
using NotificationService.API.Services;

namespace NotificationService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationsController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public NotificationsController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> Send(
            NotificationRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Email))
            {
                return BadRequest("Email is required.");
            }

            var subject = "MiniShop - Order Successful";

            var body = $@"
Order ugurla yaradildi!

Order ID: {request.OrderId}
Mehsul: {request.ProductName}
Miqdar: {request.Quantity}
Umumi mebleg: {request.TotalPrice} AZN

Cox sagolun!";

            await _emailService.SendEmailAsync(
                request.Email,
                subject,
                body);

            return Ok(new
            {
                message = "Email notification sent successfully."
            });
        }
    }
}

