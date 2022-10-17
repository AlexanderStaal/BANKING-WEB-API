using System.IO;
using BankingWebAPI;
using Microsoft.AspNetCore.Hosting;

namespace BankingAPI
{
    public class LambdaFunction: Amazon.Lambda.AspNetCoreServer.APIGatewayProxyFunction
    {
        protected override void Init(IWebHostBuilder builder)
        {
            builder
            .UseContentRoot(Directory.GetCurrentDirectory())
            .UseStartup<Startup>()
            .UseApiGateway();
        }

        protected override void Init(IHostBuilder builder)
        {
        }
    }
}

