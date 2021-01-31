using SearchWords.BR.Services.Interfaces;
using SearchWords.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace SearchWords.BR.Services
{
    public class FolderService : IFolderService, IDisposable
    {
        private Folder folder;

        //private readonly IFileService fileService;
        //public FolderService(IFileService fileService)
        //{
        //    this.fileService = fileService;
        //}



        /// <summary>
        /// Check if the folder exists in the file system.
        /// </summary>
        /// <param name="name">Path</param>
        /// <returns>True or False</returns>
        public bool Exists(string name)
        {
            return Directory.Exists(name);
        }
        /// <summary>
        /// Load text files from folder in memory.
        /// </summary>
        public void Load(string name)
        {
            folder = new Folder()
            {
                Name = name,
                Files = new List<Models.File>()
            };

            //Load text files, names and content.
            string[] fileEntries = Directory.GetFiles(name,"*.txt");
            foreach (string fileName in fileEntries)
            {
                folder.Files.Add(new SearchWords.Models.File()
                {
                    Name = fileName,
                    Content = System.IO.File.ReadAllText(fileName)
                });
            }
        }
        /// <summary>
        /// Search coincidences in files within current folder.
        /// </summary>
        /// <param name="criteria">Text for matchings</param>
        public void Search(string criteria)
        {
            string result = String.Empty;

            var coincidenceFiles = folder.Files.Where(e => e.Content.Contains(criteria.Trim(), StringComparison.InvariantCultureIgnoreCase)).ToArray();

            if(coincidenceFiles == null || coincidenceFiles.Length == 0)
            {
                result = $"No coincidences.{System.Environment.NewLine}";
            }
            else
            {                
                foreach(var c in coincidenceFiles)
                {
                    result += $"{c.Name.Substring(c.Name.LastIndexOf('\\'))} : {Regex.Matches(c.Content, criteria).Count} occurrences {System.Environment.NewLine}";
                }
            }

            folder.Message = result;
            //Console.Write(result);
        }
        /// <summary>
        /// Return Folder object.
        /// </summary>
        /// <returns>Folder object</returns>
        public Folder GetFolder()
        {
            return folder;
        }

        public void Dispose()
        {
            if (folder.Files != null)
                folder.Files.Clear();

            folder.Message = null;
            folder = null;
        }
    }
}
