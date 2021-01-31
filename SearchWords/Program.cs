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
                throw new ArgumentException("Please write an existing path.");
            }

            using IHost host = CreateHostBuilder(args).Build();

            //LaunchPrompt(host.Services, string.Join(" ", args));
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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="folderPath"></param>
        //private static void LaunchPrompt(IServiceProvider services, string folderPath)
        //{
        //    using IServiceScope serviceScope = services.CreateScope();
        //    IServiceProvider provider = serviceScope.ServiceProvider;

        //    var folder = provider.GetRequiredService<FolderService>();

        //    if(!folder.Exists(folderPath))
        //    {
        //        Console.WriteLine($"Please write an existing path. Error: '{folderPath}' doesn't exist.");
        //        return;
        //    }

        //    folder.Load(folderPath);

        //    bool exit = false;
        //    while (!exit)
        //    {
        //        Console.Write("search> ");

        //        var criteria = Console.ReadLine();

        //        if(criteria.Trim().ToLower() == "quit" || criteria.Trim().ToLower() == "exit")
        //        {
        //            exit = true;
        //            break;
        //        }

        //        folder.Search(criteria);
        //    }
        //}
    }
}
