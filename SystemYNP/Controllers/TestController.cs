using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SystemYNP.Domains;

namespace SystemYNP.Controllers
{
    [Route("test")]
    [ApiController]
    public class TestController: ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly MailService _mailService;

        public TestController(IConfiguration configuration, MailService mailService)
        {
            _configuration = configuration;
            _mailService = mailService;
        }
        
        [HttpGet("{email}")]
        public IActionResult TestMailSender(string email)
        {
            _mailService.Send(_configuration["Smtp:Mail"], email, "Ypn service notification", "Test message");
            return Ok();
        }
    }
}