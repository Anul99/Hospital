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
    //interface IAdmin : IUser
    //{
    //    void AddDoctor(string name, string surname, string login, string password, string speciality, List<WorkingTimes> workingTimes, string telephone, int costOfConsultation);
    //    void Report();
    //    void DeleteDoctor(IDoctor doctor);
    //}
    class Admin : User/*, IAdmin*/
    {
        //public string Name { get; set; }
        //public string Surname { get; set; }
        //public string Login { get; set; }
        //public string Password { get; set; }
        //public string Possition { get; set; }

        public Admin(string name, string surname, string login, string password)
        {
            this.Name = name;
            this.Surname = surname;
            this.Login = login;
            this.Password = password;
            this.Possition = "Admin";
        }

        public void AddDoctor(string name, string surname, string login, string password, string speciality, List<WorkingTimes> workingTimes, string telephone, int costOfConsultation)
        {
            Doctor newDoctor = new Doctor(name, surname, login, password, speciality, workingTimes, telephone, costOfConsultation);
            Hospital.AllDoctors.Add(newDoctor);
            //Hospital.AllUsers.Add(newDoctor);
        }

        public void Report()
        {
            throw new NotImplementedException();
        }

        public void DeleteDoctor(Doctor doctor)
        {
            Hospital.AllDoctors.Remove(doctor);
        }

        public override string ToString()
        {
            return this.Possition + ": " + this.Name + " " + this.Surname + "\nLogin: " + this.Login;
        }

        //public int CompareTo(IUser other)
        //{
        //    return this.Name.CompareTo(other.Name);       
        //}


    }
}
