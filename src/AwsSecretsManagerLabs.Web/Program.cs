#region Imports
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
#endregion

namespace AwsSecretsManagerLabs.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    if (!hostingContext.HostingEnvironment.IsDevelopment())
                    {
                        // Don't add AWS secrets in local environment
                        //config.AddSecretsManager();
                        config.AddSecretsManager(configurator: opts =>
                        {
                            // Replace __ tokens in the configuration key name
                            opts.KeyGenerator = (secret, name) => name.Replace("__", ":");
                        });
                    }
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}



#region References
//https://github.com/Kralizek/AWSSecretsManagerConfigurationExtensions
//https://andrewlock.net/secure-secrets-storage-for-asp-net-core-with-aws-secrets-manager-part-1/
//https://andrewlock.net/secure-secrets-storage-for-asp-net-core-with-aws-secrets-manager-part-2/
#endregion
