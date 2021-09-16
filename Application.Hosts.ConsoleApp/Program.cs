﻿using Application.Configuration.Modules;
using Application.Contracts.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Application.Hosts.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Hello World");
            System.Console.WriteLine("Please enter your name");

            var services = new ServiceCollection();

            var module = new DatabaseModule();
            module.Host = ".";
            module.Password = "";
            module.DatabaseName = "SampleDb";
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

                var person = result.GetService<ISystemUserDao>();
                if (person != null)
                {
                    try
                    {
                        var people = person.GetAllSystemUsers();
                        
                    }
                    catch (Exception ex)
                    {
                        string correlationId = Guid.NewGuid().ToString();
                        logger.LogError(ex, "An unexpected error occured. Error Code: {ErrorCode}", correlationId);
                        throw new ApplicationException($"Unexpected server error: {correlationId}");
                    }

                }

            }
        }
    }
}