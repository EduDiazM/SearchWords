using SearchWords.Models;
using System.Collections.Generic;

namespace SearchWords.Models
{
    public class Folder : FileSystemItem
    {
        //public string Name { get; set; }
        public List<File> Files { get; set; }
        //public string Message { get; set; }
    }
}