using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Hospital
{
    class Program
    {
        public SqlConnection connection = new SqlConnection(".NETFramework,Version=v4.5.2");
        static void Main(string[] args)
        {
            Hospital.MyConsole();

            //DateTime s = DateTime.Now;
            //DateTime e = s.AddHours(1);
            //Console.WriteLine("Your request for consultation was confirmed. \nYour consultation: " + s.ToString().Substring(0,16)  + " - " + e.TimeOfDay.ToString().Substring(0,5) + "\n                   Doctor: Armen Sargsyan" + " (" + "login" + ")");
            //Console.Write("Messeges ");
            //Console.ForegroundColor = ConsoleColor.Red;
            //Console.WriteLine("1");
            //Console.ResetColor();

            //List<string> Messages = new List<string>();
            //Messages.Add("1");
            //Messages.Add("2");
            //Messages.Add("3");
            //Messages.Add("4");
            //Messages.Add("5");
            //Messages.Add("6");
            //int i = Messages.Count - 1;
            //int countOfUnreadMessages = 3;
            //Console.Write("Messeges ");
            //Console.ForegroundColor = ConsoleColor.Red;
            //Console.WriteLine(countOfUnreadMessages);
            //Console.ForegroundColor = ConsoleColor.DarkGray;
            //for (i = Messages.Count - 1; i >= 0; i--)
            //{
            //    if (i == Messages.Count - countOfUnreadMessages - 1)
            //    {
            //        Console.ForegroundColor = ConsoleColor.White;
            //    }
            //    Console.WriteLine(Messages[i]);
            //}
            //List<WorkingTimes> op = new List<WorkingTimes>();
            //op.Add(new WorkingTimes(DayOfWeek.Friday, "10:00", "24:00"));
            //op.Add(new WorkingTimes(DayOfWeek.Wednesday, "10:00", "24:00"));
            //op.Add(new WorkingTimes(DayOfWeek.Saturday, "10:00", "24:00"));
            //op.Add(new WorkingTimes(DayOfWeek.Sunday, "10:00", "24:00"));
            //op.Add(new WorkingTimes(DayOfWeek.Monday, "10:00", "24:00"));
            //op.Add(new WorkingTimes(DayOfWeek.Thursday, "10:00", "24:00"));
            //op.Add(new WorkingTimes(DayOfWeek.Tuesday, "10:00", "24:00"));

            //IUser d = new Doctor("n", "s", "l", "p", "s", op, "t", 10000);
            //IPatient p = (IPatient)d;

            //Patient p = new Patient("n", "s", "l", "p", 17);
            //Consultation c = new Consultation(DateTime.Now, p);
            //Console.WriteLine(c);

            //string startingTime = "12:00";
            //string endingTime = "9:00";
            //string[] startingT = startingTime.Split(':');
            //string[] endingT = endingTime.Split(':');

            //Console.WriteLine("{0}, {1}, {2}, {3}", startingT[0], startingT[1], endingT[0], endingT[1]);
            //TimeSpan StartingTime = new TimeSpan(int.Parse(startingT[0].Trim()), int.Parse(startingT[1].Trim()), 0);
            //TimeSpan EndingTime = new TimeSpan(int.Parse(endingT[0].Trim()), int.Parse(endingT[1].Trim()), 0);
            //Console.WriteLine(startingTime);
            //Console.WriteLine(EndingTime);
        }
    }
}
