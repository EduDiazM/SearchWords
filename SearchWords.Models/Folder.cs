using SearchWords.Models.Interfaces;
using System.Collections.Generic;

namespace SearchWords.Models
{
    public class Folder : IFileSystemItem
    {
        //public string Name { get; set; }
        public List<File> Files { get; set; }
        //public string Message { get; set; }
    }
}