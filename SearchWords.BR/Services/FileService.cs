using SearchWords.BR.Services.Interfaces;
using SearchWords.Models;
using System;
using System.Collections.Generic;

namespace SearchWords.BR.Services
{
    public class FileService : IFileService, IDisposable
    {
        /// <summary>
        /// Load files (file name and content) from folder.
        /// </summary>
        /// <param name="fileName">File name to load.</param>
        /// <returns>File object</returns>
        public File Load(string fileName)
        {
            File file = new File()
            {
                Name = fileName,
                Content = System.IO.File.ReadAllText(fileName)
            };

            return file;
        }

        public void Dispose()
        { }
    }
}
