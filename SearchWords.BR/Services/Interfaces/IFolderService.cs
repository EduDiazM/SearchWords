using SearchWords.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SearchWords.BR.Services.Interfaces
{
    public interface IFolderService
    {
        public Folder Folder { get; set; }
        string Message { get; }
        void Load(string name);
        void Search(string criteria);
        bool Exists(string name);
    }
}
