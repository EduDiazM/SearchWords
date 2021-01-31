using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SearchWords.BR.Services;
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

            using IHost host = CreateHostBuilder(args).Build();

            ConsoleService.LaunchPrompt(host.Services, string.Join(" ", args));

            host.Run();
        }

        static IHostBuilder CreateHostBuilder(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                    services
                        //.AddSingleton<IFolderService, FolderService>()
                        //.AddSingleton<IFileService, FileService>());
                        .AddSingleton<FolderService>()
                        .AddSingleton<FileService>());

            return host;
        }
    }
}
