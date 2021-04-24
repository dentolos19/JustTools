using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;
using Syncfusion.Licensing;

namespace DentoTools
{

    public class Program
    {

        public static async Task Main(string[] args)
        {
            SyncfusionLicenseProvider.RegisterLicense("{LICENSE_KEY}"); 
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.Services.AddSyncfusionBlazor();
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            await builder.Build().RunAsync();
        }

    }

}