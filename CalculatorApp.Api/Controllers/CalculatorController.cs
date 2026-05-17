using Microsoft.AspNetCore.Mvc;
using CalculatorApp.Core;

namespace CalculatorApp.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculatorController : ControllerBase
    {
        private readonly Calculator _calculator;
        private readonly ILogger<CalculatorController> _logger;

        public CalculatorController(ILogger<CalculatorController> logger)
        {
            _calculator = new Calculator();
            _logger = logger;
        }

        [HttpGet("add")]
        public IActionResult Add(int a, int b)
        {
            _logger.LogInformation($"Вызов Add с параметрами: {a}, {b}");
            return Ok(_calculator.Add(a, b));
        }

        [HttpGet("subtract")]
        public IActionResult Subtract(int a, int b)
        {
            return Ok(_calculator.Subtract(a, b));
        }

        [HttpGet("multiply")]
        public IActionResult Multiply(int a, int b)
        {
            return Ok(_calculator.Multiply(a, b));
        }

        [HttpGet("divide")]
        public IActionResult Divide(int a, int b)
        {
            try
            {
                return Ok(_calculator.Divide(a, b));
            }
            catch (DivideByZeroException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("isprime")]
        public IActionResult IsPrime(int number)
        {
            return Ok(_calculator.IsPrime(number));
        }
    }
}