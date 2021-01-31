using Microsoft.Extensions.DependencyInjection;
using System;
using SearchWords.Models;

namespace SearchWords.BR.Services
{
    public static class ConsoleService
    {
        public static void LaunchPrompt(IServiceProvider services, string folderPath)
        {
            using IServiceScope serviceScope = services.CreateScope();
            IServiceProvider provider = serviceScope.ServiceProvider;
            var folderService = provider.GetRequiredService<FolderService>();

            if (!folderService.Exists(folderPath))
            {
                Console.WriteLine($"Please write an existing path. Error: '{folderPath}' doesn't exist.");
                return;
            }

            folderService.Load(folderPath);

            bool exit = false;
            while (!exit)
            {
                Console.Write("search> ");

                var criteria = Console.ReadLine();

                if (criteria.Trim().ToLower() == "quit" || criteria.Trim().ToLower() == "exit")
                {
                    exit = true;
                    break;
                }

                folderService.Search(criteria);
                ShowResult(folderService);
            }
        }
        /// <summary>
        /// Show latest generated message within FolderService.
        /// </summary>
        /// <typeparam name="T">FolderService type</typeparam>
        /// <param name="item">FolderService object</param>
        private static void ShowResult<T>(T item)
            where T : FolderService => Console.WriteLine($"{item.Message}");
    }
}
