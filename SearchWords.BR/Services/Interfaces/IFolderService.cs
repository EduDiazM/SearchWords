using SearchWords.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SearchWords.BR.Services.Interfaces
{
    public interface IFolderService
    {
        void Load(string name);
        void Search(string criteria);
        bool Exists(string name);
    }
}
