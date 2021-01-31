using SearchWords.BR.Services.Interfaces;
using SearchWords.Models;
using System.Collections.Generic;

namespace SearchWords.BR.Services
{
    public class FileService : IFileService
    {
        /// <summary>
        /// Load files (file name and content) from folder.
        /// </summary>
        /// <param name="folderName">Folder name where execute this load method</param>
        /// <returns>List of File objects</returns>
        public List<File> Load(string folderName)
        {
            return null;
        }
    }
}
