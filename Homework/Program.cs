using Homework.Helpers;
using Homework.Parameters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Homework
{
    class Program
    {
        static void Main()
        {
            string pathPrefix = @"C:\Users\HGrigor\Desktop\Homework\";
            string inputPath = pathPrefix + "Data.txt";
            string outputPath = pathPrefix + "Output.txt";

            if (!File.Exists(inputPath))
            {
                Console.WriteLine($"The specified path '{inputPath}' doesn't exist.");
            }

            string inputText = File.ReadAllText(inputPath);
            if (string.IsNullOrWhiteSpace(inputText))
            {
                Console.WriteLine("File doesn't contain any information.");
                return;
            }
            List<StudentsList> stByGroups = Parser.TransformRecevedInformation(inputText);

            Response response = Analyzer.AnalyzeReceievedInforamtion(stByGroups);
            string responseString = JsonConvert.SerializeObject(response, Formatting.Indented);

            File.WriteAllText(outputPath, responseString);
        }
    }
}
