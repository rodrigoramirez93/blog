using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

[assembly: FunctionsStartup(typeof(FunctionApp.Startup))]

namespace FunctionApp
{
    public class AppSettings
    {
        public string SomeValue { get; set; }
    }

    public class Startup : FunctionsStartup
    {

        private IConfigurationRoot Config = null;

        private IConfigurationRoot FunctionConfig(string appDir) =>
            Config ??= new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(appDir, "appsettings.json"), optional: false, reloadOnChange: true)
                .Build();

        public override void Configure(IFunctionsHostBuilder builder)
        {
            //Config
            builder.Services.AddOptions<AppSettings>()
                .Configure<IOptions<ExecutionContextOptions>>((settings, exeContext) =>
                FunctionConfig(exeContext.Value.AppDirectory).Bind(settings));
        }
    }
}
