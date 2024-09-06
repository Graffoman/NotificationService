using Microsoft.AspNetCore.Mvc;
using NotificationService.Classes;
using NotificationService.Services;
using System.Threading.Tasks;

namespace NotificationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IMailService _mail;

        public MailController(IMailService mail)
        {
            _mail = mail;
        }

        [HttpPost("sendmail")]
        public async Task<IActionResult> SendMailAsync1(MailData mailData)
        {
            bool result = await _mail.SendAsync(mailData);

            if (result)
            {
                return StatusCode(StatusCodes.Status200OK, "Mail has successfully been sent.");
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error occurred. The Mail could not be sent.");
            }
        }
    }
}
