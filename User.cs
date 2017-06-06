using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital
{
     public class User: IComparable<User>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Possition { get; set; }

        public User(string name, string surname, string login, string password, string possition)
        {
            this.Name = name;
            this.Surname = surname;
            this.Login = login;
            this.Password = password;
            this.Possition = possition;
        }

        public int CompareTo(User other)
        {
            return this.Name.CompareTo(other.Name);
        }
    }
}
