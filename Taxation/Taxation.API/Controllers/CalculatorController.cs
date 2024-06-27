using Microsoft.AspNetCore.Mvc;
using Taxation.API.Models;
using Taxation.Common.Models.Response;
using Taxation.Services.Features.Calculations;
using Taxation.Services.Features.Information;

namespace Taxation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalculatorController : ControllerBase
    {
        private readonly InfoProvider _infoProvider;
        private readonly TaxCalculator _taxCalculator;

        public CalculatorController(InfoProvider infoProvider, TaxCalculator taxCalculator)
        {
            _infoProvider = infoProvider;
            _taxCalculator = taxCalculator;
        }

        [HttpGet(Name = "GetLegislation")]
        public string Get()
        {
            return _infoProvider.GetLegislation();
        }

        [HttpPost]
        public ActionResult<Taxes> CalculateTaxes([FromBody] TaxPayer taxPayer)
        {
            var taxes = _taxCalculator.Calculate(taxPayer.GrossIncome, taxPayer.CharitySpent);

            return Ok(taxes);
        }
    }
}
