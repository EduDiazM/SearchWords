using SearchWords.Models.Interfaces;

namespace SearchWords.Models
{
    public class File : IFileSystemItem
    {
        public string Name { get; set; }
        public string Content { get; set; }
    }
}
