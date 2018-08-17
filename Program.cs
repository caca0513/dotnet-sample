using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace dotnet_sample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var envs = System.Environment.GetEnvironmentVariables();
            int port = 0;
            if (envs.Contains("APP_PORT"))
                port = int.Parse(envs["APP_PORT"].ToString());

            BuildWebHost(args, port).Run();
        }

        public static IWebHost BuildWebHost(string[] args, int port) =>
            WebHost.CreateDefaultBuilder(args)
                   .UseKestrel(options =>
                   {
                       if (port != 0)
                       //just need to setup the port, no need for IP
                       {
                           options.ListenAnyIP(port);
                       }
                   })
                   .UseStartup<Startup>()
                   .ConfigureLogging(logging =>
                   {
                       logging.AddConsole();
                       logging.AddDebug();
                   })
                   .Build();
    }
}
