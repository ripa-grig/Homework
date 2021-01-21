using Homework.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Homework.Helpers
{
    public static class Parser
    {
        /// <summary>
        /// Helper method used to transform received data for better manipulating.
        /// </summary>
        /// <param name="inputText">Recievet information.</param>
        /// <returns></returns>
        public static List<StudentsList> TransformRecevedInformation(string inputText)
        {
            inputText = inputText.Substring(inputText.IndexOf("Entrance examination", StringComparison.OrdinalIgnoreCase) + "Entrance examination".Length);
            // var pattern = @"Group[\d]+";
            string pattern = @"Group";
            string[] result = Regex.Split(inputText, pattern,
                                       RegexOptions.IgnoreCase);
            List<StudentsList> studentsInGroups = new List<StudentsList>();

            foreach (string group in result)
            {
                if (string.IsNullOrWhiteSpace(group))
                    continue;
                List<string> studentsInGroup = group.Split("\r\n").ToList();

                // Get group number.
                string groupNumber = studentsInGroup.FirstOrDefault(i => i.All(char.IsDigit));
                if (string.IsNullOrWhiteSpace(groupNumber))
                {
                    Console.WriteLine("Group information is incorrect.");
                    groupNumber = "0";
                }

                List<Students> studentsList = GetStudentsInEachGroup(studentsInGroup);
                StudentsList existingGroup = studentsInGroups.FirstOrDefault(g => g.GroupNumber == Convert.ToInt32(groupNumber));
                if (existingGroup == null)
                {
                    studentsInGroups.Add(new StudentsList { GroupNumber = Convert.ToInt32(groupNumber), StudentsByGoup = studentsList });
                }
                else
                {
                    existingGroup.StudentsByGoup.AddRange(studentsList);
                }
            }
            return studentsInGroups;
        }

        /// <summary>
        /// For each group retrieves students with appropriate information.
        /// </summary>
        /// <param name="studentsInGroup">List of students information in string representation.</param>
        /// <returns></returns>
        private static List<Students> GetStudentsInEachGroup(List<string> studentsInGroup)
        {
            List<Students> studentsList = new List<Students>();
            foreach (string student in studentsInGroup)
            {
                if (student.Contains(';'))
                {
                    string[] studentInfo = student.Split(';');
                    string studentName = studentInfo[0];
                    if (string.IsNullOrWhiteSpace(studentName) || studentName.Contains('='))
                    {
                        Console.WriteLine("Has been received wring information");
                        continue;
                    }

                    Dictionary<string, int> kvPairs = RetrieveSubjectKeyValuePairs(studentInfo, studentName);

                    Students s = new Students
                    {
                        Name = studentName,
                        Math = GetSubjectResult(kvPairs, studentName, "math"),
                        Physics = GetSubjectResult(kvPairs, studentName, "physics"),
                        English = GetSubjectResult(kvPairs, studentName, "english")
                    };
                    studentsList.Add(s);
                }
            }
            return studentsList;
        }

        /// <summary>
        /// Parse subjects information to dictioanry.
        /// </summary>
        /// <param name="studentInfo"></param>
        /// <returns></returns>
        private static Dictionary<string, int> RetrieveSubjectKeyValuePairs(string[] studentInfo, string studentName)
        {
            Dictionary<string, int> kvpairs = new Dictionary<string, int>();
            foreach (string info in studentInfo)
            {
                if (!info.Contains('='))
                    continue;
                string[] kv = info.Split('=');
                if (string.IsNullOrWhiteSpace(kv[0]) || string.IsNullOrWhiteSpace(kv[1]))
                {
                    Console.WriteLine($"For student '{studentName}' has received wrong information.");
                    continue;
                }
                if (!kvpairs.ContainsKey(kv[0].ToLowerInvariant()))
                {
                    kvpairs.Add(kv[0].ToLowerInvariant(), Convert.ToInt32(kv[1]));
                }
                else
                {
                    Console.WriteLine($"For student '{studentName}' information of '{kv[0]}' subject has been already recorded.");
                }
            }
            return kvpairs;
        }

        /// <summary>
        /// Retrieve value from the dictionary with the given key.
        /// </summary>
        /// <param name="kvPairs"></param>
        /// <param name="subjectName"></param>
        /// <returns></returns>
        private static int GetSubjectResult(Dictionary<string, int> kvPairs, string studentName, string subjectName)
        {
            if (kvPairs.TryGetValue(subjectName, out int value))
                return value;
            else
            {
                Console.WriteLine($"For student '{studentName}' the information of '{subjectName}' subject is missing, will be used default 0 value.");
                return 0;
            }
        }
    }
}
