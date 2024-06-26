using Microsoft.AspNetCore.Mvc;
using Taxation.Common.Models.Config;
using Taxation.Common.Models.Input;
using Taxation.Common.Models.Response;
using Taxation.Services.Factories;
using Taxation.Services.Features.Calculations;
using Taxation.Services.Features.Information;

namespace Taxation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalculatorController : ControllerBase
    {
        private readonly InfoProvider _infoProvider;

        public CalculatorController(InfoProvider infoProvider)
        {
            this._infoProvider = infoProvider;
        }

        [HttpGet(Name = "GetLegislation")]
        public string Get()
        {
            return _infoProvider.GetLegislation();
        }

        [HttpPost]
        public ActionResult<Taxes> CalculateTaxes([FromBody] TaxPayer taxPayer)
        {
            if (taxPayer == null)
            {
                return BadRequest("Invalid TaxPayer data.");
            }

            var configurations = new List<TaxRuleConfiguration>
            {
                new TaxRuleConfiguration { RuleType = TaxRuleType.TaxFree, Order = 1 },
                new TaxRuleConfiguration { RuleType = TaxRuleType.CharityDeduction, Order = 2 },
                new TaxRuleConfiguration { RuleType = TaxRuleType.IncomeTax, Order = 3 },
                new TaxRuleConfiguration { RuleType = TaxRuleType.SocialContribution, Order = 4 }
            };

            var rules = TaxationRuleFactory.CreateTaxRules(configurations.OrderBy(c => c.Order).ToList());

            var taxCalculator = new TaxCalculator(rules);

            var taxes = taxCalculator.Calculate(taxPayer.GrossIncome, taxPayer.CharitySpent);

            return Ok(taxes);
        }
    }
}
