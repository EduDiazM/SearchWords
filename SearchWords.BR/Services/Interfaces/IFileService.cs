using SearchWords.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SearchWords.BR.Services.Interfaces
{
    public interface IFileService
    {
        File Load(string fileName);
    }
}
