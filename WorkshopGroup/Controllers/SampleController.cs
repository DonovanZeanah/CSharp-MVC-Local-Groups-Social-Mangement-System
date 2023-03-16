namespace WorkshopGroup.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

        [ApiController]
        [Route("api/[controller]")]
        public class SampleController : ControllerBase
        {
            [HttpGet]
            public ActionResult<IEnumerable<string>> Get()
            {
                return new List<string> { "value1", "value2" };
            }
        }
    }