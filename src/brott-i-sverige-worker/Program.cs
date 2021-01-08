using BrottISverigeWorker.Application;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace brott_i_sverige_worker
{
    class Program
    {
        public static IConfigurationRoot Configuration { get; set; }
        public static async Task Main(string[] args)
        {
            string environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
            if(string.IsNullOrWhiteSpace(environment))
            {
                throw new ArgumentException("DOTNET_ENVIRONMENT not set!");
            }

            string appsettings = environment == "Development" || environment == "Production"
                ? $"appsettings.{environment}.json"
                : "appsettings.json";

            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(AppContext.BaseDirectory))
                .AddJsonFile(appsettings)
                .AddEnvironmentVariables();

            Configuration = builder.Build();

            IServiceProvider serviceProvider = new ServiceCollection()
                .AddLogging(x => x.AddConsole())
                .AddSingleton<IConfiguration>(provider => Configuration)
                .AddHttpClient<ICrimeEventGetter, CrimeEventGetter>("crimeEventGetter", client =>
                {
                    client.DefaultRequestHeaders.Add("User-agent", "BrottISverige");
                }).Services
                .BuildServiceProvider();

            // Call worker
            var crimeEvents = await serviceProvider.GetService<ICrimeEventGetter>().GetPoliceEventsAsync();
        }
    }
}
