using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace api
{
  class Program
  {
    internal static void Main(string[] args)
    {
      CreateWebHostBuilder(args).Build().Run();
    }

    static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
      WebHost.CreateDefaultBuilder(args)
        .UseUrls("http://*:5000")
        .UseStartup<Startup>();
  }
}
