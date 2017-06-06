using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Hospital
{
    public enum Command
    {
        search,
        signIn,
        signUp,
        signOut,
        exit,

        //Patient's commands
        requestForConsultation,
        myHistory,
        myMesseges,

        //Doctor's commands
        addPatientHistory,
        openListOfRequests,
        confirm,
        refuse,
        myCalendar,

        //Admin's commands
        addDoctor,
        deleteDoctor,
        reports, //????????????

        nothing

    }

    public static class Hospital
    {
        private static List<User> allUsers = new List<User>();
        public static List<User> AllUsers
        {
            get { return allUsers; }
            set { allUsers = value; }
        }

        public static Command DetectCommand(string str)
        {
            foreach (string c in Enum.GetNames(typeof(Command)))
            {
                if (str == c)
                {
                    Command command = (Command)Enum.Parse(typeof(Command), c);
                    if ((int)command >= 5 && (int)command < 8 && myPatient == null)
                        return Command.nothing;
                    if ((int)command >= 8 && (int)command < 13 && myDoctor == null)
                        return Command.nothing;
                    if ((int)command >= 13 && myAdmin == null)
                        return Command.nothing;
                    if ((int)command < 5)
                        return command;
                }
            }
            return Command.nothing;
        }

        public static User Search(string line)
        {
            List<User> foundUsers = new List<User>();
            if (line == "")
            {
                AllUsers.Sort();
                foundUsers = AllUsers;
            }
            string[] s = line.Split();
            switch (s.Length)
            {
                case 1:
                    SearchByName(s[0], foundUsers);
                    SearchBySurname(s[0], foundUsers);
                    SearchByLogin(s[0], foundUsers);
                    break;
                case 2:
                    SearchByNameAndSurname(s[0], s[1], foundUsers);
                    SearchByNameAndSurname(s[1], s[0], foundUsers);
                    break;
                case 3:
                    SearchByNameSurnnameAndLogin(s[0], s[1], s[2], foundUsers);
                    SearchByNameSurnnameAndLogin(s[1], s[0], s[2], foundUsers);
                    SearchByNameSurnnameAndLogin(s[1], s[2], s[0], foundUsers);
                    SearchByNameSurnnameAndLogin(s[2], s[1], s[0], foundUsers);
                    break;
            }
            if (foundUsers.Count == 0)
            {
                Console.WriteLine("There is no user with this name.");
                return null;
            }
            else if (foundUsers.Count == 1)
            {
                return foundUsers[0];
            }
            else
            {
                foundUsers.Sort();
                for (int i = 0; i < foundUsers.Count; i++)
                {
                    Console.WriteLine(i + 1 + " " + foundUsers[i].Name + " " + foundUsers[i].Surname + " login:" + foundUsers[i].Login);
                }
                return Select(foundUsers);

            }
        }

        private static void SearchByName(string name, List<User> foundUsers)
        {
            foreach (User u in AllUsers)
            {
                if (u.Name.ToLower() == name.ToLower())
                    foundUsers.Add(u);
            }
        }

        private static void SearchBySurname(string surname, List<User> foundUsers)
        {
            foreach (User u in AllUsers)
            {
                if (u.Surname.ToLower() == surname.ToLower())
                    foundUsers.Add(u);
            }
        }

        private static void SearchByNameAndSurname(string name, string surname, List<User> foundUsers)
        {
            foreach (User u in AllUsers)
            {
                if (u.Name.ToLower() == name.ToLower() && u.Surname.ToLower() == surname.ToLower())
                    foundUsers.Add(u);
            }
        }

        private static void SearchByLogin(string login, List<User> foundUsers)
        {
            foreach (User u in AllUsers)
            {
                if (u.Login == login)
                    foundUsers.Add(u);
            }
        }

        private static void SearchByNameSurnnameAndLogin(string name, string surname, string login, List<User> foundUsers)
        {
            foreach (User u in AllUsers)
            {
                if (u.Name.ToLower() == name.ToLower() && u.Surname.ToLower() == surname.ToLower() && u.Login == login)
                    foundUsers.Add(u);
            }
        }


        public static User Select(List<User> list)
        {
            try
            {
                int num = Convert.ToInt32(Console.ReadLine());
                if (num < 1 || num > list.Count)
                {
                    MessageBox.Show("Write correct number.");
                    return null;
                }
                else
                {
                    return list[num - 1];
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Write a number.");
                return null;
            }
        }

        public static Consultation Select(List<Consultation> list, string s)
        {
            try
            {
                int num = Convert.ToInt32(s);
                if (num < 1 || num > list.Count)
                {
                    MessageBox.Show("Incorrect number.");
                    return null;
                }
                else
                {
                    return list[num - 1];
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Write a number.");
                return null;
            }
        }


        public static void RequestForConsultation(string date)
        {
            if (selected == null)
            {
                Console.WriteLine("There is no selected doctor.");
            }
            else
            {

                Doctor doctor = (Doctor)selected;
                doctor.Calendar.Sort();
                foreach (Consultation c in doctor.Calendar)
                {
                    if (c.StartOfConsultation > DateTime.Now)
                    {
                        Console.WriteLine(c);
                    }
                }
                Console.WriteLine("Write date and time for consultation.");
                date = Console.ReadLine();
                try
                {
                    string[] dateAndTime = date.Split('/', '.', ' ', ':');
                    DateTime startOfConsultation = new DateTime(int.Parse(dateAndTime[2]), int.Parse(dateAndTime[1]), int.Parse(dateAndTime[0]), int.Parse(dateAndTime[3]), int.Parse(dateAndTime[4]), 0);
                    myPatient.RequestForConsultation(doctor, startOfConsultation);
                }
                catch (Exception)
                {
                    MessageBox.Show("Please enter date in DD/MM/YY HH:MM or DD.MM.YY HH:MM format.");
                }
            }
        }

        public static void ConfirmRequest(Consultation request)
        {
            if (request != null)
            {
                Console.WriteLine("Write duration of consulteation(by minutes).");
                bool t = false;
                int duration = 0;
                while (!t)
                {
                    try
                    {
                        duration = Convert.ToInt32(Console.ReadLine());
                        t = true;
                    }
                    catch (FormatException)
                    {
                        MessageBox.Show("Write duration of consulteation by minutes.");
                    }
                }
                Consultation newConsultation = new Consultation(request.StartOfConsultation, request.StartOfConsultation.AddMinutes(duration), request.Patient);
                myDoctor.ServeAPatient(newConsultation);
            }
        }






        private static Patient myPatient = null;
        private static Doctor myDoctor = null;
        private static Admin myAdmin = null;
        private static User selected = null;

        public static void MyConsole()
        {
            Console.WriteLine("WELCOME TO OUR HOSPITAL :)\n");
            Console.WriteLine("Active commands: " + (Command)0);
            for (int i = 1; i < 5; i++)
            {
                Console.WriteLine("                 " + (Command)i);
            }


            Command command = Command.nothing;
            while (command != Command.exit)
            {
                String line = Console.ReadLine();
                command = DetectCommand(line.Split()[0]);
                line = line.Replace(command.ToString(), "").Trim();
                switch (command)
                {
                    case Command.nothing:
                        MessageBox.Show("Incorrect command!");
                        break;
                    case Command.search:
                        selected = Search(line);
                        Console.WriteLine(selected);
                        break;
                    case Command.signIn:

                        break;
                    case Command.requestForConsultation:
                        RequestForConsultation(line.Replace(command.ToString(), "").Trim());
                        break;
                    case Command.myHistory:
                        myPatient.SeeMyHistory();
                        break;
                    case Command.myMesseges:
                        myPatient.SeeMyMessages();
                        break;
                    case Command.addPatientHistory:
                        try
                        {
                            Patient selectedPatient = (Patient)selected;
                            myDoctor.AddNoteToPatientHistory(selectedPatient, line.Replace(command.ToString(), "").Trim());
                        }
                        catch (System.InvalidCastException)
                        {
                            MessageBox.Show("There is no patient selected.");
                        }
                        break;
                    case Command.openListOfRequests:
                        myDoctor.OpenListOfRequests();
                        break;
                    case Command.confirm:
                        ConfirmRequest(Select(myDoctor.ListOfRequests, line.Replace(command.ToString(), "").Trim()));
                        break;
                    case Command.refuse:
                        Consultation request = Select(myDoctor.ListOfRequests, line.Replace(command.ToString(), "").Trim());
                        if (request != null)
                        {
                            myDoctor.RefuseARequest(request);
                        }
                        break;
                    case Command.myCalendar:
                        myDoctor.SeeMyCalendar();
                        break;
                }
            }
        }
    }
}
