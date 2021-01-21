using System.Collections.Generic;

namespace Homework.Parameters
{
    public class Response
    {
        public int Average;
        public int Median;
        public Modus Modus;
        public List<GroupInformation> GroupsInfo = new List<GroupInformation>();
    }
    public class GroupInformation
    {
        public int Average;
        public string Group;
        public int Median;
        public Modus Modus;
        public List<StudentInformation> StudentsInfo = new List<StudentInformation>();
    }
    public class StudentInformation
    {
        public string Name;
        public int Average;
    }
    public class Modus
    {
        public int Frequency;
        public List<int> Values;
    }
}
