using Homework.Helpers;
using Homework.Parameters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Homework
{
    class Program
    {
        static void Main()
        {
            string inputText = GetInputText();

            List<StudentsList> stByGroups = Parser.TransformRecevedInformation(inputText);

            Response response = Analyzer.AnalyzeReceievedInforamtion(stByGroups);
            string responseString = JsonConvert.SerializeObject(response, Formatting.Indented);

            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string outputPath = Path.GetFullPath(Path.Combine(path, @"..\..\..\"));
           
            File.WriteAllText(outputPath + "Output.txt", responseString);
        }

        private static string GetInputText()
        {
            Stream stream = typeof(Program).Assembly.GetManifestResourceStream("Homework.Data.txt");
            if (stream == null)
            {
                Console.WriteLine($"Data file is missing.");
            }
            stream.Position = 0;
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }

    }
}
