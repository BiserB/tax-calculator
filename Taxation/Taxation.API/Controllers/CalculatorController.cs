using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Taxation.API.Models;
using Taxation.API.Models.Config;
using Taxation.Common.Models.Response;
using Taxation.Domain.Rules.Interfaces;
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
        private readonly IMemoryCache _memoryCache;
        private readonly CacheSettings _cacheSettings;

        public CalculatorController(InfoProvider infoProvider,
            TaxCalculator taxCalculator,
            IMemoryCache memoryCache,
            IOptions<CacheSettings> cacheSettings)
        {
            _infoProvider = infoProvider;
            _taxCalculator = taxCalculator;
            _memoryCache = memoryCache;
            _cacheSettings = cacheSettings.Value;
        }

        [HttpGet(Name = "GetLegislation")]
        public string Get()
        {
            return _infoProvider.GetLegislation();
        }

        [HttpPost]
        public ActionResult<Taxes> CalculateTaxes([FromBody] TaxPayer taxPayer)
        {
            var cacheKey = GenerateCacheKey(taxPayer);

            if (_memoryCache.TryGetValue(cacheKey, out ITaxationContext cachedResult))
            {
                return Ok(cachedResult);
            }

            var taxes = _taxCalculator.Calculate(taxPayer.GrossIncome, taxPayer.CharitySpent);

            SaveToCache(cacheKey, taxes);

            return Ok(taxes);
        }

        private void SaveToCache(string cacheKey, ITaxationContext taxes)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(_cacheSettings.SlidingExpirationMinutes));

            _memoryCache.Set(cacheKey, taxes, cacheEntryOptions);
        }

        private string GenerateCacheKey(TaxPayer taxPayer)
        {
            return $"{taxPayer.FullName}-{taxPayer.DateOfBirth}-{taxPayer.GrossIncome}-{taxPayer.SSN}-{taxPayer.CharitySpent}";
        }
    }
}
