using LogManagerAsBackgroundService.Interfaces.Business;
using Microsoft.AspNetCore.Mvc;

namespace LogManagerAsBackgroundService.Controllers
{
    public class ExampleController : ControllerBase
    {
        IBusinessService _businessService;
        public ExampleController(IBusinessService businessService)
        {
            _businessService = businessService;
            Task.Run(() => _businessService.DoWork());
        }
        public IActionResult Get()
        { 
            return Ok("Hello World");
        }
    }
}
