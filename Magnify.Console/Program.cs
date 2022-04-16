using Magnify.Application.Extensions;
using Magnify.Console;
using Magnify.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Magnify.Application
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var container = CreateContainer();

            var client = container.GetService<IConsoleClient>();


            ConsoleKeyInfo key = default;
            do
            {
                try
                {
                    System.Console.Clear();
                    System.Console.WriteLine("1. Shipper 2. Carrier 0. Exit");
                    key = System.Console.ReadKey();
                    System.Console.WriteLine();

                    switch (key.Key)
                    {
                        case ConsoleKey.D1:
                            await client.ShowShipperInterface();
                            break;
                        case ConsoleKey.D2:
                            await client.ShowCarrierInterface();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.Message);
                    System.Console.WriteLine("Press any key to continue");
                    System.Console.ReadKey();

                }
            }
            while (key.Key != ConsoleKey.D0);

        }

        private static ServiceProvider CreateContainer()
        {
            var serviceProvider = new ServiceCollection()
                .AddCQRS()
                .AddScoped<IConsoleClient, ConsoleClient>()
                .AddRepositories()
                .BuildServiceProvider();

            return serviceProvider;
        }
    }
}
