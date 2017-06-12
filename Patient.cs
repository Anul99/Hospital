using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Security.Cryptography;

namespace Hospital
{
    public struct Message
    {
        public string Text { get; set; }
        public DateTime Time { get; set; }
        public bool Read { get; set; }

        public Message(string text, DateTime time)
        {
            this.Text = text;
            this.Time = time;
            this.Read = false;
        }

        public override string ToString()
        {
            return this.Time + "\n" + this.Text + "\n";
        }
    }

    //public interface IPatient : IUser
    //{
    //    string History { get; set; }
    //    List<Message> Messages { get; set; }
    //    int CountOfUnreadMessages { get; }

    //    void RequestForConsultation(Doctor doctor, DateTime startOfConsultation);
    //    void SeeMyHistory();
    //    void SeeMyMessages();
    //}

    public class Patient : User/*, IPatient */
    {
        //public string Name { get; set; }
        //public string Surname { get; set; }
        //public string Login { get; set; }
        //public string Password { get; set; }
        //public string Possition { get; set; }
        public string History { get ; set; }
        public List<Message> Messages { get; set; }
        public int CountOfUnreadMessages
        {
            get
            {
                int count = 0;
                foreach (Message m in this.Messages)
                {
                    if (!m.Read)
                    {
                        count++;
                    }
                }
                return count;
            }
        }


        public Patient(string name, string surname, string login, string password)
        {
            this.Name = name;
            this.Surname = surname;
            this.Login = login;
            this.Password = password;
            this.Possition = "Patient";
            this.Messages = new List<Message>();
        }

        public void RequestForConsultation(Doctor doctor, DateTime startOfConsultation)
        {
            foreach (Consultation c in doctor.Calendar)
            {
                if (startOfConsultation >= c.StartOfConsultation && startOfConsultation < c.EndOfConsultation)
                {
                    MessageBox.Show("You can't have a consultation at this time.");
                }
            }
            doctor.ListOfRequests.Add(new Consultation(startOfConsultation, this));
        }

        public void SeeMyHistory()
        {
            Console.WriteLine(this.History);
        }

        public void SeeMyMessages()
        {
            int i = this.Messages.Count - 1;
            //int countOfUnreadMessages = 0;
            //while (!Messages[i].Read && i >= 0)
            //{
            //    countOfUnreadMessages++;
            //    i--;
            //}
            Console.Write("Messeges ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(this.CountOfUnreadMessages);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            for (i = this.Messages.Count - 1; i > this.Messages.Count - this.CountOfUnreadMessages - 1; i--)
            { 
                Console.WriteLine(this.Messages[i]);
            }
            Console.ForegroundColor = ConsoleColor.White;
            for ( ; i >= 0; i--)
            {
                Console.WriteLine(this.Messages[i]);
            }

        }

        public override string ToString()
        {
            return this.Name + " " + this.Surname + "\nLogin: " + this.Login + "\nHistory: " + this.History;
        }

        //public int CompareTo(IUser other)
        //{
        //    return this.Name.CompareTo(other.Name);
        //}

        public void ChangePassword(string newPassword)
        {
            this.Password = newPassword;
        }
    }
}
