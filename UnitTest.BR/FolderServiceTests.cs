using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchWords.BR.Services;
using SearchWords.Models;

namespace UnitTest.BR
{
    [TestClass]
    public class FolderServiceTests
    {
        /// <summary>
        /// Test Search method. No results expected.
        /// </summary>
        [TestMethod]
        public void Search_NoResults()
        {
            string criteria = "unit_test";
            string expected = $"No coincidences.{System.Environment.NewLine}";
            FolderService folderService = new FolderService();
            folderService.Folder = new Folder()
            {
                Name = "folder_test",
                Files = new System.Collections.Generic.List<File>()
            };
            folderService.Folder.Files.Add(new File() { Name = "file_test", Content = "00000000000000000000000000000000000000000000000000000000" });


            folderService.Search(criteria);
            var actual = folderService.Message;



            Assert.AreEqual(expected, actual, "Wrong result.");
        }
        /// <summary>
        /// Test Search method. Some coincidences expected.
        /// </summary>
        [TestMethod]
        public void Search_WithResults()
        {
            string criteria = "unit_test";
            string expected = $"file_test1 : 1 occurrences {System.Environment.NewLine}";
            expected += $"file_test3 : 1 occurrences {System.Environment.NewLine}";
            FolderService folderService = new FolderService();
            folderService.Folder = new Folder()
            {
                Name = "folder_test",
                Files = new System.Collections.Generic.List<File>()
            };
            folderService.Folder.Files.Add(new File() { Name = "file_test1", Content = "0000000unit_test0000000000000000000000000000000000000000000000000" });
            folderService.Folder.Files.Add(new File() { Name = "file_test2", Content = "00000000000000000000000000000000000000000000000000000000" });
            folderService.Folder.Files.Add(new File() { Name = "file_test3", Content = "0000000000000000000000000000000000000unit_test0000000000000000000" });


            folderService.Search(criteria);
            var actual = folderService.Message;



            Assert.AreEqual(expected, actual, "Wrong result.");
        }
    }
}
