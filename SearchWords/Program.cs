using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SearchWords.BR.Services;
using SearchWords.BR.Services.Interfaces;
using System;

namespace SearchWords
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args == null)
            {
                Console.WriteLine("Please write an existing path.");
                throw new ArgumentException("Please write an existing path.");
            }

            //Define Dependency Injection
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddSingleton<IFolderService, FolderService>()
                .BuildServiceProvider();

            //configure console logging
            //serviceProvider.GetService<ILoggerFactory>().AddConsole(LogLevel.Debug);

            var logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger<Program>();
            logger.LogDebug("Starting application");

            var folderService = serviceProvider.GetService<IFolderService>();
            var folderPath = string.Join(" ", args);

            if (!folderService.Exists(folderPath))
            {
                Console.WriteLine($"Please write an existing path. Error: '{folderPath}' doesn't exist.");
                return;
            }

            //Map into memory the specified path
            folderService.Load(folderPath);

            while (true)
            {
                Console.Write("search> ");

                var criteria = Console.ReadLine();

                if (criteria.Trim().ToLower() == "quit" || criteria.Trim().ToLower() == "exit")
                {
                    //Break the loop and close the app.
                    break;
                }

                folderService.Search(criteria);
                Console.WriteLine($"{folderService.Message}");
            }
        }
    }
}
