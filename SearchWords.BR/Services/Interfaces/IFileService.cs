using SearchWords.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SearchWords.BR.Services.Interfaces
{
    public interface IFileService
    {
        List<File> Load(string folderName);
    }
}
