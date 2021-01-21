using System.Collections.Generic;

namespace Homework.Parameters
{
    public class StudentsList
    {
        public int GroupNumber;
        public List<Students> StudentsByGoup;
    }


    public class Students
    {
        public string Name;
        public int Math = 0;
        public int Physics = 0;
        public int English = 0;
    }
}

