using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace SearchWords.IoC
{
    public static class DependencyInjection
    {
        //public static IServiceProvider RegisterDependencyInjection(IServiceCollection services)
        //{
        //    return null;
        //}

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args).ConfigureServices(
                services =>
                {
                    services.AddSingleton<Program>();
                    services.AddSingleton<IFolderService, FolderService>();
                    //services.AddSingleton<IFileService, FileService>(); ==> this interface is not required.
                });
        }
    }
}
