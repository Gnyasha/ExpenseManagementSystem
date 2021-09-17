using Application.Configuration.Modules;
using Application.Contracts.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace Application.Hosts.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Hello World");

            var services = new ServiceCollection();

            var module = new DatabaseModule();
            module.Host = ".";
            module.Password = "";
            module.DatabaseName = "ExpenseManagementSystem";
            module.UserName = "";
            module.IntegratedSecurity = true;

            module.Register(services);

            var result = services.BuildServiceProvider();
            if (result != null)
            {

                var logger = result.GetService<ILogger>();
                if (logger != null)
                {
                    logger.LogInformation("About to get people.");

                }

                var person = result.GetService<ITransactionStatusDao>().GetAll();
                if (person != null)
                {
                    try
                    {
                       
                        foreach (var item in person)
                        {
                            Console.WriteLine(item.Status);
                        }
                    }
                    catch (Exception ex)
                    {
                        //string correlationId = Guid.NewGuid().ToString();
                        //logger.LogError(ex, "An unexpected error occured. Error Code: {ErrorCode}", correlationId);
                        //throw new ApplicationException($"Unexpected server error: {correlationId}");
                    }

                }

            }
        }
    }
}
