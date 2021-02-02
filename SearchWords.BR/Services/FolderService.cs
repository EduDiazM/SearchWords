using Microsoft.Extensions.Logging;
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
        private readonly ILogger<FolderService> logger;

        public Folder Folder { get; set; }
        public string Message { get; set; }

        /// <summary>
        /// Add new default constructor to use ILogger.
        /// </summary>
        /// <param name="logger"></param>
        public FolderService(ILogger<FolderService> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Check if the folder exists in the file system.
        /// </summary>
        /// <param name="name">Path</param>
        /// <returns>True or False</returns>
        public bool Exists(string name)
        {
            logger.LogInformation($"FolderService -> Exists.");
            return Directory.Exists(name);
        }
        /// <summary>
        /// Load text files from folder in memory.
        /// </summary>
        public void Load(string name)
        {
            logger.LogInformation($"FolderService -> Load.");
            Folder = new Folder()
            {
                Name = name,
                Files = new List<Models.File>()
            };

            //Load text files, names and content.
            string[] fileEntries = Directory.GetFiles(name,"*.txt");

            using(var fileService = new FileService())
            {
                foreach (string fileName in fileEntries)
                {
                    Folder.Files.Add(fileService.Load(fileName));
                }
            }
        }
        /// <summary>
        /// Search coincidences in files within current folder.
        /// </summary>
        /// <param name="criteria">Text for matchings</param>
        public void Search(string criteria)
        {
            logger.LogInformation($"FolderService -> Search.");
            string result = String.Empty;

            var coincidenceFiles = Folder.Files.Where(
                e => !string.IsNullOrWhiteSpace(e.Content)
                && !string.IsNullOrWhiteSpace(e.Name)
                && e.Content.Contains(criteria.Trim(), StringComparison.InvariantCultureIgnoreCase)).ToArray();

            if (coincidenceFiles == null || coincidenceFiles.Length == 0)
            {
                result = $"No coincidences.{System.Environment.NewLine}";
            }
            else
            {                
                foreach(var c in coincidenceFiles)
                {
                    result += $"{c.Name.Substring(c.Name.LastIndexOf('\\') + 1)} : {Regex.Matches(c.Content, criteria).Count} occurrences {System.Environment.NewLine}";
                }
            }

            Message = result;
        }
        /// <summary>
        /// Dispose method, from interface IDisposible.
        /// </summary>
        public void Dispose()
        {
            logger.LogInformation($"FolderService -> Dispose.");
            if (Folder != null)
            {
                if (Folder.Files != null)
                    Folder.Files.Clear();

                Folder = null;
            }
        }
    }
}
