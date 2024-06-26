using Microsoft.AspNetCore.Mvc;
using Taxation.Domain.Constants;

namespace Taxation.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Calculator : ControllerBase
    {
        [HttpGet(Name = "GetLegislation")]
        public string Get()
        {
            return Legislation.Content;
        }
    }
}
