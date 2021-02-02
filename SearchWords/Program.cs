using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SearchWords.BR.Services;
using SearchWords.BR.Services.Interfaces;
using System;
using System.Linq;

namespace SearchWords
{
    public class Program
    {
        #region Private
        private readonly ILogger<Program> logger;
        private readonly IFolderService folderService;
        private readonly IConfiguration configuration;
        private string defaultPrompt;
        private string[] exitCommands;
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
        /// Setup configuration for reading from appsettings.json
        /// </summary>
        /// <param name="args"></param>
        /// <returns>Newly created host</returns>
        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostContext, config) =>
                {
                    config.SetBasePath(Environment.CurrentDirectory);
                    config.AddJsonFile("appsettings.json", optional: false);
                })
                .ConfigureServices(
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
        public Program(ILogger<Program> logger, IConfiguration configuration, IFolderService folderService)
        {
            this.logger = logger;
            this.configuration = configuration;
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
            defaultPrompt = configuration["defaultPrompt"];
            exitCommands = configuration["exitCommands"].Split(';');

            if (!folderService.Exists(folderPath))
            {
                Console.WriteLine($"Please write an existing path. Error: '{folderPath}' doesn't exist.");
                return;
            }

            //Map into memory the specified path
            folderService.Load(folderPath);

            while (true)
            {
                Console.Write(defaultPrompt);

                var criteria = Console.ReadLine();

                if (exitCommands.Any(e => e == criteria.Trim().ToLower()))
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
