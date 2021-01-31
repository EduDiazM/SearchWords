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
            var folder = provider.GetRequiredService<FolderService>();

            if (!folder.Exists(folderPath))
            {
                Console.WriteLine($"Please write an existing path. Error: '{folderPath}' doesn't exist.");
                return;
            }

            folder.Load(folderPath);

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

                folder.Search(criteria);

                ShowResult(folder.GetFolder());
            }
        }

        private static void ShowResult<T>(T item)
            where T : Folder => Console.WriteLine($"{item.Message}");
    }
}
