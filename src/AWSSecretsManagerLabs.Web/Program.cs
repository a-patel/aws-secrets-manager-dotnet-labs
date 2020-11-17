#region Imports
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
#endregion

namespace AWSSecretsManagerLabs.Web
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
//https://aws.amazon.com/blogs/security/how-to-use-aws-secrets-manager-client-side-caching-in-dotnet/
//https://www.codeproject.com/Articles/1279555/Access-AWS-Secrets-Manager-via-NET-CORE
//https://medium.com/@jonvines/using-aws-secrets-manager-with-dotnet-core-a44b54bba8d4
#endregion
