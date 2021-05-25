using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;
#if !DEBUG
using Syncfusion.Licensing;
#endif

namespace DentoTools
{

    public static class Program
    {

        public static async Task Main(string[] args)
        {
            #if !DEBUG
            SyncfusionLicenseProvider.RegisterLicense("<LICENSE_KEY>"); 
            #endif
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.Services.AddSyncfusionBlazor();
            builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            await builder.Build().RunAsync();
        }

    }

}