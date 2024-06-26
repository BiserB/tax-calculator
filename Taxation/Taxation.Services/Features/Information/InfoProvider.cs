using Taxation.Domain.Constants;

namespace Taxation.Services.Features.Information
{
    public class InfoProvider
    {
        public string GetLegislation() 
        {
            return Legislation.Content;
        }
    }
}
