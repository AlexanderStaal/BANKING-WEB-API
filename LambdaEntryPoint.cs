using System;
using Microsoft.AspNetCore.Hosting;
using Amazon.Lambda.AspNetCoreServer;
using BankingWebAPI;

namespace BankingAPI
{
    public class LambdaEntryPoint: APIGatewayHttpApiV2ProxyFunction
    {
        protected override void Init(IWebHostBuilder builder)
        {
            builder
            .UseStartup<Startup>();
        }
    }
}

