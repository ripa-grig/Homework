using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using Homework.Parameters;
using Homework.Helpers;
using System.Linq;
using System;

namespace Homework.Tests
{
    public class Tests
    {
        [Test, Description("Check if that the student information is parsed correctly.")]
        public void StudentsNumberPassTest()
        {
            string inputPath = @"C:\Users\HGrigor\Desktop\Homework\Homework.Tests\TestFiles\CorrectFile.txt";
            string inputText = File.ReadAllText(inputPath);
            List<StudentsList> stByGroups = Parser.TransformRecevedInformation(inputText);

            Assert.AreEqual(10, stByGroups.FirstOrDefault(i => i.GroupNumber == 1).StudentsByGoup.Count);
        }

        [Test, Description("Check that the incorrect group name is skipped.")]
        public void GroupNameWrongTest()
        {
            string inputPath = @"C:\Users\HGrigor\Desktop\Homework\Homework.Tests\TestFiles\WrongGroupName.txt";
            string inputText = File.ReadAllText(inputPath);
            List<StudentsList> stByGroups = Parser.TransformRecevedInformation(inputText);
            StudentsList group0 = stByGroups.FirstOrDefault(g => g.GroupNumber == 0);
          
            Assert.True(group0 != null);
            Assert.AreEqual(10, group0.StudentsByGoup.Count);
        }

        [Test, Description("Check that the incorrect records don't cause exceptions and are parsed as 0.")]
        public void WrongRecordsTest()
        {
            string inputPath = @"C:\Users\HGrigor\Desktop\Homework\Homework.Tests\TestFiles\WrongRecordsTest.txt";
            string inputText = File.ReadAllText(inputPath);
            List<StudentsList> stByGroups = Parser.TransformRecevedInformation(inputText);

            Assert.AreEqual(0, stByGroups
                .FirstOrDefault(i => i.GroupNumber == 1)
                .StudentsByGoup.FirstOrDefault(s => string.Compare(s.Name, "Harrison Perez", System.StringComparison.OrdinalIgnoreCase) == 0).English);

            Assert.AreEqual(0, stByGroups
                    .FirstOrDefault(i => i.GroupNumber == 1)
                    .StudentsByGoup.FirstOrDefault(s => string.Compare(s.Name, "Shantelle Farrow", System.StringComparison.OrdinalIgnoreCase) == 0).Math);

            Assert.AreEqual(0, stByGroups
                .FirstOrDefault(i => i.GroupNumber == 1)
                .StudentsByGoup.FirstOrDefault(s => string.Compare(s.Name, "Regan Walton", System.StringComparison.OrdinalIgnoreCase) == 0).Physics);
         
            Assert.AreEqual(0, stByGroups
                    .FirstOrDefault(i => i.GroupNumber == 1)
                    .StudentsByGoup.FirstOrDefault(s => string.Compare(s.Name, "Rohan Buckner", System.StringComparison.OrdinalIgnoreCase) == 0).Physics);
           
            Assert.AreEqual(45, stByGroups
                           .FirstOrDefault(i => i.GroupNumber == 1)
                           .StudentsByGoup.FirstOrDefault(s => string.Compare(s.Name, "Rohan Buckner", System.StringComparison.OrdinalIgnoreCase) == 0).Math);
        }

        [Test, Description("Median")]
        public void MedianCalculateTest()
        {
            string inputPath = @"C:\Users\HGrigor\Desktop\Homework\Homework.Tests\TestFiles\CorrectFile.txt";
            string inputText = File.ReadAllText(inputPath);
            List<StudentsList> stByGroups = Parser.TransformRecevedInformation(inputText);
            Response response = Analyzer.AnalyzeReceievedInforamtion(stByGroups);
            Assert.AreEqual(45, response.GroupsInfo.FirstOrDefault(g => g.Group == "Group1").Median);

        }

        [Test, Description("Modus")]
        public void ModusCalculateTest()
        {
            string inputPath = @"C:\Users\HGrigor\Desktop\Homework\Homework.Tests\TestFiles\CorrectFile.txt";
            string inputText = File.ReadAllText(inputPath);
            List<StudentsList> stByGroups = Parser.TransformRecevedInformation(inputText);
            Response response = Analyzer.AnalyzeReceievedInforamtion(stByGroups);
            Modus modus = response.GroupsInfo.FirstOrDefault(g => string.Compare(g.Group, "Group1", StringComparison.OrdinalIgnoreCase) == 0).Modus;

            Assert.AreEqual(3, modus.Frequency);
            Assert.AreEqual(1, modus.Values.Count);
        }

        [Test, Description("Average")]
        public void AverageCalculateTest()
        {
            string inputPath = @"C:\Users\HGrigor\Desktop\Homework\Homework.Tests\TestFiles\CorrectFile.txt";
            string inputText = File.ReadAllText(inputPath);
            List<StudentsList> stByGroups = Parser.TransformRecevedInformation(inputText);
            Response response = Analyzer.AnalyzeReceievedInforamtion(stByGroups);
            Modus modus = response.GroupsInfo.FirstOrDefault(g => g.Group == "Group1").Modus;

            Assert.AreEqual(51, response.GroupsInfo.FirstOrDefault(g => string.Compare(g.Group, "Group1", StringComparison.OrdinalIgnoreCase) == 0).Average);
        }


        [Test, Description("Student average")]
        public void StudentsAverageTest()
        {
            string inputPath = @"C:\Users\HGrigor\Desktop\Homework\Homework.Tests\TestFiles\CorrectFile.txt";
            string inputText = File.ReadAllText(inputPath);
            List<StudentsList> stByGroups = Parser.TransformRecevedInformation(inputText);
            Response response = Analyzer.AnalyzeReceievedInforamtion(stByGroups);
            Modus modus = response.GroupsInfo.FirstOrDefault(g => string.Compare(g.Group, "Group1", StringComparison.OrdinalIgnoreCase) == 0).Modus;

            Assert.AreEqual(25, response.GroupsInfo
                .FirstOrDefault(g => g.Group == "Group1")
                .StudentsInfo.FirstOrDefault(s => string.Compare(s.Name, "Harrison Perez", StringComparison.OrdinalIgnoreCase) == 0).Average);
        }
    }
}