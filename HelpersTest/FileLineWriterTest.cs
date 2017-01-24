using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;

namespace Frozenskys.Helpers.UnitTests
{
    [TestClass]
    public class FileLineWriterTest
    {
        IFileSystem fileSystem;

        [TestMethod]
        public void TestWriteLineToExistingFile()
        {
            var writer = new FileLineWriter(fileSystem, @"c:\test.txt");
            writer.WriteLine("Test");
            var expect = "Fail" + Environment.NewLine + "Test" + Environment.NewLine;
            Assert.AreEqual(expect, fileSystem.File.ReadAllText(@"c:\test.txt"));
        }

        [TestMethod]
        public void TestWriteLineToNewFile()
        {

            var writer = new FileLineWriter(fileSystem, @"c:\test2.txt");
            writer.WriteLine("Test2");
            Assert.AreEqual("Test2" + Environment.NewLine, fileSystem.File.ReadAllText(@"c:\test2.txt"));
        }

        [TestInitialize]
        public void Setup()
        {
            fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
                {
                    { @"c:\test.txt", "Fail" + Environment.NewLine },
                });
        }
    }
}