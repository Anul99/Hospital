using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital
{
    public struct Message
    {
        public string Text { get; set; }
        public DateTime Time { get; set; }
        public bool Read { get; set; }
        public static int CountOfUnreadMessages { get; set; }

        public Message(string text, DateTime time)
        {
            this.Text = text;
            this.Time = time;
            this.Read = false;
            CountOfUnreadMessages++;
        }

        public override string ToString()
        {
            return this.Time + "\n" + this.Text + "\n";
        }
    }

    public class Patient : User 
    {
        public List<string> History { get ; set; }
        public int Age { get; set; }
        public List<Message> Messages { get; set; }

        public Patient(string name, string surname, string login, string password, int age) : base(name, surname, login, password, "Patient")
        {
            this.Age = age;
        }

        public void RequestForConsultation(Doctor doctor, DateTime startOfConsultation)
        {
            foreach (Consultation c in doctor.Calendar)
            {
                if (startOfConsultation >= c.StartOfConsultation && startOfConsultation < c.EndOfConsultation)
                {
                    Console.WriteLine("You can't have a consultation at this time.");
                }
            }
            doctor.ListOfRequests.Add(new Consultation(startOfConsultation, this));
        }

        public void SeeMyHistory()
        {
            foreach (string h in this.History)
            {
                Console.WriteLine(h);
            }
        }

        public void SeeMyMessages()
        {
            int i = this.Messages.Count - 1;
            int countOfUnreadMessages = 0;
            while (!Messages[i].Read)
            {
                countOfUnreadMessages++;
            }
            Console.Write("Messeges ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(countOfUnreadMessages);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            for (i = this.Messages.Count - 1; i >= 0; i--)
            {
                if (i == this.Messages.Count - countOfUnreadMessages - 1)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine(this.Messages[i]);
            }

        }

        public override string ToString()
        {
            string res = this.Name + " " + this.Surname + " " + "\nAge:" + this.Age + "/nLogin:" + this.Login ;
            foreach (string h in this.History)
            {
                res += h + "\n";
            }
            return res;
        }
    }
}
