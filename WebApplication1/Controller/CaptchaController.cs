using Microsoft.AspNetCore.Mvc;
using WebApplication1.Classes;
using System;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CaptchaController : ControllerBase
    {
        private static string _captchaText;

        [HttpGet("text")]
        public IActionResult GetCaptchaText()
        {
            var captcha = new CaptchaGenerate();
            _captchaText = captcha.Text;

           
            return Ok(new { captchaText = _captchaText });
        }

        [HttpPost("verify")]
        public IActionResult VerifyCaptcha([FromBody] string userInput)
        {
            if (string.IsNullOrEmpty(_captchaText))
            {
                return BadRequest("Время для написания каптчи истекло");
            }

            if (userInput.Equals(_captchaText, StringComparison.OrdinalIgnoreCase))
            {
                _captchaText = null; 
                return Ok("Каптча совпала");
            }

            return BadRequest("Каптча введа неверно");
        }
    }
}
