using Microsoft.AspNetCore.Mvc;
using MyApp.Api.Models;

namespace MyApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var messages = new List<Message>
            {
                new Message { Id = 1, Text = "Привет, Андрей!" },
                new Message { Id = 2, Text = "Это ответ от сервера." }
            };

            return Ok(messages);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Message message)
        {
            return Ok(new { status = "Сообщение получено", data = message });
        }
    }
}
