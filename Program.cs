using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

using Microsoft.Extensions.Configuration;

namespace RateMyTeam
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config_cmdparams = new ConfigurationBuilder()
            .AddCommandLine(args)
            .Build();

            var config = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("hosting.json", optional: true)
              .Build();


            var host = new WebHostBuilder()
                .UseKestrel()
                .UseConfiguration(config)
                .UseConfiguration(config_cmdparams)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
