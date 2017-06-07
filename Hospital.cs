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
        private static List<IUser> allUsers = new List<IUser>();
        public static List<IUser> AllUsers
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
                    if ((int)command >= 5 && (int)command < 8)
                    {
                        if (myPatient == null)
                            return Command.nothing;
                        else
                            return command;
                    }
                    if ((int)command >= 8 && (int)command < 13 && myDoctor == null)
                    {
                        if (myDoctor == null)
                            return Command.nothing;
                        else
                            return command;
                    }
                    //if ((int)command >= 13 && myAdmin == null)
                    //{
                    //    if (myAdmin == null)
                    //        return Command.nothing;
                    //    else
                    //        return command;
                    //}
                    if ((int)command < 5)
                        return command;
                }
            }
            return Command.nothing;
        }

        public static IUser Search(string line)
        {
            List<IUser> foundUsers = new List<IUser>();
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
                    Console.WriteLine(i + 1 + ". ");
                    PrintUser(foundUsers[i]);
                }
                return Select(foundUsers);

            }
        }

        private static void SearchByName(string name, List<IUser> foundUsers)
        {
            foreach (IUser u in AllUsers)
            {
                if (u.Name.ToLower() == name.ToLower())
                    foundUsers.Add(u);
            }
        }

        private static void SearchBySurname(string surname, List<IUser> foundUsers)
        {
            foreach (IUser u in AllUsers)
            {
                if (u.Surname.ToLower() == surname.ToLower())
                    foundUsers.Add(u);
            }
        }

        private static void SearchByNameAndSurname(string name, string surname, List<IUser> foundUsers)
        {
            foreach (IUser u in AllUsers)
            {
                if (u.Name.ToLower() == name.ToLower() && u.Surname.ToLower() == surname.ToLower())
                    foundUsers.Add(u);
            }
        }

        private static void SearchByLogin(string login, List<IUser> foundUsers)
        {
            foreach (IUser u in AllUsers)
            {
                if (u.Login == login)
                    foundUsers.Add(u);
            }
        }

        private static void SearchByNameSurnnameAndLogin(string name, string surname, string login, List<IUser> foundUsers)
        {
            foreach (IUser u in AllUsers)
            {
                if (u.Name.ToLower() == name.ToLower() && u.Surname.ToLower() == surname.ToLower() && u.Login == login)
                    foundUsers.Add(u);
            }
        }


        public static IUser Select(List<IUser> list)
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

        public static void SignIn()
        {
            if (myDoctor == null && myPatient == null /*&& myAdmin == null*/)
            {
                Console.Write("Login: ");
                string login = Console.ReadLine().Replace("Login:", "").Trim();
                Console.Write("Password: ");
                string password = GetPasswordFromUser();
                Console.WriteLine();
                foreach (IUser u in AllUsers)
                {
                    if (login == u.Login && password == u.Password)
                    {
                        try
                        {
                            myPatient = (IPatient)u;
                            Console.WriteLine();
                            PrintUser(myPatient);
                            Console.Write("\n         Messeges ");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(myPatient.CountOfUnreadMessages);
                            Console.ForegroundColor = ConsoleColor.White;
                            return;
                        }
                        catch (InvalidCastException)
                        {
                            try
                            {
                                myDoctor = (IDoctor)u;
                                Console.WriteLine();
                                PrintUser(myDoctor);
                                Console.Write("\nRequests ");
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(myDoctor.ListOfRequests.Count);
                                Console.ForegroundColor = ConsoleColor.White;
                                return;
                            }
                            catch (InvalidCastException)
                            {
                                //myAdmin = (Admin)u;
                                //Console.WriteLine();
                                //PrintUser(myAdmin);
                                //return;
                            }
                        }
                    }
                }
                MessageBox.Show("Incorrect login or password.");
            }
            else
            {
                MessageBox.Show("You are already loged in.");
            }
        }

        public static void SignUp()
        {
            Console.Write("Name: ");
            string name = Console.ReadLine().Replace("Name:", "").Trim();
            Console.Write("Surame: ");
            string surname = Console.ReadLine().Replace("Surname:", "").Trim();
            bool t = true;
            string login = "";
            do
            {
                Console.Write("Login: ");
                login = Console.ReadLine().Replace("Login:", "").Trim();
                foreach (IUser u in AllUsers)
                {
                    if (login == u.Login)
                    {
                        MessageBox.Show("This login already exists.");
                        t = false;
                    }
                }
            }
            while (!t);
            Console.Write("Password: ");
            string password = GetPasswordFromUser();
            Console.WriteLine();
            myPatient = new Patient(name, surname, login, password);
            AllUsers.Add(myPatient);
        }

        public static void SignOut()
        {
            if (myPatient != null)
            {
                myPatient = null;
            }
            else if (myDoctor != null)
            {
                myDoctor = null;
            }
            //else if (myAdmin != null)
            //{
            //    myAdmin = null;
            //}
            else
            {
                MessageBox.Show("You are not signed in.");
            }
        }

        private static string GetPasswordFromUser()
        {
            string password = "";
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                    {
                        password = password.Substring(0, (password.Length - 1));
                        Console.Write("\b \b");
                    }
                }
            } while (key.Key != ConsoleKey.Enter);
            //return Encode.Encrypt(password);
            return password;
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

        private static void PrintUser(IUser user)
        {
            Console.WriteLine("{0}: {1} {2} ({3})",user.Possition , user.Name , user.Surname , user.Login);
        }






        private static IPatient myPatient = null;
        private static IDoctor myDoctor = null;
        //private static IAdmin myAdmin = null;
        private static IUser selected = null;

        public static void MyConsole()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("WELCOME TO OUR HOSPITAL :)\n");
            Console.WriteLine("Active commands: " + (Command)0);
            for (int i = 1; i < 5; i++)
            {
                Console.WriteLine("                 " + (Command)i);
            }


            Command command = Command.nothing;
            while (command != Command.exit)
            {
                if (myPatient != null)
                {
                    Console.WriteLine();
                    PrintUser(myPatient);
                }
                else if (myDoctor != null)
                {
                    Console.WriteLine();
                    PrintUser(myDoctor);
                }
                //else if (myAdmin != null)
                //{
                //    Console.WriteLine();
                //    PrintUser(myAdmin);
                //}
                Console.Write("$ ");
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
                        SignIn();
                        break;
                    case Command.signUp:
                        SignUp();
                        break;
                    case Command.signOut:
                        SignOut();
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
