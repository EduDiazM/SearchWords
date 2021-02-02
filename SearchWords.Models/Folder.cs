using System.Collections.Generic;

namespace SearchWords.Models
{
    public class Folder : FileSystemItem
    {
        public List<File> Files { get; set; }
    }
}