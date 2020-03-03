namespace Romulus.Web
{
  using System;
  using System.IO;
  using Microsoft.AspNetCore.Hosting;
  using Microsoft.Extensions.Hosting;

  public class Program
  {
    static string webRoot = Path.Combine(AppContext.BaseDirectory, "wwwroot");

    public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

    public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel((bc, o) => { o.AddServerHeader = false; })
                      //.UseWebRoot(webRoot)
                      .UseStartup<Startup>();
                });
    
  }
}
