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
        #region Private Interface
        private readonly ILogger<Program> logger;
        private readonly IFolderService folderService;
        #endregion

        /// <summary>
        /// Entry point, setup the application.
        /// </summary>
        /// <param name="args">Existing path folder.</param>
        static void Main(string[] args) 
        {
            var host = CreateHostBuilder(args).Build();
            host.Services.GetRequiredService<Program>().RunApp(args);
        }

        /// <summary>
        /// Create the host to register the services for the dependency injection.
        /// </summary>
        /// <param name="args"></param>
        /// <returns>Newly created host</returns>
        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args).ConfigureServices(
                services =>
                {
                    services.AddSingleton<Program>();
                    services.AddSingleton<IFolderService, FolderService>();
                    //services.AddSingleton<IFileService, FileService>(); ==> this service is not used here.
                });
        }

        /// <summary>
        /// Setup interfaces to use during the execution.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="folderService"></param>
        public Program(ILogger<Program> logger, IFolderService folderService)
        {
            this.logger = logger;
            this.folderService = folderService;
        }

        /// <summary>
        /// Execute the core of the app.
        /// </summary>
        /// <param name="args"></param>
        public void RunApp(string[] args)
        {
            logger.LogInformation($"The execution has started.");
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

            logger.LogInformation($"The execution has finished.");
        }
    }
}
