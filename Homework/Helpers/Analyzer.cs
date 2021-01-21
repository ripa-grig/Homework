using Homework.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Homework.Helpers
{
    public class Analyzer
    {
        /// <summary>
        /// Used to analyze data for all students and for each group.
        /// </summary>
        /// <param name="stByGroups"></param>
        /// <returns></returns>
        public static Response AnalyzeReceievedInforamtion(List<StudentsList> stByGroups)
        {
            Response response = new Response();
            List<int> allresults = new List<int>();
            foreach (StudentsList group in stByGroups)
            {
                string groupName = "Group" + group.GroupNumber;

                GroupInformation groupInformation = InvestigateForEachGroup(group.StudentsByGoup, groupName, out List<int> resultsCollectionForGroup);
                response.GroupsInfo.Add(groupInformation);
                allresults.AddRange(resultsCollectionForGroup);
            }
            response.Median = CalculateMedian(allresults);
            response.Modus = CalculateModus(allresults);
            response.Average = CalculateAverage(allresults);
            return response;
        }

        /// <summary>
        /// Analyze data for separate groups and record.
        /// </summary>
        /// <param name="students"></param>
        /// <param name="groupName"></param>
        /// <param name="results"></param>
        /// <returns></returns>
        private static GroupInformation InvestigateForEachGroup(List<Students> students, string groupName, out List<int> results)
        {
            List<StudentInformation> stInfoList = new List<StudentInformation>();
            List<int> groupResults = new List<int>();

            foreach (Students student in students)
            {
                StudentInformation stInfo = new StudentInformation
                {
                    Name = student.Name,
                    Average = (int)Math.Round((decimal)(
                    (student.Math * 40) +
                    (student.Physics * 35) +
                    (student.English * 25)) / 100)
                };
                groupResults.AddRange(new List<int> { student.Math, student.English, student.Physics });
                stInfoList.Add(stInfo);
            }

            results = groupResults;
            GroupInformation GroupInfo = new GroupInformation
            {
                Group = groupName,
                StudentsInfo = stInfoList,
                Median = CalculateMedian(groupResults),
                Modus = CalculateModus(groupResults),
                Average = CalculateAverage(groupResults)
            };
            return GroupInfo;
        }

        /// <summary>
        /// Calculate average from list of integers.
        /// </summary>
        /// <param name="groupResults"></param>
        /// <returns></returns>
        private static int CalculateAverage(List<int> groupResults)
        {
            return (int)Math.Round((decimal)groupResults.Sum() / (decimal)groupResults.Count);
        }

        /// <summary>
        /// Calculate median from list of integers.
        /// </summary>
        /// <param name="groupResults"></param>
        /// <returns></returns>
        private static int CalculateMedian(List<int> groupResults)
        {
            if (groupResults.Count == 0)
                return 0;

            bool isEven = groupResults.Count() % 2 == 0;
            int middle = groupResults.Count() / 2;
            groupResults.Sort();
            if (isEven)
            {
                return (groupResults[middle - 1] + groupResults[middle]) / 2;
            }
            else
            {
                return groupResults[middle];
            }
        }

        /// <summary>
        /// Calculate modus from list of integers.
        /// </summary>
        /// <param name="groupResults"></param>
        /// <returns></returns>
        private static Modus CalculateModus(List<int> groupResults)
        {
            if (groupResults.Count == 0)
                return new Modus();

            Dictionary<int, int> groupedResults = groupResults.GroupBy(x => x)
                .ToDictionary(v => v.Key, v => v.Count())
                .OrderByDescending(i => i.Value)
                .ToDictionary(a => a.Key, a => a.Value);

            KeyValuePair<int, int> firstVaue = groupedResults.First();

            List<int> modusValues = new List<int>();
            IEnumerable<KeyValuePair<int, int>> resultsWithSameModus = groupedResults.Where(i => i.Value == firstVaue.Value);

            foreach (KeyValuePair<int, int> item in resultsWithSameModus)
            {
                modusValues.Add(item.Key);
            }
            return new Modus { Frequency = firstVaue.Value, Values = modusValues };
        }
    }
}
