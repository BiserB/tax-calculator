using Taxation.API.Extensions;
using Taxation.API.Models.Config;
using Taxation.Common.Models.Config;

namespace Taxation.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddInfrastructureServices();
            builder.Services.AddBusinessServices();

            builder.Services.Configure<TaxSettings>(builder.Configuration.GetSection("TaxSettings"));
            builder.Services.Configure<CacheSettings>(builder.Configuration.GetSection("CacheSettings"));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddMemoryCache();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
