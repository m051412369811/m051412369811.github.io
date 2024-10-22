﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_MidTerm
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Course> courseList = new List<Course>()
              {
                new Course() { CourseId = "A001", Name = "C#", Teacher = "Bill", Classroom = "L107", Credit = 3 },
                new Course() { CourseId = "A002", Name = "HTML/CSS", Teacher = "Amos", Classroom = "L104", Credit = 2 },
                new Course() { CourseId = "A003", Name = "JavaScript/jQuery", Teacher = "奚江華", Classroom = "L104", Credit = 3 },
                new Course() { CourseId = "A004", Name = "MSSQL", Teacher = "Jimmy", Classroom = "L202", Credit = 3 },
                new Course() { CourseId = "A005", Name = "MVC5/CoreMVC", Teacher = "奚江華", Classroom = "L107", Credit = 6 },
                new Course() { CourseId = "B001", Name = "VueJS", Teacher = "Jimmy", Classroom = "L104", Credit = 2 },
                new Course() { CourseId = "B002", Name = "DevOps", Teacher = "David", Classroom = "L107", Credit = 3 },
                new Course() { CourseId = "B003", Name = "MongoDB", Teacher = "Ben", Classroom = "L202", Credit = 2 },
                new Course() { CourseId = "B004", Name = "Redis", Teacher = "Ben", Classroom = "L202", Credit = 2 },
                new Course() { CourseId = "B005", Name = "Git", Teacher = "Andy", Classroom = "L107", Credit = 1 },
                new Course() { CourseId = "B006", Name = "Git", Teacher = "Jimmy", Classroom = "L107", Credit = 1 }
              };

            List<Student> studentList = new List<Student>()
              {
                new Student() { StudentId = "S0001", Name = "小新", Gender = GenderOption.Male, CourseList = new List<string>() { "A001", "A004", "B002", "B003", "B004", "B005" } },
                new Student() { StudentId = "S0002", Name = "妮妮", Gender = GenderOption.Female, CourseList = new List<string>() { "A002", "A003", "B001", "B002", "B005" } },
                new Student() { StudentId = "S0003", Name = "風間", Gender = GenderOption.Male, CourseList = new List<string>() { "A001", "A002", "A003", "A004", "A005", "B001", "B002", "B003", "B004", "B005"  } },
                new Student() { StudentId = "S0004", Name = "阿呆", Gender = GenderOption.Male, CourseList = new List<string>() { "A001", "A002", "A003", "A004", "A005" } },
                new Student() { StudentId = "S0005", Name = "正男", Gender = GenderOption.Male, CourseList = new List<string>() { "B001", "B002", "B003", "B004", "B005" } },
                new Student() { StudentId = "S0006", Name = "小丸子", Gender = GenderOption.Female, CourseList = new List<string>() { "A005" } },
                new Student() { StudentId = "S0007", Name = "小玉", Gender = GenderOption.Female, CourseList = new List<string>() { "A005", "B002", "B003", "B004" } },
              };

            #region 第1題
            // 1. 列出所有課程的名稱
            Console.WriteLine("1. 列出所有課程的名稱");
            {
                var result1 = courseList.Select(p => p.Name).Distinct();
                Console.WriteLine(string.Join(" , ", result1));
            }

            Console.WriteLine($"{Environment.NewLine}");
            #endregion

            #region 第2題
            // 2. 列出所有在"L107"教室上課的課程ID
            Console.WriteLine("2. 列出所有在'L107'教室上課的課程ID");
            {
                var result2 = courseList.Where(x => x.Classroom == "L107").Select(x => x.CourseId);
                Console.WriteLine(string.Join(" , ", result2));

            }

            Console.WriteLine($"{Environment.NewLine}");
            #endregion

            #region 第3題
            // 3. 列出所有在'L107'教室上課，並且學分為3的課程ID
            Console.WriteLine("3. 列出所有在'L107'教室上課，並且學分為3的課程ID");
            {
                var result3 = courseList.Where(x => x.Classroom == "L107" && x.Credit==3).Select(x =>x.CourseId);
                Console.WriteLine(string.Join(" , ", result3));

            }

            Console.WriteLine($"{Environment.NewLine}");
            #endregion

            #region 第4題
            // 4. 列出所有老師的名字(名字不能重複出現)
            Console.WriteLine("4. 列出所有老師的名字(名字不能重複出現)");
            {
                var result4 = courseList.Select(p => p.Teacher).Distinct();
                Console.WriteLine(string.Join(" , ", result4));

            }

            Console.WriteLine($"{Environment.NewLine}");
            #endregion

            #region 第5題
            // 5. 列出所有有在'L202'上課的老師名字(名字不能重複出現)
            Console.WriteLine("5. 列出所有有在'L202'上課的老師名字(名字不能重複出現)");
            {
                var result5 = courseList.Where(x=>x.Classroom=="L202").Select(x => x.Teacher).Distinct();
                Console.WriteLine(string.Join(" , ", result5));

            }

            Console.WriteLine($"{Environment.NewLine}");
            #endregion

            #region 第6題
            // 6. 列出所有女性學生的名字
            Console.WriteLine("6. 列出所有女性學生的名字");
            {
                var result6 = studentList.Where(x => x.Gender == GenderOption.Female).Select(x => x.Name);
                Console.WriteLine(string.Join(" , ", result6));

            }

            Console.WriteLine($"{Environment.NewLine}");
            #endregion

            #region 第7題
            // 7. 列出有上'Git'這門課的學員名字
            Console.WriteLine("7. 列出有上'Git'這門課的學員名字");
            {
                //var result701 = studentList.Where(s => s.CourseList.Any(courseList.Where(c => c.Name == "Git").Select(c => c.CourseId).ToString()));
                var result7 = studentList.Where(s => s.CourseList.Any(c => courseList.Any(x => x.CourseId == c && x.Name == "Git"))).Select(s => s.Name);

                Console.WriteLine(string.Join(" , ", result7));
            }

            Console.WriteLine($"{Environment.NewLine}");
            #endregion

            #region 第8題
            // 8. 列出每個學員上的每一堂課
            // ex:
            /*
                       小玉: 
                            MVC5/CoreMVC
                            DevOps
                            MongoDB
                            Redis
                    */
            Console.WriteLine("8. 列出每個學員上的每一堂課");
            {

                var result8 = studentList.Select(s =>
                $"{s.Name}:{Environment.NewLine}{string.Join(Environment.NewLine, s.CourseList.Select(c => $"\t{courseList.First(x =>x.CourseId == c).Name}"))}");
                Console.WriteLine(string.Join(Environment.NewLine, result8));
            }
            Console.WriteLine($"{Environment.NewLine}");
            #endregion

            #region 第9題
            // 9. 找出誰上的課數量最少
            Console.WriteLine("9. 找出誰上的課數量最少");
            {
                var result9 = studentList.OrderBy(s => s.CourseList.Count).FirstOrDefault();

                if (result9 != null) Console.WriteLine(result9.Name);

            }

            Console.WriteLine($"{Environment.NewLine}");
            #endregion

            #region 第10題
            // 10. 找出誰修的學分總和小於10
            Console.WriteLine("10. 找出誰修的學分總和小於10");
            {
                var result10 = studentList
                    .Where(s => s.CourseList.Sum(x => courseList.First(c => c.CourseId == x).Credit) < 10)
                    .Select(s => s.Name);

                Console.WriteLine(string.Join(" , ", result10));

            }

            Console.WriteLine($"{Environment.NewLine}");
            #endregion

            #region 第11題
            // 11. 找出誰最後獲得學分數最高
            Console.WriteLine("11. 找出誰最後獲得學分數最高");
            {
                var result11 = studentList
                    .OrderByDescending(s => s.CourseList.Sum(c => courseList.First(x => x.CourseId == c).Credit))
                    .Select(s => s.Name).FirstOrDefault();

                if (result11 != null) Console.WriteLine(result11);
            }

            Console.WriteLine($"{Environment.NewLine}");
            #endregion

            #region 第12題(加分題)
            // 12. 十二生肖自定義排序
            Console.WriteLine("12. 十二生肖自定義排序");
            {
                var zoo = new List<string> { "龍", "鼠", "馬", "豬", "羊" }; //答案: 鼠、龍、馬、羊、豬
                var zotopia = new List<string> { "鼠","牛","虎","兔","龍","蛇","馬","羊","猴","雞","狗","豬" };
                Console.WriteLine($"排序前: {string.Join("、", zoo)}{Environment.NewLine}");
                Console.Write("排序後: ");

                var result12 = zoo.OrderBy(animal => zotopia.IndexOf(animal));

                Console.WriteLine($"排序後: {string.Join("、", result12)}");
            }

            #endregion

            Console.ReadLine();
        }
    }
    public class Course
    {
        /// <summary>
        /// 課程ID
        /// </summary>
        public string CourseId { get; set; }

        /// <summary>
        /// 課程名稱
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 指導教師
        /// </summary>
        public string Teacher { get; set; }

        /// <summary>
        /// 課程教室
        /// </summary>
        public string Classroom { get; set; }

        /// <summary>
        /// 學分
        /// </summary>
        public int Credit { get; set; }
    }

    public class Student
    {
        /// <summary>
        /// 學生姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 性別
        /// </summary>
        public GenderOption Gender { get; set; }

        /// <summary>
        /// 學號
        /// </summary>
        public string StudentId { get; set; }

        /// <summary>
        /// 選課
        /// </summary>
        public List<string> CourseList { get; set; }
    }

    public enum GenderOption
    {
        Male,
        Female
    }

}
